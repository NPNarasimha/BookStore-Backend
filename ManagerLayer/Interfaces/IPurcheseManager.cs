using System;
using System.Collections.Generic;
using System.Text;
using RepositoyLayer.Entity;

namespace ManagerLayer.Interfaces
{
    public interface IPurcheseManager
    {
        public Purchese AddToPurchese(int userId, int cartid);
        public List<Purchese> GetAllPurchese(int userId);
    }
}
