using Application.Services;
using Domain.IRepository;
using Infrastructure.DataAccess;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(optn =>
    {
    optn.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnectionString"));

    });

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IRewardService, RewardService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<ApplicationDbContext>();

try
{
    await context.Database.MigrateAsync();
}
catch(Exception ex)
{

}

app.Run();
