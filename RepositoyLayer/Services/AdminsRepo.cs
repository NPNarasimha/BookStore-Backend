using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLayer.Models;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using RepositoyLayer.Context;
using RepositoyLayer.EncodeDecodePass;
using RepositoyLayer.Entity;
using RepositoyLayer.Interfaces;

namespace RepositoyLayer.Services
{
    public class AdminsRepo:IAdminsRepo
    {
        private readonly IConfiguration config;
        private readonly BookStoreDBContext bookStoreDb;

        public AdminsRepo(IConfiguration config, BookStoreDBContext bookStoreDb)
        {
            this.config = config;
            this.bookStoreDb = bookStoreDb;
        }

        public bool CheckEmail(string email)
        {
            var result = this.bookStoreDb.Admins.FirstOrDefault(x => x.Email == email);
            if (result == null)
            {
                return false;
            }
            return true;
        }
        public Admin adminRegister(AdminRegisterModel adminRegister)
        {
            Admin admin = new Admin();
            admin.FirstName = adminRegister.FirstName;
            admin.LastName = adminRegister.LastName;
            admin.Email = adminRegister.Email;
            admin.Password = EncryptionPass.EncodePasswordToBase64(adminRegister.Password);
            this.bookStoreDb.Admins.Add(admin);
            bookStoreDb.SaveChanges();
            return admin;
        }
    }
}
