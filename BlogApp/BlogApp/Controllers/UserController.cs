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
using BlogApp.Views;
using System.Reflection.Metadata.Ecma335;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogAppAPI.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private IUserRepository users;
        private IRoleRepository roles;
        private IMapper mapper;
        private readonly IHttpContextAccessor _http;

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

        [HttpGet]
        [Route("GetByLogin")]
        public async Task<IActionResult> GetByLogin(string login)
        {
            var user = await users.GetByLogin(login);
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

        [Route("CreateUser")]
        public IActionResult CreateUser()
        {
            return RedirectToPage("/RegistrationPage");
        }

        [HttpPost]
        [Route("UserUpdate")]
        public async Task<IActionResult> UserUpdate(UserUpdateViewModel request)
        {
            try
            {
                Console.WriteLine("UserUpdate");
                List<Role> requastRoles = new List<Role>();

                var allRoles = await roles.GetAll();

                foreach (var c in request.CheckRoles)
                {
                    var tmp = allRoles.FirstOrDefault(x => x.Name == c.roleName & c.RememberMe);
                    if (tmp != null)
                        requastRoles.Add(tmp);
                }

                var user =await users.GetByLogin(request.Login);

                user.Roles = requastRoles;
                user.Email = request.Email;
                user.Password = request.Password;
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;

                users.Update(user);
            }
            catch { Console.WriteLine("UserUpdate : Error"); }

            return RedirectToPage("/Index");
        }

        [Route("GetUserToUpdate/{id?}")]
        public IActionResult GetUserToUpdate(RegistrationViewModel model, [FromRoute] Guid ID)
        {
            //return RedirectToRoute("TagUpdatePage", new {id=Id});
            return RedirectToPage("/UserUpdatePage", new { id = ID.ToString() });
        }

        [HttpGet]
        [Route("GetCurrentUser")]
        public string GetCurrentUser()
        {
            return _http.HttpContext.User.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
        }
    }
}
