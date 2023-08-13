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
    public class TagRepository : ITagRepository
    {
        private readonly BlogBD _db;

        public TagRepository(BlogBD db)
        {
            _db = db;
        }

        public async Task Create(Tag item)
        {
            _db.Tags.Add(item);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Tag item)
        {
            _db.Tags.Remove(item);
            await _db.SaveChangesAsync();
        }

        public async Task<Tag> Get(Guid id)
        {
            return await _db.Tags.FindAsync(id);
        }

        public async Task<IEnumerable<Tag>> GetAll()
        {
            return await _db.Tags.ToListAsync();
        }

        public async Task Update(Tag item)
        {
            _db.Tags.Update(item);
            await _db.SaveChangesAsync();
        }
    }
}
