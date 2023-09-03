using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BlogApp.Views
{
    public class TagUpdateViewModel
    {
        [Required]
        [Display(Name = "Название", Prompt = "Введите название")]
        public string Name { get; set; }

        [Display(Name = "Описание", Prompt = "Введите описание")]
        public string? Comment { get; set; }

        public TagUpdateViewModel() { }
    }
}
