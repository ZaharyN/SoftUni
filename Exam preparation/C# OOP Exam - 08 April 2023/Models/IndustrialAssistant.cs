using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Models
{
    public class IndustrialAssistant : Robot
    {
        private const int IndustrailAssistantBatteryCapacity = 40_000;
        private const int IndustrailAssistantConversionIndex = 5000;
        public IndustrialAssistant(string model) 
            : base(model, IndustrailAssistantBatteryCapacity, IndustrailAssistantConversionIndex)
        {
        }
    }
}
