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
    public class PurcheseRepo : IPurcheseRepo
    {
        private readonly BookStoreDBContext context;
        public PurcheseRepo(BookStoreDBContext context)
        {
            this.context = context;
        }
        public Purchese AddToPurchese(int userId, int cartid)
        {
            var cart = context.Carts.Include(c => c.Book).FirstOrDefault(c => c.CartId == cartid && c.UserId == userId && !c.IsPurchased);
            if (cart == null || cart.Book == null || cart.Book.Quantity < cart.Quantity)
            {
                return null;
            }
            var purchese = new Purchese
            {
                UserId = userId,
                BookId = cart.BookId,
                Quantity = cart.Quantity,
                TotalPrice = cart.Quantity * cart.Book.DiscountPrice,
                PurcheseDate = DateTime.Now
            };
            cart.Book.Quantity -= cart.Quantity;
            cart.IsPurchased = true;
            context.Carts.Remove(cart);
            context.Purcheses.Add(purchese);
            context.SaveChanges();
            return purchese;
        }
    }
}
