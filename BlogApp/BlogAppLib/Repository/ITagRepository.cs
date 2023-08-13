﻿using BlogApp.DLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DLL.Repository
{
    public interface ITagRepository
    {
        IEnumerable<Tag> GetAll();
        Task<Tag> Get(Guid id);
        Task Create(Tag item);
        Task Update(Tag item);
        Task Delete(Tag item);
    }
}
