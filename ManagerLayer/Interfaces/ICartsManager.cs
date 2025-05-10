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
    }
}
