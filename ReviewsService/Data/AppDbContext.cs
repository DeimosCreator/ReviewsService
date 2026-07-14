using Microsoft.EntityFrameworkCore;

namespace ReviewsService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}