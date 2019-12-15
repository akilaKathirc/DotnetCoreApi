using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public interface IDatingRepository
    {
      void Add<T>(T entity) where T : class;
      void Delete<T>(T entity) where T : class;
      Task<bool> SaveAll();
      Task<IEnumerable<Users>> GetAllUsers();
     Task<Users> GetUser(int ID);

        Task<Photos> GetPhotos(int ID);

        Task<Photos> GetMainPhotoForUser(int ID);
    }
}
