using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLayer.Models;
using Microsoft.EntityFrameworkCore;
using RepositoyLayer.Context;
using RepositoyLayer.Entity;
using RepositoyLayer.Interfaces;

namespace RepositoyLayer.Services
{
    public class CartsRepo:ICartsRepo
    {
        private readonly BookStoreDBContext context;
        public CartsRepo(BookStoreDBContext context)
        {
            this.context = context;
        }
        public Cart AddToCart(int userId,CartModel model)
        {
            var book = context.Books.FirstOrDefault(x => x.BookId == model.BookId);
            if (book == null || book.Quantity < model.Quantity)
            {
                return null;
            }
            var existingCartItem = context.Carts.FirstOrDefault(c => c.UserId == userId && c.BookId == model.BookId && !c.IsPurchased);
            if (existingCartItem != null)
            {
                //update Exist cart item
                var newQuantity = existingCartItem.Quantity + model.Quantity;
                if (newQuantity > book.Quantity)
                {
                    return null; // Not enough stock
                }
                // Update the quantity in the cart
                existingCartItem.Quantity = newQuantity;
                existingCartItem.Price = newQuantity * book.DiscountPrice;
                context.SaveChanges();
                return existingCartItem;
            }
            else
            {
                var cart = new Cart
                {
                    UserId = userId,
                    BookId = model.BookId,
                    Quantity = model.Quantity,
                    Price = model.Quantity * book.DiscountPrice,
                    IsPurchased = false
                };
                context.Carts.Add(cart);
                context.SaveChanges();
                return cart;
            }
        }

        public List<Cart> GetCarts(int userId)
        {
            var cartList = context.Carts.Include(x => x.Book).Where(x => x.UserId == userId && !x.IsPurchased).ToList();
            if (cartList == null)
            {
                return null;
            }
            return cartList;
        }

        public bool DeleteCart(int userId, int cartId)
        {
            var cartItem = context.Carts.FirstOrDefault(x => x.UserId == userId && x.CartId == cartId);
            if (cartItem != null)
            {
                context.Carts.Remove(cartItem);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        
    }
}
