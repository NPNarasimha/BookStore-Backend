using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using ManagerLayer.Interfaces;
using RepositoyLayer.Entity;
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
        public Customer AddCustomer(int userId, CustomerModel model)
        {
            return customerRepo.AddCustomer(userId, model);

        }
        public List<Customer> GetAllCustomers(int userId)
        {
            return customerRepo.GetAllCustomers(userId);
        }
    }
    
}
