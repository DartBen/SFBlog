using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DLL.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }

        // Статьи потльзователя
        public List<Article> Articles { get; set; } = new List<Article>();

        // комментарии пользователя
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
