using BikeRentalSystem.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using BikeRentalSystem.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using BikeRentalSystem.MappingProfiles;
using FluentValidation;
using BikeRentalSystem.Validators;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(x => x.UseInMemoryDatabase("Test"));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultUI().AddDefaultTokenProviders();
builder.Services.AddScoped<VehicleRepository>();
builder.Services.AddScoped<RentalPointRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddAutoMapper(typeof(RentalPointMappingProfile),typeof(VehicleMappingProfile),typeof(VehicleTypeMappingProfile));
builder.Services.AddTransient<IValidator<Reservation>, ReservationValidator>();
builder.Services.AddTransient<IValidator<RentalPoint>, RentalPointValidator>();
builder.Services.AddTransient<IValidator<Vehicle>, VehicleValidator>();
builder.Services.AddTransient<IValidator<VehicleType>, VehicleTypeValidator>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;


    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;

});
builder.Services.ConfigureApplicationCookie(options =>
{
    
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceprovider = scope.ServiceProvider;
    var dbContext = serviceprovider.GetRequiredService<AppDbContext>();
    var userManager = serviceprovider.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = serviceprovider.GetRequiredService<RoleManager<IdentityRole>>();
    var dataInitialization = new DataInitialization(dbContext, userManager, roleManager);
    await dataInitialization.InitializeUsersAsync();
    dataInitialization.Initialize(dbContext);
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
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
