using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballTeamGenerator
{
    public class Team
    {
        private string name;
        private readonly List<Player> players;
        public Team(string name)
        {
            Name = name;
            players = new List<Player>();
        }
        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("A name should not be empty.");
                }
                name = value;
            }
        }
        
        public double Rating
        {
            get
            {
                if (players.Any())
                {
                    return players.Average(x => x.SkillLevel);
                }
                return 0;
            }
        }

        public void AddPlayer(Player player) => players.Add(player);

        public void RemovePlayer(string playerName)
        {
            Player playerToRemove = players.FirstOrDefault(x => x.Name == playerName);

            if (playerToRemove == null)
            {
                throw new ArgumentException($"Player {playerName} is not in {this.Name} team.");
            }

            players.Remove(playerToRemove);
        }
        public override string ToString() => $"{Name} - {Rating:f0}";
    }
}
