using BlogApp.DLL.Models;
using BlogApp.DLL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DLL.Repository
{
    public class UserRepository : IUserRepository
    {
        public Task Create(User item)
        {
            throw new NotImplementedException();
        }

        public Task Delete(User item)
        {
            throw new NotImplementedException();
        }

        public Task<User> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task Update(User item)
        {
            throw new NotImplementedException();
        }
    }
}
