using RobotService.Core.Contracts;
using RobotService.Models;
using RobotService.Models.Contracts;
using RobotService.Repositories;
using RobotService.Repositories.Contracts;
using RobotService.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Core
{
    public class Controller : IController
    {
        private IRepository<IRobot> robotRepository;
        private IRepository<ISupplement> supplementRepository;

        public Controller()
        {
            robotRepository = new RobotRepository();
            supplementRepository = new SupplementRepository();
        }

        public string CreateRobot(string model, string typeName)
        {
            if (typeName != "DomesticAssistant"
                && typeName != "IndustrialAssistant")
            {
                return string.Format(OutputMessages.RobotCannotBeCreated, typeName);
            }

            IRobot robot;

            if (typeName == "DomesticAssistant")
            {
                robot = new DomesticAssistant(model);
            }
            else
            {
                robot = new IndustrialAssistant(model);
            }

            robotRepository.AddNew(robot);

            return string.Format(OutputMessages.RobotCreatedSuccessfully, typeName, model);
        }

        public string CreateSupplement(string typeName)
        {
            if (typeName != "SpecializedArm"
                && typeName != "LaserRadar")
            {
                return string.Format(OutputMessages.SupplementCannotBeCreated, typeName);
            }
            ISupplement supplement;

            if (typeName == "SpecializedArm")
            {
                supplement = new SpecializedArm();
            }
            else
            {
                supplement = new LaserRadar();
            }

            supplementRepository.AddNew(supplement);

            return string.Format(OutputMessages.SupplementCreatedSuccessfully, typeName, typeName);

        }

        public string PerformService(string serviceName, int intefaceStandard, int totalPowerNeeded)
        {
            IEnumerable<IRobot> robotsThatSupportStandart = robotRepository
                .Models()
                .Where(x => x.InterfaceStandards
                .Any(x => x == intefaceStandard));

            if (robotsThatSupportStandart.Count() == 0)
            {
                return string.Format(OutputMessages.UnableToPerform, intefaceStandard);
            }

            robotsThatSupportStandart = robotsThatSupportStandart.OrderByDescending(x => x.BatteryLevel);

            int robotsPower = robotsThatSupportStandart.Sum(x => x.BatteryLevel);

            if(robotsPower < totalPowerNeeded)
            {
                return string.Format(OutputMessages.MorePowerNeeded, serviceName, totalPowerNeeded - robotsPower);
            }

            int counter = 0;

            foreach (var robot in robotsThatSupportStandart)
            {
                counter++;

                if (robot.BatteryLevel >= totalPowerNeeded)
                {
                    robot.ExecuteService(totalPowerNeeded);
                    break;
                }
                else
                {
                    totalPowerNeeded -= robot.BatteryLevel;
                    robot.ExecuteService(robot.BatteryLevel);
                }
            }

            return string.Format(OutputMessages.PerformedSuccessfully, serviceName, counter);
        }

        public string Report()
        {
            StringBuilder sb = new();

            foreach (var robot in robotRepository
                .Models()
                .OrderByDescending(x=>x.BatteryLevel)
                .ThenBy(x=>x.BatteryCapacity))
            {
                sb.AppendLine(robot.ToString());
            }

            return sb.ToString().TrimEnd();
        }

        public string RobotRecovery(string model, int minutes)
        {
            int robotsFed = 0;

            foreach (var robot in robotRepository
                .Models()
                .Where(x=>x.Model == model && x.BatteryLevel < (x.BatteryCapacity / 2)))
            {
                robot.Eating(minutes);
                robotsFed++;
            }

            return string.Format(OutputMessages.RobotsFed, robotsFed);
        }

        public string UpgradeRobot(string model, string supplementTypeName)
        {
            ISupplement supplementToFind = supplementRepository
                .Models()
                .FirstOrDefault(x => x.GetType().Name == supplementTypeName);

            int interfaceValueToTake = supplementToFind.InterfaceStandard;

            IEnumerable<IRobot> robotsToUpgrade = robotRepository
                .Models()
                .Where(x => x.InterfaceStandards.All(x => x != interfaceValueToTake));

            robotsToUpgrade = robotsToUpgrade.Where(x => x.Model == model);

            if (robotsToUpgrade.Count() == 0)
            {
                return string.Format(OutputMessages.AllModelsUpgraded, model);
            }

            IRobot robotToUpgrade = robotsToUpgrade.FirstOrDefault();

            robotToUpgrade.InstallSupplement(supplementToFind);
            supplementRepository.RemoveByName(supplementTypeName);

            return string.Format(OutputMessages.UpgradeSuccessful, model, supplementTypeName);
        }
    }
}
