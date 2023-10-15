using EDriveRent.Models.Contracts;
using EDriveRent.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Repositories
{
    public class UserRepository : IRepository<IUser>
    {
        private List<IUser> users;
        public UserRepository()
        {
            users = new List<IUser>();
        }
        public void AddModel(IUser user)
        {
            users.Add(user);
        }

        public IUser FindById(string identifier) => users.FirstOrDefault(x => x.DrivingLicenseNumber == identifier);

        public IReadOnlyCollection<IUser> GetAll()
        {
            IReadOnlyCollection<IUser> usersAsIReadCollection = users;

            return usersAsIReadCollection;
        }

        public bool RemoveById(string identifier) => users.Remove(FindById(identifier));
    }
}
