using System;
using System.Collections.Generic;
using System.Text;
using RepositoyLayer.Context;
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

    }
    
}
