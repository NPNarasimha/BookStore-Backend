using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using ManagerLayer.Interfaces;
using RepositoyLayer.Entity;
using RepositoyLayer.Interfaces;

namespace ManagerLayer.Services
{
    public class AdminsManager:IAdminsManager
    {
        private readonly IAdminsRepo adminsRepo;
        public AdminsManager(IAdminsRepo adminsRepo)
        {
            this.adminsRepo = adminsRepo;
        }

        public Admin adminRegister(AdminRegisterModel adminRegister)
        {
            return adminsRepo.adminRegister(adminRegister);
        }
        public bool CheckEmail(string email)
        {
            return adminsRepo.CheckEmail(email);
        }

        public string AdminLogin(AdminLoginModel model)
        {
        return adminsRepo.AdminLogin(model);
        }
        public AdminForgotPasswordModel AdminForgotPassword(string email)
        {
            return adminsRepo.AdminForgotPassword(email);
        }
        public bool AdminResetPassword(string email, AdminResetPasswordModel model)
        {
            return adminsRepo.AdminResetPassword(email, model);
        }

    }
}
