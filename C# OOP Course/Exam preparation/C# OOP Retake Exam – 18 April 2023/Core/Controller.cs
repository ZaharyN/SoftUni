using EDriveRent.Core.Contracts;
using EDriveRent.Models;
using EDriveRent.Models.Contracts;
using EDriveRent.Repositories;
using EDriveRent.Repositories.Contracts;
using EDriveRent.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Core
{
    public class Controller : IController
    {
        private IRepository<IUser> users;
        private IRepository<IVehicle> vehicles;
        private IRepository<IRoute> routes;
        public Controller()
        {
            users = new UserRepository();
            vehicles = new VehicleRepository();
            routes = new RouteRepository();
        }

        public string AllowRoute(string startPoint, string endPoint, double length)
        {
            IRoute routeAlreadyExists = routes.GetAll().
                FirstOrDefault(x => x.StartPoint == startPoint
                && x.EndPoint == endPoint
                && x.Length == length);

            if (routeAlreadyExists is not null)
            {
                return $"{startPoint}/{endPoint} - {length} km is already added in our platform.";
            }
            if (routes.GetAll().
                FirstOrDefault(x => x.StartPoint == startPoint
                && x.EndPoint == endPoint
                && x.Length < length) != null)
            {
                return $"{startPoint}/{endPoint} shorter route is already added in our platform.";
            }

            int routeId = routes.GetAll().Count + 1;
            IRoute route = new Route(startPoint, endPoint, length, routeId);
            routes.AddModel(route);

            IRoute sameRouteWithLongerLength = routes.GetAll().
                FirstOrDefault(x => x.StartPoint == startPoint
                && x.EndPoint == endPoint
                && x.Length > length);

            if (sameRouteWithLongerLength != null)
            {
                sameRouteWithLongerLength.LockRoute();
            }
            return $"{startPoint}/{endPoint} - {length} km is unlocked in our platform.";
        }

        public string MakeTrip(string drivingLicenseNumber, string licensePlateNumber, string routeId, bool isAccidentHappened)
        {
            IUser user = users.FindById(drivingLicenseNumber);
            IVehicle vehicle = vehicles.FindById(licensePlateNumber);
            IRoute route = routes.FindById(routeId);

            if (user.IsBlocked)
            {
                return $"User {drivingLicenseNumber} is blocked in the platform! Trip is not allowed.";
            }
            if (vehicle.IsDamaged)
            {
                return $"Vehicle {licensePlateNumber} is damaged! Trip is not allowed.";
            }
            if (route.IsLocked)
            {
                return $"Route {routeId} is locked! Trip is not allowed.";
            }
            vehicle.Drive(route.Length);

            if (isAccidentHappened)
            {
                vehicle.ChangeStatus();
                user.DecreaseRating();
            }
            else
            {
                user.IncreaseRating();
            }

            return vehicle.ToString().TrimEnd();
        }

        public string RegisterUser(string firstName, string lastName, string drivingLicenseNumber)
        {
            IUser userAlreadyExists = users.FindById(drivingLicenseNumber);

            if (userAlreadyExists is not null)
            {
                return $"{drivingLicenseNumber} is already registered in our platform.";
            }

            IUser user = new User(firstName, lastName, drivingLicenseNumber);
            users.AddModel(user);

            return $"{firstName} {lastName} is registered successfully with DLN-{drivingLicenseNumber}";
        }

        public string RepairVehicles(int count)
        {
            IEnumerable<IVehicle> damagedVehicles = vehicles
                .GetAll()
                .Where(x => x.IsDamaged == true)
                .OrderBy(x => x.Brand)
                .ThenBy(c => c.Model);

            int vehiclesCount = 0;

            if (damagedVehicles.Count() < count)
            {
                vehiclesCount = damagedVehicles.Count();
            }
            else
            {
                vehiclesCount = count;
            }

            int counter = 0;

            foreach (var currentVehicle in damagedVehicles)
            {
                if(counter == vehiclesCount)
                {
                    break;
                }
                currentVehicle.ChangeStatus();
                currentVehicle.Recharge();
                counter++;
            }
            return string.Format(OutputMessages.RepairedVehicles,vehiclesCount);
        }

        public string UploadVehicle(string vehicleType, string brand, string model, string licensePlateNumber)
        {
            IVehicle vehicle;

            if (vehicleType != "CargoVan"
                && vehicleType != "PassengerCar")
            {
                return $"{vehicleType} is not accessible in our platform.";
            }

            IVehicle vehicleAlreadyExists = vehicles.FindById(licensePlateNumber);

            if (vehicleAlreadyExists is not null)
            {
                return $"{licensePlateNumber} belongs to another vehicle.";
            }

            if (vehicleType == "CargoVan")
            {
                vehicle = new CargoVan(brand, model, licensePlateNumber);
            }
            else
            {
                vehicle = new PassengerCar(brand, model, licensePlateNumber);
            }
            vehicles.AddModel(vehicle);

            return $"{brand} {model} is uploaded successfully with LPN-{licensePlateNumber}";
        }

        public string UsersReport()
        {
            StringBuilder sb = new ();

            sb.AppendLine("*** E-Drive-Rent ***");

            foreach (var currentUser in users
                .GetAll()
                .OrderByDescending(x => x.Rating)
                .ThenBy(x => x.LastName)
                .ThenBy(x => x.FirstName))
            {
                sb.AppendLine(currentUser.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
