﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BlogApp.Views
{
    public class CreateRoleViewModel
    {
        [Required]
        [Display(Name = "Название", Prompt = "Введите название")]
        public string Name { get; set; }

        [Display(Name = "Описание", Prompt = "Введите описание")]
        public string Comment { get; set; }
    }
}
