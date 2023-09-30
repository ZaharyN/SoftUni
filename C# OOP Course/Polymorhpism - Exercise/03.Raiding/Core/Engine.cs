using _03.Raiding.Core.Interfaces;
using _03.Raiding.IO.Interfaces;
using _03.Raiding.Models;
using _03.Raiding.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.Raiding.Core
{
    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        public readonly List<string> heroTypes = new List<string>
        {
            "Druid",
            "Paladin",
            "Rogue",
            "Warrior"
        };
        public Engine(IReader reader, IWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
        }
        
        public void Run()
        {
            List<IBaseHero> heroes = new List<IBaseHero>();

            int inputLines = int.Parse(reader.ReadLine());

            for (int i = 0; i < inputLines; i++)
            {
                string currentHeroName = reader.ReadLine();
                string currentHeroType = reader.ReadLine();

                if (!heroTypes.Contains(currentHeroType))
                {
                    Console.WriteLine("Invalid hero!");
                    i--;
                }
                else
                {
                    IBaseHero currentHero = CreateHero(currentHeroName, currentHeroType); 
                    heroes.Add(currentHero);
                }     
            }
            int bossPower = int.Parse(reader.ReadLine());

            foreach (var hero in heroes)
            {
                writer.WriteLine(hero.CastAbility());
            }
            int totalHeroPower = heroes.Sum(x => x.Power);

            if(totalHeroPower >= bossPower)
            {
                writer.WriteLine("Victory!");
            }
            else
            {
                writer.WriteLine("Defeat...");
            }
        }

        static IBaseHero CreateHero(string name, string type)
        {
            if(type == "Druid")
            {
                return new Druid(name);
            }
            else if(type == "Paladin")
            {
                return new Paladin(name);
            }
            else if(type == "Rogue")
            {
                return new Rogue(name);
            }
            
            return new Warrior(name);
        }

    }
}
