using EDriveRent.Models.Contracts;
using EDriveRent.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Models
{
    public abstract class Vehicle : IVehicle
    {
        private string brand;
        private string model;
        private string licensePlateNumber;
        private int batteryLevel;

        protected Vehicle(string brand, string model, double maxMileage, string licensePlateNumber)
        {
            Brand = brand;
            Model = model;
            MaxMileage = maxMileage;
            LicensePlateNumber = licensePlateNumber;
            BatteryLevel = 100;
            IsDamaged = false;
        }

        public string Brand
        {
            get => brand;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.BrandNull));
                }
                brand = value;
            }
        }

        public string Model
        {
            get => model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.ModelNull));
                }
                model = value;
            }
        }

        public double MaxMileage
        {
            get;
            private set;
        }

        public string LicensePlateNumber
        {
            get => licensePlateNumber;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.LicenceNumberRequired));
                }
                licensePlateNumber = value;
            }
        }

        public int BatteryLevel 
        {
            get => batteryLevel;
            private set
            {
                batteryLevel = value;
            }
        }

        public bool IsDamaged
        {
            get;
            private set;
        }

        public void ChangeStatus()
        {
            if (IsDamaged)
            {
                IsDamaged = false;
            }
            else
            {
                IsDamaged = true;
            }
        }

        public void Drive(double mileage)
        {
            int batteryPercentageDrop = (int)(Math.Round(mileage / MaxMileage * 100));

            BatteryLevel -= batteryPercentageDrop;

            if(this.GetType().Name == "CargoVan")
            {
                BatteryLevel -= 5;
            }
        }

        public void Recharge() => BatteryLevel = 100;

        public override string ToString()
        {
            string status = IsDamaged ? "damaged" : "OK";

            return $"{Brand} {Model} License plate: {LicensePlateNumber} Battery: {BatteryLevel}% Status: {status}";
        }
    }
}
