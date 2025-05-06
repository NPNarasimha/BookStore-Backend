using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using RepositoyLayer.Entity;

namespace ManagerLayer.Interfaces
{
    public interface IAdminsManager
    {
        public Admin adminRegister(AdminRegisterModel adminRegister);
        public bool CheckEmail(string email);
    }
}
