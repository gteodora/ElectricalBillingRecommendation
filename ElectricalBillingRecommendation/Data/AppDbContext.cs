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
            // Automatski preslikaj sve nazive tabela i kolona u snake_case
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // Mijenja naziv tabele
                entity.SetTableName(ToSnakeCase(entity.GetTableName()));

                // Mijenja nazive kolona
                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(ToSnakeCase(property.Name));
                }

                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(ToSnakeCase(property.Name));
                }

                // Mijenja nazive ključeva, indeksa itd. ako želiš - slično kao gore
            }
        }

        private static string ToSnakeCase(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            var startUnderscores = Regex.Match(input, @"^_+");
            return startUnderscores + Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
        }

        public DbSet<Models.TaxGroup> TaxGroups { get; set; }
        public DbSet<Models.Plan> Plans { get; set; } = default!;
    }
}
