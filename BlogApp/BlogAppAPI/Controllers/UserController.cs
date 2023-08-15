using AutoMapper;
using BlogApp.DLL.Models;
using BlogApp.DLL.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository users;
        private IMapper mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            users = userRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<User>> GetAll()
        {
            return await users.GetAll();
        }

        [HttpGet]
        [Route("{GetById}")]
        public async Task<User> GetById(Guid id)
        {
            return await users.Get(id);
        }

        [HttpPost]
        [Route("Create")]
        public async Task Create([FromBody] User user)
        {
            await users.Create(user);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task Update(Guid id, [FromBody] User user)
        {
            await users.Update(user);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete(Guid id)
        {
            var user = await users.Get(id);
            await users.Delete(user);
        }
    }
}
