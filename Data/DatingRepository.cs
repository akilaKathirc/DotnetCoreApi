using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;


namespace WebApplication2.Data
{
    public class DatingRepository : IDatingRepository
    {
        private readonly DataContext _context;

        public DatingRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<IEnumerable<Users>> GetAllUsers()
        {
            var users =await _context.Users.Include(p => p.Photos).ToListAsync();
            return users;
        }

        public async Task<Photos> GetMainPhotoForUser(int ID)
        {
            var PhotosFromRepo = await _context.Photos.Where(u => u.UserId == ID).FirstOrDefaultAsync(u => u.isMain == true);
            return PhotosFromRepo;
        }

        public async Task<Photos> GetPhotos(int ID)
        {
            var PhotosFromRepo = await _context.Photos.FirstOrDefaultAsync(u => u.ID == ID);
            return PhotosFromRepo;
        }

        public async Task<Users> GetUser(int ID)
        {
            var user =await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == ID);
            return user;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
