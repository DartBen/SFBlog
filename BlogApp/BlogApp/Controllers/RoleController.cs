using AutoMapper;
using BlogApp.BLL.RequestModels;
using BlogApp.Views;
using BlogApp.DLL.Models;
using BlogApp.DLL.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BlogApp.Controllers
{
    [Route("api/[controller]")]
    public class RoleController : Controller
    {
        private IRoleRepository roles;
        private IMapper mapper;

        public RoleController(IRoleRepository roleRepository, IMapper mapper)
        {
            roles = roleRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var role = await roles.Get(id);
            if (role != null)
                return StatusCode(200, role);
            else
                return NotFound();
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var allTags = await roles.GetAll();
            if (allTags != null)
                return StatusCode(200, allTags);
            else
                return NoContent();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(RoleReqest request)
        {
            if (request.Id.ToString() == "" || await roles.Get(request.Id) == null)
            {
                var role = mapper.Map<RoleReqest, Role>(request);
                await roles.Create(role);
                return StatusCode(200);
            }
            else
                return StatusCode(400, "Уже существует");
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(RoleReqest request)
        {
            if (await roles.Get(request.Id) != null)
            {
                var role = mapper.Map<RoleReqest, Role>(request);
                await roles.Update(role);
                return StatusCode(200);
            }
            else
                return NotFound();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var role = await roles.Get(id);
            if (role != null)
            {
                await roles.Delete(role);
                return StatusCode(200);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("AddRole")]
        public async Task<IActionResult> AddRole(CreateRoleViewModel model)
        {
            var allroles = await roles.GetAll();
            var temp = allroles.Where(x => x.Name == model.Name).FirstOrDefault();
            if (temp != null) return StatusCode(400);
            RoleReqest request = new RoleReqest();
            request.Id = Guid.NewGuid();
            request.Name = model.Name;

            var result = Create(request);

            return RedirectToPage("/Index");
        }
    }
}
