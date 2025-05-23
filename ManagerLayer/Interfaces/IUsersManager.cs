﻿using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using RepositoyLayer.Entity;

namespace ManagerLayer.Interfaces
{
    public interface IUsersManager
    {
        public Users RegisterUser(RegisterModel model);
        public bool CheckEmail(string email);
        public LoginGenaratesTokens UserLogin(LoginModel model);
        public ForgetPasswordModel forgetPassword(string email);
        public bool ResetPassword(string email, ResetPasswordModel model);
        public Users GetUserByEmail(string email);
    }
}
