using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.BLL.Views
{
    public class RegistrationViewModel
    {
        [Required]
        [Display(Name = "Login", Prompt = "Введите Login")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль", Prompt = "Введите пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; } = false;

        public string ReturnUrl { get; set; } = string.Empty;
    }
}
