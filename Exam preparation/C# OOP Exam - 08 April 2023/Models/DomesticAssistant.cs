using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Models
{
    public class DomesticAssistant : Robot
    {
        private const int domesticAssistantBatteryCapacity = 20_000;
        private const int domesticAssistantconvertionIndex = 2000;
        public DomesticAssistant(string model) 
            : base(model, domesticAssistantBatteryCapacity, domesticAssistantconvertionIndex)
        {
        }
    }
}
