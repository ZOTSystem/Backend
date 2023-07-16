﻿using be.DTOs;
using be.Models;
using be.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace be.Controllers
{
    [Route("api/home")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        private readonly DbZotsystemContext _db;

        public HomeController(DbZotsystemContext db, IUserService userService, IConfiguration configuration)
        {
            this._db = db;
            _userService = userService;
            _configuration = configuration;
        }

        #region HUY- LOGIN/REGISTER/FORGOR PASSWORD/GETINFOR/CONFIRM ACCOUNT/ UPDATE USER/ CHANGE PASSWORD
        [HttpPost("login")]
        public async Task<ActionResult> Login(Login login)
        {
            try
            {
                var result = _userService.Login(login.Email, login.Password, _configuration);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("info")]
        public async Task<ActionResult> GetInfo(string token)
        {
            try
            {
                if(token == "")
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

        [HttpPost("register")]
        public async Task<ActionResult> Register(Register register)
        {
            try
            {
                var result = _userService.Register(register);
                return Ok(new
                {
                    message = "Add user success!",
                    status = 200,
                    data = result
                });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("confirm")]
        public async Task<ActionResult> ConfirmAccount(string email)
        {
            try
            {
                var _user = _db.Accounts.SingleOrDefault(x => x.Email.Equals(email));
                if (_user == null)
                {
                    return NotFound();
                }
                _user.Status = "Đang hoạt động";
                //_db.Entry(_db.Accounts.FirstOrDefaultAsync(x => x.Email == email)).CurrentValues.SetValues(_user);
                _db.SaveChanges();
                return Ok(new
                {
                    status = 200,
                    message = "Confirm success"
                });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("getAllPhone")]
        public async Task<ActionResult> GetAllPhone()
        {
            var result = (from account in _db.Accounts
                         select account.Phone).ToList();    
            if(result == null)
            {
                return Ok(new
                {
                    message = "No Data",
                    status = 400,
                });
            }
            return Ok(result);
        }

        [HttpPost("forgotPassword")]
        public async Task<ActionResult> ChangePassword(string email)
        {
            try
            {
                var result = _userService.ForgotPassword(email);
                return Ok(result);
            } catch
            {
                return BadRequest();
            }
        }

        [HttpPost("loginByGoogle")]
        public async Task<ActionResult> LoginByGoogle(Register register)
        {
            try
            {
                var result = _userService.LoginByGoogle(register, _configuration);
                return Ok(result);
            } catch 
            { 
                return  BadRequest();
            }   
        }

        [HttpPost("updateUser")]
        public async Task<ActionResult> UpdateUser (UpdateUser user)
        {
            try
            {
                var result = _userService.UpdateUser(user);
                return Ok(result);
            } catch { return BadRequest(); }    
        }

        [HttpPost("changePassword")]
        public async Task<ActionResult> ChangePassword (int accountId, string newPassword)
        {
            try
            {
                var result = _userService.ChangePassword(accountId, newPassword);
                return Ok(result);
            } catch
            {
                return BadRequest();
            }
        }

        [HttpPost("changeAvatar")]
        public async Task<ActionResult> ChangeAvatar(int accountId, string newAvatar)
        {
            try
            {
                var result = _userService.ChangeAvatar(accountId, newAvatar);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult> SearchInforByEmail (string email)
        {
            try
            {
                var result = _userService.SearchInforByEmail(email);
                return Ok(result);
            } catch
            {
                return BadRequest();
            }
        }
        #endregion



    }
}
