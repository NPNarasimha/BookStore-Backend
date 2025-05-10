using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using RepositoyLayer.Entity;

namespace RepositoyLayer.Interfaces
{
    public interface ICartsRepo
    {
        public Cart AddToCart(int userId, CartModel model);
    }
}
