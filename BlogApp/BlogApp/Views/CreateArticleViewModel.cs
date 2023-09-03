using BlogApp.Pages;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BlogApp.Views
{
    public class CreateArticleViewModel
    {
        [Required]
        [Display(Name = "Название", Prompt = "Введите название")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Текст", Prompt = "Введите текст")]
        public string ArticleBody { get; set; }

        [Display(Name = "Описание", Prompt = "Введите описание")]
        public string Comment { get; set; }

        [Required]
        public List<CheckTag> CheckTags { get; set; }

        public CreateArticleViewModel() { }
    }
}
