﻿using AutoMapper;
using BlogApp.DLL.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private IArticleRepository articles;
        private IMapper mapper;

        public ArticleController(IArticleRepository articleRepository, IMapper mapper)
        {
            articles = articleRepository;
            this.mapper = mapper;
        }

        // GET: api/<ArticleController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ArticleController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ArticleController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ArticleController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ArticleController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
