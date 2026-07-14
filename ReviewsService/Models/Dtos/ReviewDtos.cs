using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ReviewsService.Models.Dtos;

public record ReviewDto(int Id, int ProductId, int Rating, string Text, DateTime CreatedAt);

public record CreateReviewDto(
    [property: JsonPropertyName("product_id")] int ProductId,
    [param: Range(1, 5)] int Rating,
    string Text);