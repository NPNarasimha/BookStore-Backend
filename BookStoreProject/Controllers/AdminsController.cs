using System;
using CommonLayer.Models;
using ManagerLayer.Interfaces;
using ManagerLayer.Services;
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

    }
}
