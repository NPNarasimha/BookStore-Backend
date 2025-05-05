using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RepositoyLayer.Entity;

namespace RepositoyLayer.Context
{
    public class BookStoreDBContext : DbContext
    {
        public BookStoreDBContext(DbContextOptions option) : base(option) { }
        public DbSet<Users> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
    }
}
