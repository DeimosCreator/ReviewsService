using Microsoft.EntityFrameworkCore;
using ReviewsService.Models;

namespace ReviewsService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Review> Reviews => Set<Review>();
}