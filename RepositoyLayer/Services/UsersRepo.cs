using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoyLayer.Context;
using RepositoyLayer.EncodeDecodePass;
using RepositoyLayer.Entity;
using RepositoyLayer.Interfaces;

namespace RepositoyLayer.Services
{
    public class UsersRepo:IUsersRepo
    {
        private readonly IConfiguration config;
        private readonly BookStoreDBContext BookStoreDb;
        public UsersRepo(IConfiguration config, BookStoreDBContext BookStoreDb)
        {
            this.config = config;
            this.BookStoreDb = BookStoreDb;
        }
        public bool CheckEmail(string email)
        {
            var result = this.BookStoreDb.Users.FirstOrDefault(x => x.Email == email);
            if (result == null)
            {
                return false;
            }
            return true;
        }
        public Users RegisterUser(RegisterModel model)
        {
            Users user = new Users();
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.Password =EncryptionPass.EncodePasswordToBase64(model.Password);
            this.BookStoreDb.Users.Add(user);
            BookStoreDb.SaveChanges();
            return user;
        }


    }
}
