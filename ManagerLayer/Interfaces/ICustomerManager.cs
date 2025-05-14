using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using RepositoyLayer.Entity;

namespace ManagerLayer.Interfaces
{
    public interface ICustomerManager
    {
        public Customer AddCustomer(int userId, CustomerModel model);
        public List<Customer> GetAllCustomers(int userId);
    }
}
