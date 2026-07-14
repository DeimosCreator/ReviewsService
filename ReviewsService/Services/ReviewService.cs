using Microsoft.EntityFrameworkCore;
using ReviewsService.Data;
using ReviewsService.Models;
using ReviewsService.Models.Dtos;
using ReviewsService.Services.Interfaces;

namespace ReviewsService.Services;

public class ReviewService : IReviewService
{
    private readonly AppDbContext _db;

    public ReviewService(AppDbContext db)
    {
        _db = db;
    }
    
    public async Task<ReviewDto> CreateReview(CreateReviewDto createReviewDto)
    {
        var review = new Review
        {
            ProductId = createReviewDto.ProductId,
            Rating = createReviewDto.Rating,
            Text = createReviewDto.Text,
            CreatedAt = DateTime.UtcNow
        };

        _db.Reviews.Add(review);
        await _db.SaveChangesAsync();

        var reviewDto = new ReviewDto(review.Id, review.ProductId, review.Rating, review.Text, review.CreatedAt);
        return reviewDto;
    }

    public async Task<List<ReviewDto>> GetReviews(int productId)
    {
        var reviews = await _db.Reviews
            .Where(reviews => reviews.ProductId == productId)
            .Select(review => new ReviewDto(review.Id, review.ProductId, review.Rating, review.Text, review.CreatedAt))
            .ToListAsync();
        return reviews;
    }
}