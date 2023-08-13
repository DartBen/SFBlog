using BlogApp.DLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DLL.Repository.Interfaces
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> GetAll();
        Task<Article> Get(Guid id);
        Task Create(Article item);
        Task Update(Article item);
        Task Delete(Article item);
    }
}
