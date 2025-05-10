using System;
using CommonLayer.Models;
using ManagerLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreProject.Controllers
{
    [Route("api/carts")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartsManager cartsManager;
        public CartsController(ICartsManager cartsManager)
        {
            this.cartsManager = cartsManager;
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddToCart(CartModel model)
        {
            try
            {
                var role = User.FindFirst("custom_role")?.Value;
                if (role != "User")
                {
                    return Unauthorized(new ResponseModel<string> { Success = false, Message = "Only Users can add to cart." });
                }
                int userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
                if (userId == null)
                {
                    return Unauthorized(new ResponseModel<string> { Success = false, Message = "User ID not found." });
                }
                var result = cartsManager.AddToCart(userId, model);
                if (result == null)
                {
                    return BadRequest(new ResponseModel<string> { Success = false, Message = "Book not found or insufficient stock." });
                }
                return Ok(new ResponseModel<object> { Success = true, Message = "Book added to cart successfully.", Data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<string> { Success = false, Message = ex.Message });
            }
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetCarts()
        {
            try
            {
                var role = User.FindFirst("custom_role")?.Value;
                if (role != "User")
                {
                    return Unauthorized(new ResponseModel<string> { Success = false, Message = "Only Users can view cart." });
                }
                int userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
                if (userId == null)
                {
                    return Unauthorized(new ResponseModel<string> { Success = false, Message = "User ID not found." });
                }
                var result = cartsManager.GetCarts(userId);
                if (result == null || result.Count == 0)
                {
                    return NotFound(new ResponseModel<string> { Success = false, Message = "No items in cart." });
                }
                return Ok(new ResponseModel<object> { Success = true, Message = "Cart retrieved successfully.", Data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<string> { Success = false, Message = ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("{cartId}")]
        public IActionResult DeleteCart(int cartId)
        {
            try
            {
                var role = User.FindFirst("custom_role")?.Value;
                if (role != "User")
                {
                    return Unauthorized(new ResponseModel<string> { Success = false, Message = "Only Users can delete from cart." });
                }
                var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
                if (userId == null)
                {
                    return Unauthorized(new ResponseModel<string> { Success = false, Message = "User ID not found." });
                }
                var result = cartsManager.DeleteCart(userId, cartId);
                if (!result)
                {
                    return NotFound(new ResponseModel<string> { Success = false, Message = "Cart item not found." });
                }
                return Ok(new ResponseModel<string> { Success = true, Message = "Cart item deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<string> { Success = false, Message = ex.Message });
            }
        }
        

    }
}
