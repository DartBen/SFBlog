using AutoMapper;
using BlogApp.DLL.Models;
using BlogApp.DLL.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Xml.Linq;

namespace BlogApp.Pages
{
    public class ArticlePageModel : PageModel
    {
        private IArticleRepository _articles;
        private ITagRepository _tags;
        private ICommentRepository _comments;
        private IMapper _mapper;
        public object? Id { get; private set; }

        public Article article { get; set; }


        public string BodyText { get; set; }
        public Guid articleId { get; set; }

        public ArticlePageModel(IArticleRepository articleRepository, ITagRepository tagRepository, ICommentRepository commentRepository, IMapper mapper)
        {
            _articles = articleRepository;
            _tags = tagRepository;
            _comments = commentRepository;
            _mapper = mapper;
        }

        public async void OnGet()
        {
            Id = RouteData.Values["id"];
            Guid guid= (Guid)TypeDescriptor.GetConverter(typeof(Guid)).ConvertFromString((string)RouteData.Values["id"]);
            article = await _articles.Get(guid);
        }
    }

}