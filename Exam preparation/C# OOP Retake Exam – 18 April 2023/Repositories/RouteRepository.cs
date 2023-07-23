using EDriveRent.Models.Contracts;
using EDriveRent.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Repositories
{
    public class RouteRepository : IRepository<IRoute>
    {

        List<IRoute> routes;

        public RouteRepository()
        {
            routes = new List<IRoute>();
        }

        public void AddModel(IRoute model)
        {
            routes.Add(model);
        }

        public IRoute FindById(string identifier) => routes.FirstOrDefault(x => x.RouteId == int.Parse(identifier));

        public IReadOnlyCollection<IRoute> GetAll()
        {
            IReadOnlyCollection<IRoute> routesAsReadOnly = routes;

            return routesAsReadOnly;
        }

        public bool RemoveById(string identifier) => routes.Remove(FindById(identifier));
    }
}
