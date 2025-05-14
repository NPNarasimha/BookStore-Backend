using System;
using System.Collections.Generic;
using System.Text;
using ManagerLayer.Interfaces;
using RepositoyLayer.Interfaces;

namespace ManagerLayer.Services
{
    public class CustomerManager : ICustomerManager
    {
        private readonly ICustomerRepo customerRepo;
        public CustomerManager(ICustomerRepo customerRepo)
        {
            this.customerRepo = customerRepo;
        }
    }
    {
    }
}
