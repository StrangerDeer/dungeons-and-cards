using dungeons_and_cards.Models.UserModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace dungeons_and_cards.Models.Contexts;

public class Context : DbContext
{
    
    public DbSet<User>? Users { get; set; }
    public DbSet<BannedUser>? BannedUsers { get; set; }
    
    public Context(DbContextOptions<Context> options) : base(options)
    {
        
    }

    
}