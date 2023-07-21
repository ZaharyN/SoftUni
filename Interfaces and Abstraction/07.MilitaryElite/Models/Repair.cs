using _07.MilitaryElite.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _07.MilitaryElite.Models
{
    public class Repair : IRepair
    {
        public Repair(string partName, int hours)
        {
            PartName = partName;
            Hours = hours;
        }

        public string PartName 
        {
            get; 
            private set;
        }

        public int Hours 
        { 
            get; 
            private set; 
        }
        public override string ToString() => $"Part Name: {PartName} Hours Worked: {Hours}";
    }
}
