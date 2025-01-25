using Microsoft.EntityFrameworkCore;
using KaliadzichShumer.SneakersShop.DAO.SQL.DataObjects;

namespace KaliadzichShumer.SneakersShop.DAO.SQL.DataContext
{
    public class AppDbContext : DbContext
    {
        public DbSet<ManufacturerDO> Manufacturers { get; set; }
        public DbSet<SneakerDO> Sneakers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Можно указать файл, где будет храниться база SQLite:
            optionsBuilder.UseSqlite("Data Source=sneakers.db");
        }
    }
}
