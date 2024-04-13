using Automarket;
using Automarket.DAL;
using Automarket.DAL.Interfaces;
using Automarket.DAL.Repositories;
using Automarket.Domain.Entity;
using Automarket.Domain.Responce;
using Automarket.Service.Implementations;
using Automarket.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
        options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
    });

builder.Services.AddHttpContextAccessor();
builder.Services.InitializeRepositories();
builder.Services.InitializeServices();

// Build the application.
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Errors/Error");
    app.UseStatusCodePagesWithReExecute("/Errors/InternalServerError");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "Error",
        pattern: "{*url}",
        defaults: new { controller = "Errors", action = "Error" }
    );
});

app.Run();
