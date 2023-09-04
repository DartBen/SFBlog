using AutoMapper;
using BlogApp.DLL.Models;
using BlogApp.DLL.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlogApp.Pages.Shared.Navbar
{
    public class ArticlesModel : PageModel
    {
        private IArticleRepository _articles;
        private IMapper mapper;
        public List<Article> articles { get; set; }

        public ArticlesModel(IArticleRepository articles, IMapper mapper)
        {
            _articles = articles;
            this.mapper = mapper;
        }

        public async void OnGet()
        {
            articles = new List<Article>();

            var allarticles = _articles.GetAll().Result;
            articles.AddRange(allarticles);
        }
    }
}
