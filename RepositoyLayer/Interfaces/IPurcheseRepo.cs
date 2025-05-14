using System;
using System.Collections.Generic;
using System.Text;
using RepositoyLayer.Entity;

namespace RepositoyLayer.Interfaces
{
    public interface IPurcheseRepo
    {
        public Purchese AddToPurchese(int userId, int cartid);
        public List<Purchese> GetAllPurchese(int userId);
    }
}
