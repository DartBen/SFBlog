using AutoMapper;
using BlogApp.BLL.RequestModels;
using BlogApp.DLL.Models;
using BlogApp.DLL.Repository.Interfaces;
using BlogApp.Pages;
using BlogApp.Views;
using Microsoft.AspNetCore.Mvc;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogApp.Controllers
{
    [Route("api/[controller]")]
    public class ArticleController : Controller
    {
        private IArticleRepository articles;
        private ITagRepository tags;
        private IMapper mapper;

        public ArticleController(IArticleRepository articleRepository, ITagRepository tagRepository, IMapper mapper)
        {
            articles = articleRepository;
            tags = tagRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var allArticle = await articles.GetAll();
            if (allArticle != null)
            {
                return StatusCode(200, allArticle);
            }
            else
                return NoContent();
        }
        [HttpGet]
        [Route("GetAllByAuthor")]
        public async Task<IActionResult> GetAllByAuthorId(Guid authorGuid)
        {
            var allArticle = await articles.GetAllByAuthorId(authorGuid);

            if (allArticle != null)
            {
                return StatusCode(200, allArticle);
            }
            else
                return NoContent();
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var article = await articles.Get(id);
            if (article != null)
                return StatusCode(200, article);
            else
                return NotFound();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(ArticleRequest request)
        {
            if (request.Id.ToString() == "" || await articles.Get(request.Id) == null)
            {
                var newArticle = mapper.Map<ArticleRequest, Article>(request);
                await articles.Create(newArticle);
                return StatusCode(200);
            }
            else
                return StatusCode(400, "Уже существует");
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(ArticleRequest request)
        {
            if (await articles.Get(request.Id) != null)
            {
                var newArticle = mapper.Map<ArticleRequest, Article>(request);
                await articles.Update(newArticle);
                return StatusCode(200);
            }
            else
                return NotFound();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var newArticle = await articles.Get(id);
            if (newArticle != null)
            {
                await articles.Delete(newArticle);
                return StatusCode(200);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("AddArticle")]
        public async Task<IActionResult> AddArticle(CreateArticleViewModel model)
        {
            try
            {
                Console.WriteLine("AddArticle");
                List<Tag> requastTags = new List<Tag>();

                var allTags = await tags.GetAll();



                foreach (var c in model.CheckTags)
                {
                    var tmp = allTags.FirstOrDefault(x => x.TagName == c.tagName & c.RememberMe);
                    if (tmp != null)
                        requastTags.Add(tmp);
                }

                Article article = new Article();
                article.Tags = requastTags;
                article.BodyText = model.ArticleBody;
                article.CreateTime = DateTime.Now;
                article.Title = model.Name;

                CreateArticle(article);


            }
            catch { }

            return RedirectToPage("/Index");
        }

        [HttpPost]
        [Route("CreateArticle")]
        public async Task<IActionResult> CreateArticle(Article article)
        {
            if (article.Id.ToString() == "" || await articles.Get(article.Id) == null)
            {
                await articles.Create(article);
                return StatusCode(200);
            }
            else
                return StatusCode(400, "Уже существует");
        }


        [Route("CreateArticle")]
        public IActionResult CreateArticle()
        {
            return RedirectToPage("/CreateArticlePage");
        }

        [Route("GetArticleToUpdate/{id?}")]
        public IActionResult GetArticleToUpdate(ArticleUpdateViewModel model, [FromRoute] Guid ID)
        {
            return RedirectToPage("/ArticleUpdatePage", new { id = ID.ToString() });
        }

        [Route("ArticleToDelete/{id?}")]
        public IActionResult ArticleToDelete(ArticleUpdateViewModel model, [FromRoute] Guid ID)
        {
            var result = Delete(ID);

            return RedirectToPage("/Navbar/Articles");
        }

    }
}
