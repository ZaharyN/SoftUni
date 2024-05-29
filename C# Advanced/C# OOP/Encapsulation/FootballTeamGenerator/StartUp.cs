namespace FootballTeamGenerator
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<Team> teams = new List<Team>();
            string input;

            while ((input = Console.ReadLine()) != "END")
            {
                string[] commandArgs = input
                    .Split(';');

                string operation = commandArgs[0];
                string teamName = commandArgs[1];

                if (operation == "Team")
                {
                    try
                    {
                        Team team = new(teamName);
                        teams.Add(team);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else if (operation == "Add")
                {
                    string playerName = commandArgs[2];
                    int endurance = int.Parse(commandArgs[3]);
                    int sprint = int.Parse(commandArgs[4]);
                    int dribble = int.Parse(commandArgs[5]);
                    int passing = int.Parse(commandArgs[6]);
                    int shooting = int.Parse(commandArgs[7]);

                    try
                    {
                        Team teamToAddPlayer = teams.FirstOrDefault(x => x.Name == teamName);


                        if (teamToAddPlayer == null)
                        {
                            throw new ArgumentException($"Team {teamName} does not exist.");
                        }

                        Player player = new(playerName, endurance, sprint, dribble, passing, shooting);

                        teamToAddPlayer.AddPlayer(player);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else if (operation == "Remove")
                {
                    string playerToRemove = commandArgs[2];
                    try
                    {
                        Team teamToRemoveAPlayer = teams.FirstOrDefault(x => x.Name == teamName);

                        if (teamToRemoveAPlayer == null)
                        {
                            Console.WriteLine($"Team {teamName} does not exist.");
                        }
                        else
                        {
                            teamToRemoveAPlayer.RemovePlayer(playerToRemove);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else if (operation == "Rating")
                {
                    try
                    {
                        Team teamToView = teams.FirstOrDefault(x => x.Name == teamName);

                        if (teamToView != null)
                        {
                            Console.WriteLine(teamToView.ToString());
                        }
                        else
                        {
                            Console.WriteLine($"Team {teamName} does not exist.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}