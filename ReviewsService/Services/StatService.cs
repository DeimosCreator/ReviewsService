using Microsoft.EntityFrameworkCore;
using ReviewsService.Data;
using ReviewsService.Models.Dtos;
using ReviewsService.Services.Interfaces;

namespace ReviewsService.Services;

public class StatService : IStatService
{
    private readonly AppDbContext _db;

    public StatService(AppDbContext db)
    {
        _db = db;
    }
    
    public async Task<Rating> GetRating(int productId)
    {
        var query = _db.Reviews.Where(review => review.ProductId == productId);

        var reviewsCount = await query.CountAsync();

        if (reviewsCount == 0)
            return new Rating(0, 0);

        var ratingAvg = (float) await query.AverageAsync(review => review.Rating);

        return new Rating(ratingAvg, reviewsCount);
    }
}