using System;
using System.Collections.Generic;
using System.Text;
using ManagerLayer.Interfaces;
using RepositoyLayer.Entity;
using RepositoyLayer.Interfaces;

namespace ManagerLayer.Services
{
    public class WishListManager:IWishListManager
    {
        private readonly IWishListRepo wishListRepo;
        public WishListManager(IWishListRepo wishListRepo)
        {
            this.wishListRepo = wishListRepo;
        }
        public WishList AddToWishList(int userId, int bookId)
        {
            return wishListRepo.AddToWishList(userId, bookId);
        }
        public bool RemoveFromWishList(int wishListId, int userId)
        {
            return wishListRepo.RemoveFromWishList(wishListId, userId);
        }
    }
}
