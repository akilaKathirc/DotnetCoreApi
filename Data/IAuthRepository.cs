using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public interface IAuthRepository
    {
        Task<Users> Register(Users user, string password);

        Task<Users> Login(string name, string password);

        Task<bool> UserExists(string name);
    }
}
