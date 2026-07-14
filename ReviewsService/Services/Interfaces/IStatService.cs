using ReviewsService.Models.Dtos;

namespace ReviewsService.Services.Interfaces;

public interface IStatService
{
    public Task<Rating> GetRating(int productId);
}