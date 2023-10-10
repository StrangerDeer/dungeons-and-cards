using dungeons_and_cards.Models.UserModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace dungeons_and_cards.Models.Contexts;

public class UserContext : DbContext
{
    
    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
        
    }
    
    public DbSet<User> Users { get; set; }

}