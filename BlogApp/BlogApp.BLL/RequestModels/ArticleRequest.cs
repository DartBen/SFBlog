﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.BLL.RequestModels
{
    public class ArticleRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string BodyText { get; set; }
    }
}
