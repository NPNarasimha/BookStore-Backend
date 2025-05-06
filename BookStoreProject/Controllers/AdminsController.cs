using System;
using CommonLayer.Models;
using ManagerLayer.Interfaces;
using ManagerLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoyLayer.Entity;

namespace BookStoreProject.Controllers
{
    [Route("")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly IAdminsManager adminsManager;
        public AdminsController(IAdminsManager adminsManager)
        {
            this.adminsManager = adminsManager;
        }

        [HttpPost("adminregister")]
        public IActionResult adminRegister(AdminRegisterModel model)
        {
            try
            {

                if (model == null)
                    return BadRequest(new ResponseModel<string>
                    { Success = false, Message = "Invalid registration details provided" });

                if (adminsManager.CheckEmail(model.Email))
                    return BadRequest(new ResponseModel<string>
                    { Success = false, Message = "Email already exists" });

                var result = adminsManager.adminRegister(model);

                if (result != null)
                {
                    return Ok(new ResponseModel<Admin>
                    { Success = true, Message = "Registered successfully", Data = result });
                }

                return BadRequest(new ResponseModel<Admin>
                { Success = false, Message = "Registration failed" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<string>
                { Success = false, Message = "An internal error occurred", Data = ex.Message });
            }
        }
        [HttpPost("adminlogin")]
        public IActionResult AdminLogin(AdminLoginModel model)
        {
            try
            {

                var result = adminsManager.AdminLogin(model);
                if (result == null)
                {
                    return BadRequest(new ResponseModel<string> { Success = false, Message = "Invalid Credentials" });
                }
                return Ok(new ResponseModel<string> { Success = true, Message = "Login successfull", Data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<string>
                { Success = false, Message = "An internal error occurred", Data = ex.Message });

            }
        }

        [HttpPost("adminforgotpassword")]
        public IActionResult ForgetPassword(string email)
        {
            try
            {

                var result = adminsManager.AdminForgotPassword(email);
                if (result == null)
                {
                    return BadRequest(new ResponseModel<string> { Success = false, Message = "Email Not Found" });
                }
                else
                {
                    SendEmail send = new SendEmail();
                    send.EmailSend(result.Email, result.Token);
                    return Ok(new ResponseModel<string> { Success = true, Message = "Reset link Sent to Email" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<string>
                { Success = false, Message = "An internal error occurred", Data = ex.Message });
            }
        }
        [Authorize]
        [HttpPost("adminresetpassword")]

        public IActionResult AdminResetPassword(AdminResetPasswordModel model)
        {
            try
            {
                string Email = User.FindFirst("EmailId").Value;
                if (adminsManager.AdminResetPassword(Email, model))
                {
                    return Ok(new ResponseModel<string> { Success = true, Message = "Password reset successfully" });
                }
                else
                {
                    return BadRequest(new ResponseModel<string> { Success = false, Message = "Password reset failed" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<string>
                { Success = false, Message = "An internal error occurred", Data = ex.Message });
            }
        }
    }
}
