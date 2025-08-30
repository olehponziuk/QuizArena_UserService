using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using UsersService.Application.Abstractions;
using UsersService.Infrastructure.Identity;
using UsersService.Infrastructure.Presistance;
using UsersService.Infrastructure.Storeage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();

builder.Services.Configure<IOptions<CloudinaryOptions>>(
    builder.Configuration.GetSection("Cloudinary"));
builder.Services.AddSingleton<IUserImageStorage, CloudinaryStorage>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}


app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.Run();
