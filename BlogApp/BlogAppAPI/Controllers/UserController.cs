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
        private IArticleRepository articles;
        private ITagRepository tags;
        private ICommentRepository comments;
        private IMapper mapper;

        public UserController(IUserRepository userRepository, ICommentRepository commentRepository,
            ITagRepository tagRepository, IArticleRepository articleRepository, IMapper mapper)
        {
            users = userRepository;
            comments = commentRepository;
            tags = tagRepository;
            articles = articleRepository;
            this.mapper = mapper;
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<IEnumerable<User>> GetAll()
        {
            return await users.GetAll();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<User> Get(Guid id)
        {
            return await users.Get(id);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task PostAsync([FromBody] User user)
        {
            await users.Create(user);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task PutAsync(Guid id, [FromBody] User user)
        {
            await users.Update(user);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            var user = await users.Get(id);
            await users.Delete(user);
        }
    }
}
