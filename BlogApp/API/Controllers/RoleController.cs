using AutoMapper;
using BlogApp.BLL.RequestModels;
using BlogApp.DLL.Models;
using BlogApp.DLL.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private IRoleRepository roles;
        private IMapper mapper;

        public RoleController(IRoleRepository roleRepository, IMapper mapper)
        {
            roles = roleRepository;
            this.mapper = mapper;
        }


        /// <summary>
        /// получить роль по ID
        /// </summary>
        /// <param name="id">GUID ID в БД</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Ошибка API</response>
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
        /// <summary>
        /// получить список всех ролей
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="204">Ошибка API</response>
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
        /// <summary>
        /// создать роль
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Уже существует</response>
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
        /// <summary>
        /// обновить роль
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Ошибка API</response>
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
        /// <summary>
        /// удалить роль по id
        /// </summary>
        /// <param name="id">GUID ID в БД</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Ошибка API</response>
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
    }
}
