﻿using DevFreela.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return StatusCode(200);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateUserModel createUser)
        {
            return StatusCode(204, (nameof(GetById), new { id = 1 }, createUser));
        }

        [HttpPut("{id}/login")]
        public IActionResult Login(int id, [FromBody] LoginModel login)
        {
            return StatusCode(204, (nameof(GetById), new { id = 1 }, login));
        }
    }
}
