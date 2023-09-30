using AutoMapper;
using BlogApp.BLL.RequestModels;
using BlogApp.DLL.Models;
using BlogApp.DLL.Repository.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private ITagRepository tags;
        private IMapper mapper;

        public TagController(ITagRepository tagRepository, IMapper mapper)
        {
            tags = tagRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// получить тэг по ID
        /// </summary>
        /// <param name="id">GUID ID в БД</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Ошибка API</response>
        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var tag = await tags.Get(id);
            if (tag != null)
                return StatusCode(200, tag);
            else
                return NotFound();
        }

        /// <summary>
        /// получить список всех тэгов
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="204">Ошибка API</response>
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var allTags = await tags.GetAll();
            if (allTags != null)
                return StatusCode(200, allTags);
            else
                return NoContent();
        }

        /// <summary>
        /// Создать тэг
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Уже существует</response>
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(TagRequest request)
        {
            if (request.Id.ToString() == "" || await tags.Get(request.Id) == null)
            {
                var newtag = mapper.Map<TagRequest, Tag>(request);
                await tags.Create(newtag);
                return StatusCode(200);
            }
            else
                return StatusCode(400, "Уже существует");
        }

        /// <summary>
        /// Изменить тэг 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Ошибка API</response>
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(TagRequest request)
        {
            if (await tags.Get(request.Id) != null)
            {
                var newtag = mapper.Map<TagRequest, Tag>(request);
                await tags.Update(newtag);
                return StatusCode(200);
            }
            else
                return NotFound();
        }

        /// <summary>
        /// Удалить тэг по ID
        /// </summary>
        /// <param name="id">GUID ID в БД</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Ошибка API</response>
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var tag = await tags.Get(id);
            if (tag != null)
            {
                await tags.Delete(tag);
                return StatusCode(200);
            }
            return NotFound();
        }
    }
}
