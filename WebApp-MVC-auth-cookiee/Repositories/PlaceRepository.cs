using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp_MVC_auth_cookiee.Data;
using WebApp_MVC_auth_cookiee.Models;

namespace WebApp_MVC_auth_cookiee.Repositories
{
    public class PlaceRepository : IPlaceRepository
    {
        private readonly AppDbContext _context;

        public PlaceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Place> GetPlaceByIdAsync(int id)
        {
            return await _context.Places.FindAsync(id);
        }

        public async Task<IEnumerable<Place>> GetAllPlacesAsync()
        {
            return await _context.Places.ToListAsync();
        }

        public async Task AddPlaceAsync(Place place)
        {
            await _context.Places.AddAsync(place);
        }

        public async Task UpdatePlaceAsync(Place place)
        {
            _context.Entry(place).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeletePlaceAsync(int id)
        {
            var place = await _context.Places.FindAsync(id);
            if (place != null)
            {
                _context.Places.Remove(place);
                await _context.SaveChangesAsync();
            }
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync(); // Сохраняем изменения в базе данных
        }
    }
}