using Microsoft.EntityFrameworkCore;
using ReviewsService.Data;
using ReviewsService.Services;
using ReviewsService.Services.Interfaces;

var builder = WebApplication.CreateBuilder();

// сервисы
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IStatService, StatService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
    
var app = builder.Build();

// конвейер
app.UseSwagger();
app.UseSwaggerUI();

// миграции
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate(); 
}

app.MapControllers();
app.Run();