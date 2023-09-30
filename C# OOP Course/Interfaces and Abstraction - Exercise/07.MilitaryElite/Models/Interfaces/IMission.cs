﻿using _07.MilitaryElite.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _07.MilitaryElite.Models.Interfaces
{
    public interface IMission
    {
        string CodeName { get; }
        State State { get; }
        void CompleteMission();
    }
}
