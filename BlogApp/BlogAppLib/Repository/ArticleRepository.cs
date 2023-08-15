using BlogApp.DLL.Context;
using BlogApp.DLL.Models;
using BlogApp.DLL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DLL.Repository
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly BlogDB _db;

        public ArticleRepository(BlogDB db)
        {
            _db = db;
        }

        public async Task Create(Article item)
        {
            var entry = _db.Entry(item);
            if (entry.State == EntityState.Detached)
                _db.Articles.Add(item);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Article item)
        {
            _db.Articles.Remove(item);
            await _db.SaveChangesAsync();
        }

        public async Task<Article> Get(Guid id)
        {
            return await _db.Articles.FindAsync(id);
        }

        public async Task<IEnumerable<Article>> GetAll()
        {
            return await _db.Articles.ToListAsync();
        }

        public async Task Update(Article item)
        {
            _db.Articles.Update(item);
            await _db.SaveChangesAsync();
        }
    }
}
