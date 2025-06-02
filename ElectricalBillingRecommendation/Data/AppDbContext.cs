using ElectricalBillingRecommendation.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace ElectricalBillingRecommendation.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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

            SeedData(modelBuilder);
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


        private void SeedData(ModelBuilder modelBuilder)
        {
            //Seed Roles
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "2", Name = "User", NormalizedName = "USER" }
                );

            //Seed Admin Data
            var hasher = new PasswordHasher<IdentityUser>();
            var adminUser = new IdentityUser
            {
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                PhoneNumber = "1234567890",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                LockoutEnabled = false,
            };
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin!123");

            modelBuilder.Entity<IdentityUser>().HasData(adminUser);

            //Assign Role To Admin
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "1",
                    UserId = adminUser.Id
                }
                );

            //Seed User Data
            var consumerUser = new IdentityUser
            {
                UserName = "user@user.com",
                NormalizedUserName = "USER@USER.COM",
                Email = "user@user.com",
                NormalizedEmail = "USER@USER.COM",
                PhoneNumber = "1234567890",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                LockoutEnabled = false,
            };
            consumerUser.PasswordHash = hasher.HashPassword(consumerUser, "User!123");

            modelBuilder.Entity<IdentityUser>().HasData(consumerUser);

            //Assign Role To User
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "2",
                    UserId = consumerUser.Id
                }
                );

            // Seed Plan
            modelBuilder.Entity<Plan>().HasData(
                new Plan { Id = 1, Name = "Standard", Discount = 0.05 },
                new Plan { Id = 2, Name = "Premium", Discount = 0.10 }
            );

            // Seed PricingTier 
            modelBuilder.Entity<PricingTier>().HasData(
                new PricingTier { Id = 1, PlanId = 1, Threshold = 100, PricePerKwh = 0.10, UpdatedAt = DateTime.UtcNow },
                new PricingTier { Id = 2, PlanId = 1, Threshold = 300, PricePerKwh = 0.08, UpdatedAt = DateTime.UtcNow },
                new PricingTier { Id = 3, PlanId = 1, Threshold = 500, PricePerKwh = 0.07, UpdatedAt = DateTime.UtcNow },
                new PricingTier { Id = 4, PlanId = 1, Threshold = null, PricePerKwh = 0.06, UpdatedAt = DateTime.UtcNow },

                new PricingTier { Id = 5, PlanId = 2, Threshold = 200, PricePerKwh = 0.09, UpdatedAt = DateTime.UtcNow },
                new PricingTier { Id = 6, PlanId = 2, Threshold = 400, PricePerKwh = 0.07, UpdatedAt = DateTime.UtcNow },
                new PricingTier { Id = 7, PlanId = 2, Threshold = 600, PricePerKwh = 0.05, UpdatedAt = DateTime.UtcNow },
                new PricingTier { Id = 8, PlanId = 2, Threshold = null, PricePerKwh = 0.04, UpdatedAt = DateTime.UtcNow }
            );

            modelBuilder.Entity<TaxGroup>().HasData(
            new TaxGroup { Id = 1, Name = "household", Vat = 0.17, EcoTax = 0.005 },
            new TaxGroup { Id = 2, Name = "business", Vat = 0.20, EcoTax = 0.010 }
             );

        }
    }
}