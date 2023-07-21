﻿using be.DTOs;
using be.Models;
using be.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace be.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        private readonly DbZotsystemContext _db;

        public UserController(DbZotsystemContext db, IUserService userService, IConfiguration configuration)
        {
            this._db = db;
            _userService = userService;
            _configuration = configuration;
        }

        [HttpGet("info")]
        public async Task<ActionResult> GetInfo(string token)
        {
            try
            {
                if (token == "")
                {
                    return BadRequest();
                }
                return Ok(_userService.GetInfo(token));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("changePassword")]
        public async Task<ActionResult> ChangePassword(int accountId, string newPassword)
        {
            try
            {
                var result = _userService.ChangePassword(accountId, newPassword);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("updateUser")]
        public async Task<ActionResult> UpdateUser(UpdateUser user)
        {
            try
            {
                var result = _userService.UpdateUser(user);
                return Ok(result);
            }
            catch { return BadRequest(); }
        }

        [HttpGet("getAllAccountUser")]
        public async Task<ActionResult> GetAllAccountUser()
        {
            try
            {
                var result = _userService.GetAllAccountUser();
                return Ok(result);
            } catch { return BadRequest(); }
        }
    }
}
