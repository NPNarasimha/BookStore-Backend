using System;
using System.Collections.Generic;
using System.Text;
using RepositoyLayer.Entity;

namespace RepositoyLayer.Interfaces
{
    public interface IWishListRepo
    {
        public WishList AddToWishList(int userId, int bookId);
        public bool RemoveFromWishList(int wishListId, int userId);
    }
}
