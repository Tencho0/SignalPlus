using Microsoft.EntityFrameworkCore;
using SignalPlus.Data;
using SignalPlus.Services.Interfaces;
using SignalPlus.Services;
using Microsoft.AspNetCore.Identity;
using SignalPlus.Models;
using System;
using SignalPlus.Infrastructure;
using SignalPlus.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders(); 

builder.Services.AddHttpClient();
builder.Services.Configure<GoogleReCaptchaConfig>(
    builder.Configuration.GetSection("GoogleReCaptcha"));
builder.Services.AddTransient<IReCaptchaService, ReCaptchaService>();

builder.Services.AddScoped<ISignalService, SignalService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

app.PrepareDatabase();

//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    try
//    {
//        var context = services.GetRequiredService<ApplicationDbContext>();
//        DatabaseSeeder.Seed(context);
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine($"An error occurred seeding the DB: {ex.Message}");
//    }
//}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/Error/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Signal}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
