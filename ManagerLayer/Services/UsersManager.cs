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



    }
}
