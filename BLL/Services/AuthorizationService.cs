using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using BLL.Models;
using BLL.Interfaces;
using System.Collections.ObjectModel;

namespace BLL.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        IDbRepos db;
        public AuthorizationService(IDbRepos dbRepos)
        {
            db = dbRepos;
        }

        
        public int GetUser(string login)
        {
            return db.AuthorizationService.GetUserId(login);
        }
        public bool CheckLogin(string login)
        {
            return db.AuthorizationService.CheckLogin(login);
        }

        public bool CheckPassword(string login, string password)
        {
            return db.AuthorizationService.CheckPassword(login, password);
        }
    }
}
