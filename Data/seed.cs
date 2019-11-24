using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public class seed
    {
        //private readonly DataContext _context;
        //public seed(DataContext context)
        //{
        //    _context = context;
        //}


        public static void seedUsers(DataContext context)
        {

            if (!context.Users.Any())
            { 
                var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");

                var users = JsonConvert.DeserializeObject<List<Users>>(userData);
            
                foreach(var user in users)
                {
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHashandSalt("password", out passwordHash, out passwordSalt);
                    user.passwordHash = passwordHash;
                    user.passwordSalt = passwordSalt;
                    user.UserName = user.UserName.ToLower();
                    context.Users.Add(user);
              
            }
                context.SaveChanges();
            }
        }



        private static void CreatePasswordHashandSalt(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                passwordSalt = hmac.Key;
            }
        }
    }
}
