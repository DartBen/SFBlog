using AutoMapper;
using BlogApp.BLL.RequestModels;
using BlogApp.Views;
using BlogApp.DLL.Models;
using BlogApp.DLL.Repository.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Xml.Linq;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogApp.Controllers
{
    [Route("api/[controller]")]
    public class TagController : Controller
    {
        private ITagRepository tags;
        private IMapper mapper;

        public TagController(ITagRepository tagRepository, IMapper mapper)
        {
            tags = tagRepository;
            this.mapper = mapper;
        }

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

        [HttpGet]
        [Route("GetAllByName")]
        public async Task<IActionResult> GetAllByName(string name)
        {
            var allTags = await tags.GetAll();
            var temp = allTags.Where(x => x.TagName == name).FirstOrDefault();
            if (temp != null)
                return StatusCode(200, temp);
            else
                return NoContent();
        }

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

        [Route("CreateTag")]
        public IActionResult CreateTag()
        {
            return RedirectToPage("/CreateTagPage");
        }

        [HttpPost]
        [Route("AddTag")]
        public async Task<IActionResult> AddTag(CreateTagViewModel model)
        {
            var allTags = await tags.GetAll();
            var temp = allTags.Where(x => x.TagName == model.Name).FirstOrDefault();
            if (temp != null) return StatusCode(400);
            TagRequest request = new TagRequest();
            request.Id = Guid.NewGuid();
            request.TagName = model.Name;

            var result = Create(request);

            return RedirectToPage("/Index");
        }

        //[HttpPost]
        [Route("GetTagToUpdate/{id?}")]
        public IActionResult GetTagToUpdate(CreateTagViewModel model, [FromRoute] Guid ID)
        {
            //return RedirectToRoute("TagUpdatePage", new {id=Id});
            return RedirectToPage("/TagUpdatePage", new { id = ID.ToString()});
        }

        [Route("TagUpdateById/{id?}")]
        public async Task<IActionResult> TagUpdateById(CreateTagViewModel model, [FromRoute] Guid ID)
        {
            try
            {
                var tmpTag = await GetById(ID);
                var tag = (Tag)(tmpTag as ObjectResult).Value;
                tag.TagName = model.Name;

                await tags.Update(tag);
            }
            catch { }


            return RedirectToPage("/Navbar/Tags");
        }
    }
}
