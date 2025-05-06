using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using CommonLayer.Models;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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

        public string AdminLogin(AdminLoginModel model)
        {
            var admin = this.bookStoreDb.Admins.FirstOrDefault(admin => admin.Email == model.Email && admin.Password == EncryptionPass.EncodePasswordToBase64(model.Password));
            if (admin == null)
            {
                return null;
            }
            else
            {
                var token = GenerateToken(admin.Email, admin.Id, admin.Role);
                return token;
            }
        }

        public AdminForgotPasswordModel AdminForgotPassword(string email)
        {
            var admin = this.bookStoreDb.Admins.ToList().Find(admin => admin.Email == email);
            if (admin == null)
            {
                return null;
            }
            else
            {
                var token = GenerateToken(admin.Email, admin.Id, admin.Role);
                AdminForgotPasswordModel adminForgotPasswordModel = new AdminForgotPasswordModel();
                adminForgotPasswordModel.Email = email;
                adminForgotPasswordModel.Token = token;
                return adminForgotPasswordModel;
            }
        }
        private string GenerateToken(string email, int userId, string Role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("EmailId",email),
                new Claim("UserId",userId.ToString()),
                new Claim("custom_role", Role)
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
