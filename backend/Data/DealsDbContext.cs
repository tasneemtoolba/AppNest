using Microsoft.EntityFrameworkCore;

namespace DealsApp.Models;

public class ApiDbContext : DbContext
{
   
    protected readonly IConfiguration Configuration;
    public ApiDbContext(IConfiguration configuration){
        Configuration = configuration;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options){
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
    }

    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Deal> Deals { get; set; } = null!;
    public DbSet<Score> Scores { get; set; } = null!;
}