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
    public class CommentRepository : ICommentRepository
    {
        private readonly BlogBD _db;

        public CommentRepository(BlogBD db)
        {
            _db = db;
        }

        public async Task Create(Comment item)
        {
            _db.Comments.Add(item);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Comment item)
        {
            _db.Comments.Remove(item);
            await _db.SaveChangesAsync();
        }

        public async Task<Comment> Get(Guid id)
        {
            return await _db.Comments.FindAsync(id);
        }

        public async Task<IEnumerable<Comment>> GetAll()
        {
            return await _db.Comments.ToListAsync();
        }

        public async Task Update(Comment item)
        {
            _db.Comments.Update(item);
            await _db.SaveChangesAsync();
        }
    }
}
