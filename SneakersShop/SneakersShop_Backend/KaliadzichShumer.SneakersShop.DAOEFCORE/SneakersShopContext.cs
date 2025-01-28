using Microsoft.EntityFrameworkCore;
using KaliadzichShumer.SneakersShop.DAOEFCORE.Models;
using KaliadzichShumer.SneakersShop.INTERFACES.Models;

namespace KaliadzichShumer.SneakersShop.DAOEFCORE {
    public class SneakersShopContext : DbContext {
        private readonly string _connectionString;

        public SneakersShopContext(string connectionString) {
            _connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite(_connectionString); }

        public DbSet<Producer> Producers { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)   {
            modelBuilder.Entity<Producer>()
                .HasMany(p => p.ProductsCollection)
                .WithOne(p => p.Producer)
                .HasForeignKey(p => p.ProducerId);

            modelBuilder.Entity<Product>()
                .Property(p => p.ShoeType)
                .HasConversion<string>() // Хранить как строку. Если хотите хранить как int, используйте HasConversion<int>()
                .IsRequired(); // Указывает, что поле обязательно
        }
    }
} 