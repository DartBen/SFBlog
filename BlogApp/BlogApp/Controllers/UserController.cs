using AutoMapper;
using BlogApp.BLL.RequestModels;
using BlogApp.DLL.Models;
using BlogApp.DLL.Repository.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogAppAPI.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository users;
        private IRoleRepository roles;
        private IMapper mapper;

        public UserController(IUserRepository userRepository,IRoleRepository role, IMapper mapper)
        {
            users = userRepository;
            roles = role;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var allUsers = await users.GetAll();
            if (allUsers != null)
            {
                return StatusCode(200, allUsers);
            }
            else
                return NoContent();
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await users.Get(id);
            if (user != null)
                return StatusCode(200, user);
            else
                return NotFound();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(UserRequest request)
        {
            Guid guid = Guid.NewGuid();
            if (await users.Get(guid) == null)
            {
                var newUser = mapper.Map<UserRequest, User>(request);
                newUser.Id = guid;
                await users.Create(newUser);
                return StatusCode(200);
            }
            else
                return StatusCode(400, "Уже существует");
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(UserRequest request)
        {
            if (await users.Get(request.Id) != null)
            {
                var newUser = mapper.Map<UserRequest, User>(request);
                await users.Update(newUser);
                return StatusCode(200);
            }
            else
                return NotFound();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await users.Get(id);
            if (user != null)
            {
                await users.Delete(user);
                return StatusCode(200);
            }
            return NotFound();
        }

        [HttpPut]
        [Route("UpdateRole")]
        public async Task<IActionResult> UpdateRole(RoleReqest reqest, Guid userId)
        {
            var role = await roles.GetByName(reqest.Name);
            var user = await users.Get(userId);
            if (user != null && role != null)
            {
                user.Roles.Add(role);
                await users.Update(user);
                return StatusCode(200);
            }
            else
                return NotFound();
        }

        [HttpPost]
        [Route("Authenticate")]
        public async Task<User> Authenticate(UserRequest request, string login, string password)
        {
            var user = users.GetByLogin(login).Result;
            if (user.Login != login)
                throw new AuthenticationException("Неверный логин");

            if (user.Password != password)
                throw new AuthenticationException("Неверный пароль");

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, login), //request.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, request.Role.Name)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claims,
                "AddCookies",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return user;
        }

        [Route("Login")]
        [HttpGet]
        public IActionResult Login()
        {           
            return View("Login");
        }

    }
}
