using BikeRentalSystem.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using BikeRentalSystem.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using BikeRentalSystem.MappingProfiles;
using FluentValidation;
using BikeRentalSystem.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(x => x.UseInMemoryDatabase("Test"));
builder.Services.AddScoped<VehicleRepository>();
builder.Services.AddScoped<RentalPointRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddAutoMapper(typeof(RentalPointMappingProfile),typeof(VehicleMappingProfile),typeof(VehicleTypeMappingProfile));
builder.Services.AddTransient<IValidator<Reservation>, ReservationValidator>();
builder.Services.AddTransient<IValidator<RentalPoint>, RentalPointValidator>();
builder.Services.AddTransient<IValidator<Vehicle>, VehicleValidator>();
builder.Services.AddTransient<IValidator<VehicleType>, VehicleTypeValidator>();




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
