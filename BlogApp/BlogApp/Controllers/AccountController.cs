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
        public IActionResult Login(LoginViewModel model)
        {
            Console.WriteLine(model.Login);



            return RedirectToPage("/Index");
        }

        [HttpPost]
        [Route("Registration")]
        public IActionResult Registration(RegistrationViewModel model)
        {
            Console.WriteLine(model.Login);

            return RedirectToPage("/Index");
        }


    }
}
