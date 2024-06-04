using Entities.Models;
using Repositories;


namespace Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;

        public RatingService(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public async Task AddRating(Rating rating)
        {
            
            await _ratingRepository.AddRating(rating);
            return;
        }
    }
}
