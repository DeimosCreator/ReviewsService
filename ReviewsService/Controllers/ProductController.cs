using Microsoft.AspNetCore.Mvc;
using ReviewsService.Models.Dtos;
using ReviewsService.Services.Interfaces;

namespace ReviewsService.Controllers;

[ApiController]
[Route("products")]
public class ProductController : ControllerBase
{
    private readonly IReviewService _reviewService;
    private readonly IStatService _statService;

    public ProductController(IReviewService reviewService, IStatService statService)
    {
        _reviewService = reviewService;
        _statService = statService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateReview([FromBody] CreateReviewDto createReviewDto)
    {
        var review = await _reviewService.CreateReview(createReviewDto);
        return Ok(review);
    }

    [HttpGet("{id::int}/reviews")]
    public async Task<IActionResult> GetReviews(int id)
    {
        var reviews = await _reviewService.GetReviews(id);
        return Ok(reviews);
    }

    [HttpGet("{id::int}/rating")]
    public async Task<IActionResult> GetRating(int id)
    {
        var rating = await _statService.GetRating(id);
        return Ok(rating);
    }
}