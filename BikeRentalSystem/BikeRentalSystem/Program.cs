using BikeRentalSystem.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using BikeRentalSystem.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(x => x.UseInMemoryDatabase("Test"));
builder.Services.AddScoped<Repository<Vehicle>>(sp =>
{
    var dbContext = sp.GetRequiredService<AppDbContext>();
    return new Repository<Vehicle>(dbContext);
});
builder.Services.AddScoped<IRepository<Vehicle>, VehicleRepository>();



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceprovider = scope.ServiceProvider;
    var dbContext = serviceprovider.GetRequiredService<AppDbContext>();
    DataInitialization.Initialize(dbContext);
}
   
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
