using System;
using System.Collections.Generic;
using System.Text;
using RepositoyLayer.Context;
using RepositoyLayer.Interfaces;

namespace RepositoyLayer.Services
{
    public class WishListRepo : IWishListRepo
    {
        private readonly BookStoreDBContext context;
        public WishListRepo(BookStoreDBContext context)
        {
            this.context = context;
        }
    }
}
