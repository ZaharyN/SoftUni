using RobotService.Models.Contracts;
using RobotService.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Models
{
    public abstract class Robot : IRobot
    {
        private string model;
        private int batteryCapacity;
        private int batteryLevel;
        private int convertionCapacityIndex;
        private List<int> interfaceStandarts;

        public Robot(string model, int batteryCapacity, int conversionCapacityIndex)
        {
            Model = model;
            BatteryCapacity = batteryCapacity;
            convertionCapacityIndex = conversionCapacityIndex;
            batteryLevel = BatteryCapacity;
            interfaceStandarts = new List<int>();
        }

        public string Model
        {
            get => model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.ModelNullOrWhitespace));
                }
                model = value;
            }
        }

        public int BatteryCapacity
        {
            get => batteryCapacity;
            private set
            {
                if(value < 0)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.BatteryCapacityBelowZero));
                }
                batteryCapacity = value;
            }
        }

        public int BatteryLevel => this.batteryLevel;

        public int ConvertionCapacityIndex => convertionCapacityIndex;

        public IReadOnlyCollection<int> InterfaceStandards => this.interfaceStandarts;

        public virtual void Eating(int minutes)
        {
            int producedElectricity = minutes * ConvertionCapacityIndex;

            batteryLevel += producedElectricity;

            if(batteryLevel == BatteryCapacity)
            {
                batteryLevel = BatteryCapacity;
            }
        }

        public bool ExecuteService(int consumedEnergy)
        {
            if(batteryLevel >= consumedEnergy)
            {
                batteryLevel -= consumedEnergy;
                return true;
            }
            return false;
        }

        public void InstallSupplement(ISupplement supplement)
        {
            int currentInterfaceStandart = supplement.InterfaceStandard;

            if(currentInterfaceStandart != null)
            {
                interfaceStandarts.Add(currentInterfaceStandart);
                BatteryCapacity -= supplement.BatteryUsage;
                batteryLevel -= supplement.BatteryUsage;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new();

            sb.AppendLine($"{this.GetType().Name} {Model}:");
            sb.AppendLine($"--Maximum battery capacity: {BatteryCapacity}");
            sb.AppendLine($"--Current battery level: {BatteryLevel}");
            sb.Append($"--Supplements installed: ");

            if(InterfaceStandards.Count == 0)
            {
                sb.Append("none");
            }
            else
            {
                sb.Append($"{string.Join(" ", InterfaceStandards)}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
