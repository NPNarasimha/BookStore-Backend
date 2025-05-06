using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using RepositoyLayer.Entity;

namespace RepositoyLayer.Interfaces
{
    public interface IAdminsRepo
    {
        public Admin adminRegister(AdminRegisterModel adminRegister);
        public bool CheckEmail(string email);
        public string AdminLogin(AdminLoginModel model);

    }
}
