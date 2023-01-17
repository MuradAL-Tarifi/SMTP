using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMTP.OtherModels
{
    public partial class TrackerHistoryDBContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public TrackerHistoryDBContext(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public TrackerHistoryDBContext(DbContextOptions<TrackerHistoryDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<InventoryHistory> InventoryHistory { get; set; }
        public virtual DbSet<InventoryHistoryBackup> InventoryHistoryBackup { get; set; }
        public virtual DbSet<InventoryHistoryMock> InventoryHistoryMock { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration["ConnectionString:DBConnectionString2"]);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InventoryHistory>(entity =>
            {
                entity.Property(e => e.Alram)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.GatewayImei)
                    .IsRequired()
                    .HasColumnName("GatewayIMEI")
                    .HasMaxLength(500);

                entity.Property(e => e.GpsDate).HasColumnType("datetime");

                entity.Property(e => e.Gsmstatus)
                    .HasColumnName("GSMStatus")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Humidity).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.Serial)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Temperature).HasColumnType("decimal(8, 2)");
            });

            modelBuilder.Entity<InventoryHistoryBackup>(entity =>
            {
                entity.Property(e => e.Alram)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.GatewayImei).HasColumnName("GatewayIMEI");

                entity.Property(e => e.GpsDate).HasColumnType("datetime");

                entity.Property(e => e.Gsmstatus)
                    .HasColumnName("GSMStatus")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Humidity).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.Serial)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Temperature).HasColumnType("decimal(8, 2)");
            });

            modelBuilder.Entity<InventoryHistoryMock>(entity =>
            {
                entity.Property(e => e.Alram)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.GatewayImei).HasColumnName("GatewayIMEI");

                entity.Property(e => e.GpsDate).HasColumnType("datetime");

                entity.Property(e => e.Gsmstatus)
                    .HasColumnName("GSMStatus")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Humidity).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.Serial)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Temperature).HasColumnType("decimal(8, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
