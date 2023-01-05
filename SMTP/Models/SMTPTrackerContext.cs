using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMTP.Models
{
    public partial class SMTPTrackerContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public SMTPTrackerContext(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public SMTPTrackerContext(DbContextOptions<SMTPTrackerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AlertTracker> AlertTracker { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration["ConnectionString:DBConnectionString"]);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlertTracker>(entity =>
            {
                entity.Property(e => e.AlertDateTime).HasColumnType("datetime");

                entity.Property(e => e.UserName).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
