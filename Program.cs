
using MediCart.Data;
using MediCart.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// SQL Server
var connectionString = builder.Configuration
    .GetConnectionString("MedicineDB")
    ?? throw new InvalidOperationException(
        "MedicineDB connection string is missing.");

builder.Services.AddDbContext<MedicineDbContext>(
    options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<
    IShopMessageService,
    ShopMessageService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Medicine}/{action=Index}/{id?}");

app.Run();
