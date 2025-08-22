using Microsoft.EntityFrameworkCore;
using UsersService.Application.Abstractions;
using UsersService.Infrastructure.Presistance;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.Run();
