using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebApplication3.Models
{
    public partial class modelContext : DbContext
    {
        public modelContext()
        {
        }

        public modelContext(DbContextOptions<modelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApiAuth> ApiAuths { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Shop> Shops { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=model;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ApiAuth>(entity =>
            {
                entity.HasKey(e => e.ApiKey);

                entity.ToTable("ApiAuth");

                entity.Property(e => e.ApiKey).HasMaxLength(100);

                entity.Property(e => e.ApiName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.ToTable("Inventory");

                //entity.HasOne(d => d.Shop)
                //    .WithMany(p => p.Inventories)
                //    .HasForeignKey(d => d.ShopId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK__Inventory__ShopI__35BCFE0A");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(x => x.OrderId);

                entity.Property(e => e.Items)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Shop>(entity =>
            {
                entity.ToTable("Shop");

                entity.Property(e => e.ShopId).ValueGeneratedNever();

                entity.Property(e => e.ActiveStatus).HasMaxLength(20);

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.ShopDesignation).HasMaxLength(20);

                entity.Property(e => e.State).HasMaxLength(2);

                entity.Property(e => e.StreetName).HasMaxLength(50);

                entity.Property(e => e.SuiteNumber).HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
