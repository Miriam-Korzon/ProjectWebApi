using Entities.Models;

namespace Repositories
{
    public interface IRatingRepository
    {
        Task AddRating(Rating rating);
    }
}