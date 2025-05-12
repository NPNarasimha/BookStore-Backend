using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RepositoyLayer.Context;
using RepositoyLayer.Entity;
using RepositoyLayer.Interfaces;

namespace RepositoyLayer.Services
{
    public class WishListRepo : IWishListRepo
    {
        private readonly BookStoreDBContext context;
        public WishListRepo(BookStoreDBContext context)
        {
            this.context = context;
        }
        public WishList AddToWishList(int userId, int bookId)
        {
            var wishList = new WishList()
            {
                userId = userId,
                BookId = bookId
            };
            context.WishLists.Add(wishList);
            context.SaveChanges();
            var result = context.WishLists.Include(w => w.Book).FirstOrDefault(w => w.WishListId == wishList.WishListId);
            return result;
        }

    }
}
