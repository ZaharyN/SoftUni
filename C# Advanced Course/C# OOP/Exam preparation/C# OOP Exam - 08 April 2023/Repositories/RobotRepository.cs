using RobotService.Models.Contracts;
using RobotService.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Repositories
{
    public class RobotRepository : IRepository<IRobot>
    {
        private List<IRobot> robots;
        public RobotRepository()
        {
            robots = new List<IRobot>();
        }

        public void AddNew(IRobot model) => robots.Add(model);

        public IRobot FindByStandard(int interfaceStandard) 
            => robots.FirstOrDefault(x => x.InterfaceStandards.Any(x => x == interfaceStandard));

        public IReadOnlyCollection<IRobot> Models() => robots.AsReadOnly();

        // Потенциал за грешка => коригирах robotModel от typeName
        public bool RemoveByName(string robotModel) => robots.Remove(robots.FirstOrDefault(x => x.Model == robotModel));
    }
}
