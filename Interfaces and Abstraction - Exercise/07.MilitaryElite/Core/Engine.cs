using _07.MilitaryElite.Core.Interfaces;
using _07.MilitaryElite.Enums;
using _07.MilitaryElite.Models;
using _07.MilitaryElite.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace _07.MilitaryElite.Core
{
    public class Engine : IEngine
    {
        private Dictionary<int, ISoldier> soldiers;

        public Engine()
        {
            soldiers = new Dictionary<int, ISoldier>();
        }
        public void Run()
        {
            string input = string.Empty;

            while ((input = Console.ReadLine()) != "End")
            {
                string[] soldierInfo = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                try
                {
                    ISoldier soldier = GetProperSoldierInstance(soldierInfo);

                    if(soldier != null)
                    {
                        soldiers.Add(soldier.Id, soldier);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            foreach (var soldier in soldiers.Values)
            {
                Console.WriteLine(soldier.ToString());
            }
        }
        
        public ISoldier GetProperSoldierInstance(string[] soldierInfo)
        {
            string soldierType = soldierInfo[0];
            int soldierId = int.Parse(soldierInfo[1]);
            string firstName = soldierInfo[2];
            string lastName = soldierInfo[3];

            if(soldierType == "Private")
            {
                return new Private(soldierId, firstName, lastName, 
                    decimal.Parse(soldierInfo[4]));
            }
            else if(soldierType == "LieutenantGeneral")
            {
                List <IPrivate> privates= new List<IPrivate>();

                for (int i = 5; i < soldierInfo.Length; i++)
                {
                    int currentSoldierId = int.Parse(soldierInfo[i]);

                    IPrivate currnetPrivate = (IPrivate)soldiers[currentSoldierId];
                    privates.Add(currnetPrivate);
                }

                return new LieutenantGeneral(soldierId, firstName, lastName, 
                    decimal.Parse(soldierInfo[4]), 
                    privates);
            }
            else if(soldierType == "Engineer")
            {
                bool corpsIsValid = Enum.TryParse<Corps>(soldierInfo[5], out Corps corps);

                if(!corpsIsValid)
                {
                    return null;
                }
                List<IRepair> repairs = new List<IRepair>();

                for (int i = 6; i < soldierInfo.Length; i+=2)
                {
                    string partName = soldierInfo[i];
                    int hours = int.Parse(soldierInfo[i+1]);

                    IRepair currentRepair= new Repair(partName, hours);

                    repairs.Add(currentRepair);
                }

                return new Engineer(soldierId, firstName, lastName, decimal.Parse(soldierInfo[4]), corps, repairs);
            }
            else if(soldierType == "Commando")
            {
                bool corpsIsValid = Enum.TryParse<Corps>(soldierInfo[5], out Corps corps);

                if (!corpsIsValid)
                {
                    return null;
                }

                List<IMission> missions = new List<IMission>();

                for (int i = 6; i < soldierInfo.Length; i += 2)
                {
                    string codeName = soldierInfo[i];
                    string missionState = soldierInfo[i+1];

                    bool stateIsValid = Enum.TryParse<State>(missionState, out State state);

                    if (!stateIsValid)
                    {
                        continue;
                    }

                    IMission currentMission = new Mission(codeName, state);

                    missions.Add(currentMission);
                }
                return new Commando(soldierId, firstName, lastName, decimal.Parse(soldierInfo[4]), corps, missions);
            }
            else if(soldierType == "Spy")
            {
                return new Spy(soldierId, firstName, lastName, int.Parse(soldierInfo[4]));
            }
            return null;
        }
    }
}
