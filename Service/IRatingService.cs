using Entities.Models;

namespace Services
{
    public interface IRatingService
    {
        Task AddRating(Rating rating);
    }
}