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
    public class UserRepository : IUserRepository
    {
        private readonly BlogBD _db;

        public UserRepository(BlogBD db)
        {
            _db = db;
        }

        public async Task Create(User item)
        {
            _db.Users.Add(item);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(User item)
        {
            _db.Users.Remove(item);
            await _db.SaveChangesAsync();
        }

        public async Task<User> Get(Guid id)
        {
            return await _db.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task Update(User item)
        {
            _db.Users.Update(item);
            await _db.SaveChangesAsync();
        }
    }
}
