using Microsoft.EntityFrameworkCore;
using System;
using WebApp_MVC_auth_cookiee.Data;
using WebApp_MVC_auth_cookiee.Models;

namespace WebApp_MVC_auth_cookiee.Repositories
{
    public class UserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByCredentials(string username, string password)
        {
            return await _context.Users
                .Include(u => u.Role) // Убедитесь, что у вас есть using Microsoft.EntityFrameworkCore;
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }
        public async Task Add(User user)
        {
            user.RoleId = 2; // Убедитесь, что это соответствует роли "пользователь" в вашей базе данных
            await _context.Users.AddAsync(user);
        }

        // Метод для сохранения изменений в базе данных
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
