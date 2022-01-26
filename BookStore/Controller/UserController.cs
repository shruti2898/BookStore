using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreManager.Interface;
using BookStoreModels.UserModel;

namespace BookStore.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserManager manager;
        public UserController(IUserManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationModel user)
        {
            try
            {
                UserRegistrationModel data = await this.manager.Register(user);
                if (data != null)
                {
                    return this.Ok(new { Status = true, Message = "Registered User Succesfully", Data = data });
                }
                else
                { 
                    return this.BadRequest(new { Status = false, Message = "Email already exist" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserCredentialsModel user)
         {
            try
            {
                var result = await this.manager.Login(user);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Logged in Succesfully", Token = result});
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Email does not exist in our system"});
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        [HttpPost]
        [Route("forgotPassword")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            try
            {
                var result = await this.manager.ForgotPassword(email);
                if (result)
                {
                    return this.Ok(new { Status = true, Message = "Link for reset password has been sent on your email"});
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Email does not exist in our system" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        [HttpPut]
        [Route("resetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] UserCredentialsModel user)
        {
            try
            {
                var result = await this.manager.ResetPassword(user);
                if (result)
                {
                    return this.Ok(new { Status = true, Message = "Password changed successfully" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Email does not exist in our system" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }
    }
}
