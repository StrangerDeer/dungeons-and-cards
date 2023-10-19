using dungeons_and_cards.Models.Contexts;
using dungeons_and_cards.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Logger
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddControllersWithViews();


//Connect database
builder.Services.AddDbContext<Context>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Setup cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAngularOrigins",
        config =>
        {
            config.WithOrigins(" http://localhost:4200")
                .AllowAnyHeader()
                .SetIsOriginAllowed((host) => true)
                .AllowAnyMethod();
        });
});

//Dependecy Injection 
builder.Services.AddTransient<IUserService, UserService>();


builder.Services.AddMvc();

builder.Services.AddControllers();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors("AllowAngularOrigins");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();