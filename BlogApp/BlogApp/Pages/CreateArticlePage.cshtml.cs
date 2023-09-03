using AutoMapper;
using BlogApp.DLL.Models;
using BlogApp.DLL.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BlogApp.Pages
{
    public class CreateArticlePageModel : PageModel
    {
        private IArticleRepository articles;
        private ITagRepository tags;
        private IMapper mapper;


        public List<CheckTag> CheckTags { get; set; }

        [Required]
        [Display(Name = "Название", Prompt = "Введите название")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Текст", Prompt = "Введите текст")]
        public string ArticleBody { get; set; }

        [Display(Name = "Описание", Prompt = "Введите описание")]
        public string Comment { get; set; }

        public CreateArticlePageModel(IArticleRepository articleRepository, ITagRepository tagRepository, IMapper mapper)
        {
            articles = articleRepository;
            tags = tagRepository;
            this.mapper = mapper;
        }

        public void OnGet()
        {
            CheckTags = new List<CheckTag>();
            var allTags = tags.GetAll().Result;

            foreach (var existTag in allTags)
            {
                var tmp = new CheckTag();
                tmp.RememberMe = false;
                tmp.tagName = existTag.TagName;
                CheckTags.Add(tmp);
            }
        }

        public void OnPost( )
        {

        }
    }

    public class CheckTag
    {
        public bool RememberMe { get; set; } = false;
        public string tagName { get; set; }
        public CheckTag(bool rememberMe, Tag tag)
        {
            RememberMe = rememberMe;
            this.tagName = tag.TagName;
        }

        public CheckTag() { }
    }
}
