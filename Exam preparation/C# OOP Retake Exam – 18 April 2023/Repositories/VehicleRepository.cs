using EDriveRent.Models.Contracts;
using EDriveRent.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Repositories
{
    public class VehicleRepository : IRepository<IVehicle>
    {
        List<IVehicle> vehicles;

        public VehicleRepository()
        {
            vehicles = new List<IVehicle>();
        }

        public void AddModel(IVehicle model)
        {
            vehicles.Add(model);
        }

        public IVehicle FindById(string identifier) => vehicles.FirstOrDefault(x => x.LicensePlateNumber == identifier);

        public IReadOnlyCollection<IVehicle> GetAll()
        {
            IReadOnlyCollection<IVehicle> vehiclesAsReadOnly = vehicles;
            return vehiclesAsReadOnly;
        }
         
        public bool RemoveById(string identifier) => vehicles.Remove(FindById(identifier));
    }
}
