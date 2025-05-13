using System;
using System.Collections.Generic;
using System.Text;
using ManagerLayer.Interfaces;
using RepositoyLayer.Entity;
using RepositoyLayer.Interfaces;

namespace ManagerLayer.Services
{
    public class PurcheseManager : IPurcheseManager
    {
        private readonly IPurcheseRepo purcheseRepo;
        public PurcheseManager(IPurcheseRepo purcheseRepo)
        {
            this.purcheseRepo = purcheseRepo;
        }
        public Purchese AddToPurchese(int userId, int cartid)
        {
            return purcheseRepo.AddToPurchese(userId, cartid);
        }
    }
}
