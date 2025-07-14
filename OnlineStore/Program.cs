using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("OnlineStoreConnectionString");
builder.Services.AddDbContext<OnlineStoreDBContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
