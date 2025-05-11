using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using RepositoyLayer.Entity;

namespace ManagerLayer.Interfaces
{
    public interface ICartsManager
    {
        public Cart AddToCart(int userId, CartModel model);
        public List<Cart> GetCarts(int userId);
        public bool DeleteCart(int userId, int cartId);
        public Cart UpdateCartItem(int userId, int CartId, CartModel model);
        public int GetCartTotal(int userId);
    }
}
