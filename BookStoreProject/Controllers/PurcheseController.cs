using System;
using CommonLayer.Models;
using ManagerLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreProject.Controllers
{
    [Route("api/purchese")]
    [ApiController]
    public class PurcheseController : ControllerBase
    {
        private readonly IPurcheseManager purcheseManager;
        public PurcheseController(IPurcheseManager purcheseManager)
        {
            this.purcheseManager = purcheseManager;
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddToPurchese(int cartdId)
        {
            var role = User.FindFirst("custom_role")?.Value;
            if (role != "User")
            {
                return Unauthorized(new ResponseModel<string> { Success = false, Message = "Only Users can purchase." });
            }
            var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
            if (userId == null)
            {
                return Unauthorized(new ResponseModel<string> { Success = false, Message = "Invalid User." });
            }
            var result = purcheseManager.AddToPurchese(userId, cartdId);
            if (result == null)
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "Add the cart Item First After Purchese it." });
            }
            return Ok(new ResponseModel<object> { Success = true, Message = "Book purchased successfully.", Data = result });
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetAllPurchese()
        {
            var role = User.FindFirst("custom_role")?.Value;
            if (role != "User")
            {
                return Unauthorized(new ResponseModel<string> { Success = false, Message = "Only Users can purchase." });
            }
            var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
            if (userId == null)
            {
                return Unauthorized(new ResponseModel<string> { Success = false, Message = "Invalid User." });
            }
            var result = purcheseManager.GetAllPurchese(userId);
            if (result == null)
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "No Purchese found" });
            }
            return Ok(new ResponseModel<object> { Success = true, Message = "Purchese retrieved successfully.", Data = result });
        }

    }
}
