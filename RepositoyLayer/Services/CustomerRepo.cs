using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using RepositoyLayer.Context;
using RepositoyLayer.Entity;
using RepositoyLayer.Interfaces;

namespace RepositoyLayer.Services
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly BookStoreDBContext context;
        public CustomerRepo(BookStoreDBContext context)
        {
            this.context = context;
        }
        public Customer AddCustomer(int userId, CustomerModel model)
        {
                var user = context.Users.FirstOrDefault(u => u.Id == userId);
                if (user == null) {
                    return null;
                }
                var customer = new Customer()
                {
                    FullName = model.FullName,
                    MobileNumber = model.MobileNumber,
                    Address = model.Address,
                    City = model.City,
                    State = model.State,
                    Type = model.Type,
                    UserId = userId
                };
                context.Customers.Add(customer);
                context.SaveChanges();
                return customer;
                                  
        }
        public List<Customer> GetAllCustomers(int userId)
        {
            var customers = context.Customers.Include(c => c.User).Where(c => c.UserId == userId).ToList();
            return customers;

        }

    }
    
}
