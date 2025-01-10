
using WebApp_MVC_auth_cookiee.Models;

namespace WebApp_MVC_auth_cookiee.Repositories
{
    public interface IPlaceRepository
    {
        Task<Place> GetPlaceByIdAsync(int id);
        Task<IEnumerable<Place>> GetAllPlacesAsync();
        Task AddPlaceAsync(Place place);
        Task UpdatePlaceAsync(Place place);
        Task DeletePlaceAsync(int id);
        Task SaveChangesAsync();
    }
}