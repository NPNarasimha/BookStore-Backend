using System;
using System.Collections.Generic;
using System.Text;
using RepositoyLayer.Entity;

namespace ManagerLayer.Interfaces
{
    public interface IWishListManager
    {
        public WishList AddToWishList(int userId, int bookId);
        public bool RemoveFromWishList(int wishListId, int userId);
    }
}
