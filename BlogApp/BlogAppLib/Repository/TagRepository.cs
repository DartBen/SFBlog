using BlogApp.DLL.Models;
using BlogApp.DLL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DLL.Repository
{
    public class TagRepository : ITagRepository
    {
        public Task Create(Tag item)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Tag item)
        {
            throw new NotImplementedException();
        }

        public Task<Tag> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tag> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task Update(Tag item)
        {
            throw new NotImplementedException();
        }
    }
}
