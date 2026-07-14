using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ReviewsService.Models;

public class Review
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Rating { get; set; }
    public string Text { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}