using FruitFactsApp.Library.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FruitFactsApp.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<FruitEntity> Fruits { get; set; } = null!;
    }

}
