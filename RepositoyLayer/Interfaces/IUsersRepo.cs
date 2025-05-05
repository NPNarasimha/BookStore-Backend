using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using RepositoyLayer.Entity;

namespace RepositoyLayer.Interfaces
{
    public interface IUsersRepo
    {
        public Users RegisterUser(RegisterModel model);
        public bool CheckEmail(string email);
    }
}
