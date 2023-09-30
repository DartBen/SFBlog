using AutoMapper;
using BlogApp.BLL.RequestModels;
using BlogApp.DLL.Models;
using BlogApp.DLL.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private IArticleRepository articles;
        private IMapper mapper;

        public ArticleController(IArticleRepository articleRepository, IMapper mapper)
        {
            articles = articleRepository;
            this.mapper = mapper;
        }
        /// <summary>
        /// получить все статьи
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Ошибка API</response>
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
        /// <summary>
        /// получить все статьи по id автора
        /// </summary>
        /// <param name="authorGuid">GUID ID в БД</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="204">Ошибка API</response>
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
        /// <summary>
        /// получить статью по ID
        /// </summary>
        /// <param name="id">GUID ID в БД</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Ошибка API</response>
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
        /// <summary>
        /// создать статью 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Уже существует</response>
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
        /// <summary>
        /// обновить статью
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Ошибка API</response>
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

        /// <summary>
        /// удалить по ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Ошибка API</response>
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
    }
}
