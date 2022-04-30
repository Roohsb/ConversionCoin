using Conversion.Class;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Conversion.Services
{
    public class UsersService
    {
        private readonly UsersRepository _usersrepository;

        public UsersService(UsersRepository userrepository)
            {
            _usersrepository = userrepository;
        }

        public Boolean Check(Users users) //Check user existence
        {

            Func<Users, bool> func;

            func = (ms => ms.IDUser == users.IDUser);

            var verifica = _usersrepository.GetAll(func);

            if (verifica.ToList().Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
