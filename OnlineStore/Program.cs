using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Data.Repositories;
using OnlineStore.Data.Repositories.Interfaces;
using OnlineStore.Model;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("OnlineStoreConnectionString");
builder.Services.AddDbContext<OnlineStoreDBContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
