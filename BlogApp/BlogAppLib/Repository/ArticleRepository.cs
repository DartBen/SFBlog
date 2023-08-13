using BlogApp.DLL.Models;
using BlogApp.DLL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DLL.Repository
{
    public class ArticleRepository : IArticleRepository
    {
        public Task Create(Article item)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Article item)
        {
            throw new NotImplementedException();
        }

        public Task<Article> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Article> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task Update(Article item)
        {
            throw new NotImplementedException();
        }
    }
}
