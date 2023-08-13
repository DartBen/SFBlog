﻿using BlogApp.DLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DLL.Repository
{
    public interface ICommentRepository
    {
        IEnumerable<Comment> GetAll();
        Task<Comment> Get(Guid id);
        Task Create(Comment item);
        Task Update(Comment item);
        Task Delete(Comment item);
    }
}
