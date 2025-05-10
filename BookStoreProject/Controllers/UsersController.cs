using System;
using System.Linq.Expressions;
using CommonLayer.Models;
using ManagerLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoyLayer.Entity;

namespace BookStoreProject.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersManager usersManager;
        public UsersController(IUsersManager usersManager)
        {
            this.usersManager = usersManager;
        }

        [HttpPost("register")]
        public IActionResult RegisterUser(RegisterModel model)
        {
            try
            {
                var result = usersManager.CheckEmail(model.Email);
                if (result == null)
                {
                    return BadRequest(new ResponseModel<Users> { Success = false, Message = "Email Already Existed" });
                }
                else
                {
                    var user = usersManager.RegisterUser(model);
                    if (user != null)
                    {
                        return Ok(new ResponseModel<Users> { Success = true, Message = "User Registered Successfully", Data = user });
                    }
                    else
                    {
                        return BadRequest(new ResponseModel<Users> { Success = false, Message = "User Registration Failed" });
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<string>
                { Success = false, Message = "An internal error occurred", Data = ex.Message });
            }
        }

        [HttpPost("login")]
        public IActionResult Login(LoginModel model)
        {
            var result = usersManager.UserLogin(model);
            if (result == null)
            {
                return Unauthorized(new ResponseModel<string>
                { Success = false, Message = "Invalid email or password" });
            }
            return Ok(new ResponseModel<LoginGenaratesTokens> { Success = true, Message = model.Email+"Login Success", Data = result });
        }
        [HttpPost("forgetpassword")]
        public IActionResult ForgetPassword(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return BadRequest(new ResponseModel<string> { Success = false, Message = "Email is required" });
            ForgetPasswordModel result =usersManager.forgetPassword(email);
            
            if (result == null)
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "Email not existed Please Enter valid E-mail" });
            }
            else
            {
                SendEmail send=new SendEmail();
                send.EmailSend(result.Email, result.Token);
                return Ok(new ResponseModel<string> { Success = true, Message = "Reset Password link Sent to Email" });
            }
        }
        [Authorize]
        [HttpPost("resetpassword")]
        public IActionResult ResetPassword(ResetPasswordModel model)
        {
            string Email = User.FindFirst("EmailId").Value;
            if (usersManager.ResetPassword(Email, model))
            {
                return Ok(new ResponseModel<bool> { Success = true, Message = Email + "Password changed Success" });
            }
           return BadRequest(new ResponseModel<bool> { Success = false, Message = "Password Not Changed" });
        }

        [HttpGet("getuser")]
        public IActionResult GetUserByEmail(string email)
        {
            var result = usersManager.GetUserByEmail(email);
            if (result == null)
            {
                return Unauthorized(new ResponseModel<string> { Success = false, Message = "Invalid email or password" });
            }
            else
            {
                return Ok(new ResponseModel<Users> { Success = true, Message = "User Found", Data = result });
            }
        }
    }
}
            
