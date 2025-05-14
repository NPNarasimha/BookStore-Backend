using System;
using CommonLayer.Models;
using ManagerLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreProject.Controllers
{
    [Route("api/Customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerManager customerManager;
        public CustomerController(ICustomerManager customerManager)
        {
            this.customerManager = customerManager;
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddCustomer(CustomerModel model)
        {
            try
            {
                var role = User.FindFirst("custom_role")?.Value;
                if (role != "User")
                {
                    return Unauthorized(new ResponseModel<string> { Success = false, Message = "Only Users can add to UserDetails." });
                }
                var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
                if (userId == null)
                {
                    return Unauthorized(new ResponseModel<string> { Success = false, Message = "Invalid User." });
                }
                var result = customerManager.AddCustomer(userId, model);
                if (result == null)
                {
                    return BadRequest(new ResponseModel<string> { Success = false, Message = "Failed to add UserDetails." });
                }
                return Ok(new ResponseModel<object> { Success = true, Message = "UserDetails added successfully.", Data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<string> { Success = false, Message = ex.Message });
            }
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            try
            {
                var role = User.FindFirst("custom_role")?.Value;
                if (role != "User")
                {
                    return Unauthorized(new ResponseModel<string> { Success = false, Message = "Only Users can view UserDetails." });
                }
                var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
                if (userId == null)
                {
                    return Unauthorized(new ResponseModel<string> { Success = false, Message = "Invalid User." });
                }
                var result = customerManager.GetAllCustomers(userId);
                if (result == null || result.Count == 0)
                {
                    return NotFound(new ResponseModel<string> { Success = false, Message = "No UserDetails found." });
                }
                return Ok(new ResponseModel<object> { Success = true, Message = "UserDetails retrieved successfully.", Data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<string> { Success = false, Message = ex.Message });
            }
        }
    }
}
