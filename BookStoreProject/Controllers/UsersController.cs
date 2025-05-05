using System;
using System.Linq.Expressions;
using CommonLayer.Models;
using ManagerLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoyLayer.Entity;

namespace BookStoreProject.Controllers
{
    [Route("")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersManager usersManager;
        public UsersController(IUsersManager usersManager)
        {
            this.usersManager = usersManager;
        }

        [HttpPost("Register")]
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
    }
}
            
