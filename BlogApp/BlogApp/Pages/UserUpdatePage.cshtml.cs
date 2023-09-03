using AutoMapper;
using BlogApp.BLL.RequestModels;
using BlogApp.DLL.Models;
using BlogApp.DLL.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BlogApp.Pages
{
    public class UserUpdatePageModel : PageModel
    {
        private IUserRepository users;
        private IRoleRepository roles;
        private IMapper mapper;

        public List<CheckRole> CheckRoles { get; set; }

        [Required(ErrorMessage = "���� Nickname ����������� ��� ����������")]
        [DataType(DataType.Text)]
        [Display(Name = "Nickname", Prompt = "Nickname")]
        public string Login { get; set; }

        [Required(ErrorMessage = "���� Email ����������� ��� ����������")]
        [EmailAddress]
        [Display(Name = "Email", Prompt = "Email@mail.ru")]
        public string Email { get; set; }

        [Required(ErrorMessage = "���� ������ ����������� ��� ����������")]
        [DataType(DataType.Password)]
        [Display(Name = "������", Prompt = "**********")]
        public string Password { get; set; }

        [Required(ErrorMessage = "����������� ����������� ������")]
        [Compare("Password", ErrorMessage = "������ �� ���������")]
        [DataType(DataType.Password)]
        [Display(Name = "����������� ������", Prompt = "**********")]
        public string PasswordConfirm { get; set; }

        [Required(ErrorMessage = "���� ��� ����������� ��� ����������")]
        [DataType(DataType.Text)]
        [Display(Name = "���", Prompt = "���")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "���� ������� ����������� ��� ����������")]
        [DataType(DataType.Text)]
        [Display(Name = "�������", Prompt = "�������")]
        public string LastName { get; set; }


        public List<RoleReqest> Roles { get; set; }

        public UserUpdatePageModel(IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper)
        {
            users = userRepository;
            roles = roleRepository;
            this.mapper = mapper;
        }

        public void OnGet()
        {
            CheckRoles = new List<CheckRole>();
            var allRoles = roles.GetAll().Result;

            foreach (var existTag in allRoles)
            {
                var tmp = new CheckRole();
                tmp.RememberMe = false;
                tmp.roleName = existTag.Name;
                CheckRoles.Add(tmp);
            }
        }

        public void OnPost()
        {

        }
    }
    public class CheckRole
    {
        public bool RememberMe { get; set; } = false;
        public string roleName { get; set; }
        public CheckRole(bool rememberMe, Role role)
        {
            RememberMe = rememberMe;
            this.roleName = role.Name;
        }

        public CheckRole() { }
    }
}

