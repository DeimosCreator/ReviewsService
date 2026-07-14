using ReviewsService.Models.Dtos;

namespace ReviewsService.Services.Interfaces;

public interface IReviewService
{
    public Task<ReviewDto> CreateReview(CreateReviewDto createReviewDto);

    public Task<List<ReviewDto>> GetReviews(int productId);
}