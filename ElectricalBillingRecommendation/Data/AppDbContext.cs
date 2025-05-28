using ElectricalBillingRecommendation.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace ElectricalBillingRecommendation.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             modelBuilder.Entity<PricingTier>()
                .HasOne(pt => pt.Plan)
                .WithMany(p => p.PricingTiers)
                .HasForeignKey(pt => pt.PlanId)
                .OnDelete(DeleteBehavior.Cascade);

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(ToSnakeCase(entity.GetTableName()));

                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(ToSnakeCase(property.Name));
                }

                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(ToSnakeCase(property.Name));
                }
            }
        }

        private static string ToSnakeCase(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            var startUnderscores = Regex.Match(input, @"^_+");
            return startUnderscores + Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
        }

        public DbSet<Models.TaxGroup> TaxGroups { get; set; }
        public DbSet<Models.Plan> Plans { get; set; }
        public DbSet<Models.PricingTier> PricingTiers { get; set; }
    }
}



/*
    modelBuilder.Entity<Plan>().HasData(
    new Plan { Id = 1, Name = "Standard", Discount = 0.1 },
    new Plan { Id = 2, Name = "Premium", Discount = 0.2 }
);
*/