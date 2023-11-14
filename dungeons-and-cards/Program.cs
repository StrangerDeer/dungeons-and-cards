using System.Text;
using dungeons_and_cards.Models.Contexts;
using dungeons_and_cards.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var services = builder.Services;

//Logger
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
services.AddControllersWithViews();

//Connect database
services.AddDbContext<Context>(
    options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

//Authentication && Authorization
services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = config["JwtSetting:Issuer"],
        ValidAudience = config["JwtSetting:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSetting:Key"]!)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
        
    };
});

services.AddAuthorization();

//Setup cors
services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAngularOrigins",
        configuration =>
        {
            configuration.WithOrigins(" http://localhost:4200")
                .AllowAnyHeader()
                .SetIsOriginAllowed((host) => true)
                .AllowAnyMethod();
        });
});

//Dependecy Injection 
services.AddTransient<IUserService, UserService>();

services.AddMvc();

services.AddControllers();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    
}


app.UseAuthentication();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors("AllowAngularOrigins");

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();