﻿using AutoMapper;
using BlogApp.BLL.RequestModels;
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
            if (request.Id.ToString() == "" || await users.Get(request.Id) == null)
            {
                var newUser = mapper.Map<UserRequest, User>(request);
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
    }
}
