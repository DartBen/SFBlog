using BlogApp.DLL.Models;
using BlogApp.DLL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DLL.Repository
{
    public class CommentRepository : ICommentRepository
    {
        public Task Create(Comment item)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Comment item)
        {
            throw new NotImplementedException();
        }

        public Task<Comment> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Comment> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task Update(Comment item)
        {
            throw new NotImplementedException();
        }
    }
}
