using AutoMapper;
using BlogApp.BLL.RequestModels;
using BlogApp.BLL.Views;
using BlogApp.DLL.Models;
using BlogApp.DLL.Repository.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;
using System.Security.Claims;
using BlogAppAPI.Controllers;
using NuGet.Protocol.Plugins;
using System.Xml.Linq;

namespace BlogApp.Controllers
{
    public class AccountController : Controller
    {
        private IUserRepository users;
        private IRoleRepository roles;
        private IMapper mapper;
        private UserController userController;

        public AccountController(IUserRepository userRepository, IRoleRepository role, IMapper mapper, UserController userController)
        {
            users = userRepository;
            roles = role;
            this.mapper = mapper;
            this.userController = userController;
        }

        public IActionResult Index()
        {
            return View();
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

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user = await users.GetByLogin(model.Login);

                if (user == null)
                return RedirectToPage("/LoginPage");

            UserRequest request = new UserRequest();

            request.Role = new RoleReqest(Guid.Empty ,"User");

            if(user.Roles.Where(x => x.Name == "Admin") != null)
            {
                request.Role.Name = "Admin";
            }
            else if (user.Roles.Where(x => x.Name == "Moderator") != null)
            {
                request.Role.Name = "Moderator";
            }

            var result =Authenticate(request, model.Login, model.Password);

            return RedirectToPage("/Index");
        }

        [HttpPost]
        [Route("Registration")]
        public async Task<IActionResult> Registration(RegistrationViewModel model)
        {
            var user = await users.GetByLogin(model.Login);

            if (user != null)
                return RedirectToPage("/RegistrationPage");

            UserRequest request = new UserRequest();
            request.Role = new RoleReqest(Guid.Empty, "User");
            request.FirstName = model.FirstName;
            request.LastName = model.LastName;
            request.Email = model.Email;    
            request.Password = model.Password;
            request.Login = model.Login;

            Guid guid = Guid.NewGuid();
            if (await users.Get(guid) == null)
            {
                var newUser = mapper.Map<UserRequest, User>(request);
                newUser.Id = guid;
                await users.Create(newUser);
            }


                return RedirectToPage("/Index");
        }


    }
}
