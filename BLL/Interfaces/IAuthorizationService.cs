using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;
using System.Collections.ObjectModel;

namespace BLL.Interfaces
{
    public interface IAuthorizationService
    {
        bool CheckLogin(string login);
        bool CheckPassword(string login, string password);
        int GetUser(string login);
    }
}
