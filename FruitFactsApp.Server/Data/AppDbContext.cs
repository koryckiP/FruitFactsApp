using FruitFactsApp.Library.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FruitFactsApp.Server.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Fruit> Fruits { get; set; }
    }

}
