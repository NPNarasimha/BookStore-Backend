using System;
using CommonLayer.Models;
using ManagerLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreProject.Controllers
{
    [Route("api/wishlist")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly IWishListManager wishListManager;
        public WishListController(IWishListManager wishListManager)
        {
            this.wishListManager = wishListManager;
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddedToWishList(WishListModel model)
        {
            var role = User.FindFirst("custom_role")?.Value;
            if (role != "User")
            {
                return Unauthorized(new ResponseModel<string> { Success = false, Message = "Only Users can add to wishlist." });
            }
            var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
            if (userId == null)
            {
                return Unauthorized(new ResponseModel<string> { Success = false, Message = "Invalid User." });
            }

            var result = wishListManager.AddToWishList(userId, model.BookId);
            if (result == null)
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "Failed to add book to wishlist." });
            }
            return Ok(new ResponseModel<object> { Success = true, Message = "Book added to wishlist successfully.", Data = result });
        }
        [Authorize]
        [HttpDelete("{wishListId}")]
        public IActionResult DeleteWishList(int wishListId)
        {
            var role = User.FindFirst("custom_role")?.Value;
            if (role != "User")
            {
                return Unauthorized(new ResponseModel<string> { Success = false, Message = "Only Users can remove from wishlist." });
            }
            var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
            if (userId == null)
            {
                return Unauthorized(new ResponseModel<string> { Success = false, Message = "Invalid User." });
            }
            var result = wishListManager.RemoveFromWishList(wishListId, userId);
            if (!result)
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "Failed to remove book from wishlist." });
            }
            return Ok(new ResponseModel<string> { Success = true, Message = "Book removed from wishlist successfully." });
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetAllWishList()
        {
            var role = User.FindFirst("custom_role")?.Value;
            if (role != "User")
            {
                return Unauthorized(new ResponseModel<string> { Success = false, Message = "Only Users can view wishlist." });
            }
            var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
            if (userId == null)
            {
                return Unauthorized(new ResponseModel<string> { Success = false, Message = "Invalid User." });
            }
            var result = wishListManager.GetAllWishList(userId);
            if (result == null || result.Count == 0)
            {
                return NotFound(new ResponseModel<string> { Success = false, Message = "No books found in wishlist." });
            }
            return Ok(new ResponseModel<object> { Success = true, Message = "Wishlist retrieved successfully.", Data = result });
        }
    }
}
