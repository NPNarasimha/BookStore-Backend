using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using ManagerLayer.Interfaces;
using RepositoyLayer.Entity;
using RepositoyLayer.Interfaces;
using RepositoyLayer.Services;

namespace ManagerLayer.Services
{
    public class UsersManager:IUsersManager
    {
        private readonly IUsersRepo usersRepo;
        public UsersManager(IUsersRepo usersRepo)
        {
            this.usersRepo = usersRepo;
        }
        public Users RegisterUser(RegisterModel model)
        {
            return usersRepo.RegisterUser(model);
        }
        public bool CheckEmail(string email)
        {
            return usersRepo.CheckEmail(email);
        }

        public LoginGenaratesTokens UserLogin(LoginModel model)
        {
            return usersRepo.UserLogin(model);
        }
        public ForgetPasswordModel forgetPassword(string email)
        {
            return usersRepo.forgetPassword(email);
        }
        public bool ResetPassword(string email, ResetPasswordModel model)
        {
            return usersRepo.ResetPassword(email, model);
        }
        public Users GetUserByEmail(string email)
        {
            return usersRepo.GetUserByEmail(email);
        }
    }
}
