using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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

        public string UserLogin(LoginModel model)
        {
            var user=this.BookStoreDb.Users.FirstOrDefault(x => x.Email == model.Email && x.Password == EncryptionPass.EncodePasswordToBase64(model.Password));
            if (user == null)
            {
                return null;
            }
            else
            {
                var token =GenerateToken(user.Email, user.Id);
                return token;
            }
        }

        public ForgetPasswordModel forgetPassword(string email)
        {
            var user = this.BookStoreDb.Users.ToList().Find(user => user.Email == email);
            if (user == null)
            {
                return null;
            }
            else
            {
                var token = GenerateToken(user.Email, user.Id);
                ForgetPasswordModel forgetPasswordModel = new ForgetPasswordModel();
                forgetPasswordModel.Email = email;
                forgetPasswordModel.Token = token;
                return forgetPasswordModel;
            }
        }


        private string GenerateToken(string email, int userId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("EmailId",email),
                new Claim("UserId",userId.ToString())
            };
            var token = new JwtSecurityToken(config["Jwt:Issuer"],
                config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
