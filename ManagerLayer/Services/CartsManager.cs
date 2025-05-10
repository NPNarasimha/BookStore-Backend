using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using ManagerLayer.Interfaces;
using RepositoyLayer.Entity;
using RepositoyLayer.Interfaces;

namespace ManagerLayer.Services
{
    public class CartsManager: ICartsManager
    {
        private readonly ICartsRepo cartsRepo;
        public CartsManager(ICartsRepo cartsRepo)
        {
            this.cartsRepo = cartsRepo;
        }
        public Cart AddToCart(int userId, CartModel model)
        {
            return cartsRepo.AddToCart(userId, model);
        }
        public List<Cart> GetCarts(int userId)
        {
            return cartsRepo.GetCarts(userId);
        }
        public bool DeleteCart(int userId, int cartId)
        {
            return cartsRepo.DeleteCart(userId, cartId);
        }
        
    }
}
