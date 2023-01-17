using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMTP.ThirdModels
{
    public partial class TrackerDBContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public TrackerDBContext(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public TrackerDBContext(DbContextOptions<TrackerDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Agent> Agent { get; set; }
        public virtual DbSet<AggregatedCounter> AggregatedCounter { get; set; }
        public virtual DbSet<Alert> Alert { get; set; }
        public virtual DbSet<AlertByUserWatcher> AlertByUserWatcher { get; set; }
        public virtual DbSet<AlertTypeLookup> AlertTypeLookup { get; set; }
        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<Counter> Counter { get; set; }
        public virtual DbSet<CustomAlert> CustomAlert { get; set; }
        public virtual DbSet<CustomAlertWatcher> CustomAlertWatcher { get; set; }
        public virtual DbSet<DayOfWeekLookup> DayOfWeekLookup { get; set; }
        public virtual DbSet<DeviceType> DeviceType { get; set; }
        public virtual DbSet<EmailHistory> EmailHistory { get; set; }
        public virtual DbSet<EventLog> EventLog { get; set; }
        public virtual DbSet<Fleet> Fleet { get; set; }
        public virtual DbSet<FleetDetails> FleetDetails { get; set; }
        public virtual DbSet<Gateway> Gateway { get; set; }
        public virtual DbSet<Hash> Hash { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<InventoryCustomAlert> InventoryCustomAlert { get; set; }
        public virtual DbSet<InventorySensor> InventorySensor { get; set; }
        public virtual DbSet<Job> Job { get; set; }
        public virtual DbSet<JobParameter> JobParameter { get; set; }
        public virtual DbSet<JobQueue> JobQueue { get; set; }
        public virtual DbSet<List> List { get; set; }
        public virtual DbSet<Nlog> Nlog { get; set; }
        public virtual DbSet<OnlineInventoryHistory> OnlineInventoryHistory { get; set; }
        public virtual DbSet<PrivilegeType> PrivilegeType { get; set; }
        public virtual DbSet<RegisterType> RegisterType { get; set; }
        public virtual DbSet<ReportSchedule> ReportSchedule { get; set; }
        public virtual DbSet<ReportScheduleHistory> ReportScheduleHistory { get; set; }
        public virtual DbSet<ReportTypeLookup> ReportTypeLookup { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<ScheduleTypeLookup> ScheduleTypeLookup { get; set; }
        public virtual DbSet<Schema> Schema { get; set; }
        public virtual DbSet<Sensor> Sensor { get; set; }
        public virtual DbSet<SensorAlert> SensorAlert { get; set; }
        public virtual DbSet<SensorAlertHisotry> SensorAlertHisotry { get; set; }
        public virtual DbSet<SensorAlertTypeLookup> SensorAlertTypeLookup { get; set; }
        public virtual DbSet<Server> Server { get; set; }
        public virtual DbSet<Set> Set { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<SystemSetting> SystemSetting { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserInventory> UserInventory { get; set; }
        public virtual DbSet<UserPrivilege> UserPrivilege { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<UserWarehouse> UserWarehouse { get; set; }
        public virtual DbSet<Warehouse> Warehouse { get; set; }
        public virtual DbSet<WaslCode> WaslCode { get; set; }
        public virtual DbSet<WaslIntegrationLog> WaslIntegrationLog { get; set; }
        public virtual DbSet<WaslIntegrationLogTypeLookup> WaslIntegrationLogTypeLookup { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration["ConnectionString:DBConnectionString3"]);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AggregatedCounter>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("PK_HangFire_CounterAggregated");

                entity.ToTable("AggregatedCounter", "HangFire");

                entity.HasIndex(e => e.ExpireAt)
                    .HasName("IX_HangFire_AggregatedCounter_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<Alert>(entity =>
            {
                entity.Property(e => e.Humidity).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.Temperature).HasColumnType("decimal(8, 2)");

                entity.HasOne(d => d.CustomAlert)
                    .WithMany(p => p.Alert)
                    .HasForeignKey(d => d.CustomAlertId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Alert_CustomAlert");
            });

            modelBuilder.Entity<AlertByUserWatcher>(entity =>
            {
                entity.Property(e => e.UserId).IsRequired();
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.NameEn).HasMaxLength(50);
            });

            modelBuilder.Entity<Counter>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Counter", "HangFire");

                entity.HasIndex(e => e.Key)
                    .HasName("CX_HangFire_Counter")
                    .IsClustered();

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<CustomAlert>(entity =>
            {
                entity.HasOne(d => d.AlertTypeLookup)
                    .WithMany(p => p.CustomAlert)
                    .HasForeignKey(d => d.AlertTypeLookupId);

                entity.HasOne(d => d.Fleet)
                    .WithMany(p => p.CustomAlert)
                    .HasForeignKey(d => d.FleetId);
            });

            modelBuilder.Entity<DayOfWeekLookup>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<EventLog>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.EventLog)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Fleet>(entity =>
            {
                entity.HasIndex(e => e.AgentId);

                entity.Property(e => e.LogoPhotoExtention).HasMaxLength(50);

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.Fleet)
                    .HasForeignKey(d => d.AgentId);
            });

            modelBuilder.Entity<FleetDetails>(entity =>
            {
                entity.Property(e => e.SfdacompanyActivities).HasColumnName("SFDACompanyActivities");
            });

            modelBuilder.Entity<Gateway>(entity =>
            {
                entity.Property(e => e.Imei)
                    .IsRequired()
                    .HasColumnName("IMEI")
                    .HasMaxLength(500);

                entity.Property(e => e.SimcardExpirationDate).HasColumnName("SIMCardExpirationDate");

                entity.Property(e => e.Simnumber).HasColumnName("SIMNumber");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Gateway)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Gateway_Brand");
            });

            modelBuilder.Entity<Hash>(entity =>
            {
                entity.HasKey(e => new { e.Key, e.Field })
                    .HasName("PK_HangFire_Hash");

                entity.ToTable("Hash", "HangFire");

                entity.HasIndex(e => e.ExpireAt)
                    .HasName("IX_HangFire_Hash_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Field).HasMaxLength(100);
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.HasIndex(e => e.GatewayId);

                entity.HasIndex(e => e.WarehouseId);

                entity.Property(e => e.SfdastoringCategory).HasColumnName("SFDAStoringCategory");

                entity.HasOne(d => d.Gateway)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.GatewayId);

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.WarehouseId);
            });

            modelBuilder.Entity<InventorySensor>(entity =>
            {
                entity.HasIndex(e => e.InventoryId);

                entity.HasOne(d => d.Inventory)
                    .WithMany(p => p.InventorySensor)
                    .HasForeignKey(d => d.InventoryId);

                entity.HasOne(d => d.Sensor)
                    .WithMany(p => p.InventorySensor)
                    .HasForeignKey(d => d.SensorId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("Job", "HangFire");

                entity.HasIndex(e => e.StateName)
                    .HasName("IX_HangFire_Job_StateName")
                    .HasFilter("([StateName] IS NOT NULL)");

                entity.HasIndex(e => new { e.StateName, e.ExpireAt })
                    .HasName("IX_HangFire_Job_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.Property(e => e.Arguments).IsRequired();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.InvocationData).IsRequired();

                entity.Property(e => e.StateName).HasMaxLength(20);
            });

            modelBuilder.Entity<JobParameter>(entity =>
            {
                entity.HasKey(e => new { e.JobId, e.Name })
                    .HasName("PK_HangFire_JobParameter");

                entity.ToTable("JobParameter", "HangFire");

                entity.Property(e => e.Name).HasMaxLength(40);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.JobParameter)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_HangFire_JobParameter_Job");
            });

            modelBuilder.Entity<JobQueue>(entity =>
            {
                entity.HasKey(e => new { e.Queue, e.Id })
                    .HasName("PK_HangFire_JobQueue");

                entity.ToTable("JobQueue", "HangFire");

                entity.Property(e => e.Queue).HasMaxLength(50);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.FetchedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<List>(entity =>
            {
                entity.HasKey(e => new { e.Key, e.Id })
                    .HasName("PK_HangFire_List");

                entity.ToTable("List", "HangFire");

                entity.HasIndex(e => e.ExpireAt)
                    .HasName("IX_HangFire_List_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<Nlog>(entity =>
            {
                entity.ToTable("NLog");

                entity.Property(e => e.Callsite).HasMaxLength(300);

                entity.Property(e => e.Https).HasMaxLength(5);

                entity.Property(e => e.Level)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.Logged).HasColumnType("datetime");

                entity.Property(e => e.Logger).HasMaxLength(300);

                entity.Property(e => e.MachineName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Message).IsRequired();

                entity.Property(e => e.Port).HasMaxLength(10);

                entity.Property(e => e.RemoteAddress).HasMaxLength(300);

                entity.Property(e => e.ServerAddress).HasMaxLength(300);

                entity.Property(e => e.ServerName).HasMaxLength(200);

                entity.Property(e => e.SiteName).HasMaxLength(300);

                entity.Property(e => e.Url).HasMaxLength(2256);

                entity.Property(e => e.Username).HasMaxLength(100);
            });

            modelBuilder.Entity<OnlineInventoryHistory>(entity =>
            {
                entity.Property(e => e.GatewayImei)
                    .IsRequired()
                    .HasColumnName("GatewayIMEI")
                    .HasMaxLength(500);

                entity.Property(e => e.Gsmstatus).HasColumnName("GSMStatus");

                entity.Property(e => e.Humidity).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Serial)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Temperature).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<ReportSchedule>(entity =>
            {
                entity.HasIndex(e => e.DayOfWeekId);

                entity.HasIndex(e => e.FleetId);

                entity.HasIndex(e => e.InventoryId);

                entity.HasIndex(e => e.ReportTypeLookupId);

                entity.HasIndex(e => e.WarehouseId);

                entity.Property(e => e.Pdf).HasColumnName("PDF");

                entity.HasOne(d => d.DayOfWeek)
                    .WithMany(p => p.ReportSchedule)
                    .HasForeignKey(d => d.DayOfWeekId);

                entity.HasOne(d => d.Fleet)
                    .WithMany(p => p.ReportSchedule)
                    .HasForeignKey(d => d.FleetId);

                entity.HasOne(d => d.Inventory)
                    .WithMany(p => p.ReportSchedule)
                    .HasForeignKey(d => d.InventoryId);

                entity.HasOne(d => d.ReportTypeLookup)
                    .WithMany(p => p.ReportSchedule)
                    .HasForeignKey(d => d.ReportTypeLookupId);

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.ReportSchedule)
                    .HasForeignKey(d => d.WarehouseId);
            });

            modelBuilder.Entity<ReportTypeLookup>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<ScheduleTypeLookup>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Schema>(entity =>
            {
                entity.HasKey(e => e.Version)
                    .HasName("PK_HangFire_Schema");

                entity.ToTable("Schema", "HangFire");

                entity.Property(e => e.Version).ValueGeneratedNever();
            });

            modelBuilder.Entity<Sensor>(entity =>
            {
                entity.Property(e => e.CalibrationDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Serial)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Sensor)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sensor_Brand");
            });

            modelBuilder.Entity<SensorAlert>(entity =>
            {
                entity.HasIndex(e => e.InventorySensorId);

                entity.HasIndex(e => e.SensorAlertTypeLookupId);

                entity.Property(e => e.FromValue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.IsSms).HasColumnName("IsSMS");

                entity.Property(e => e.ToValue).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.InventorySensor)
                    .WithMany(p => p.SensorAlert)
                    .HasForeignKey(d => d.InventorySensorId);

                entity.HasOne(d => d.SensorAlertTypeLookup)
                    .WithMany(p => p.SensorAlert)
                    .HasForeignKey(d => d.SensorAlertTypeLookupId);
            });

            modelBuilder.Entity<Server>(entity =>
            {
                entity.ToTable("Server", "HangFire");

                entity.HasIndex(e => e.LastHeartbeat)
                    .HasName("IX_HangFire_Server_LastHeartbeat");

                entity.Property(e => e.Id).HasMaxLength(200);

                entity.Property(e => e.LastHeartbeat).HasColumnType("datetime");
            });

            modelBuilder.Entity<Set>(entity =>
            {
                entity.HasKey(e => new { e.Key, e.Value })
                    .HasName("PK_HangFire_Set");

                entity.ToTable("Set", "HangFire");

                entity.HasIndex(e => e.ExpireAt)
                    .HasName("IX_HangFire_Set_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.HasIndex(e => new { e.Key, e.Score })
                    .HasName("IX_HangFire_Set_Score");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Value).HasMaxLength(256);

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.HasKey(e => new { e.JobId, e.Id })
                    .HasName("PK_HangFire_State");

                entity.ToTable("State", "HangFire");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Reason).HasMaxLength(100);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.State)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_HangFire_State_Job");
            });

            modelBuilder.Entity<SystemSetting>(entity =>
            {
                entity.Property(e => e.CompanyName).HasMaxLength(200);

                entity.Property(e => e.EnableSms).HasColumnName("EnableSMS");

                entity.Property(e => e.EnableSmtp).HasColumnName("EnableSMTP");

                entity.Property(e => e.GoogleApiKey).HasMaxLength(1000);

                entity.Property(e => e.SmsGatewayUrl)
                    .HasColumnName("SMS_GatewayURL")
                    .HasMaxLength(500);

                entity.Property(e => e.SmsPassword)
                    .HasColumnName("SMS_Password")
                    .HasMaxLength(100);

                entity.Property(e => e.SmsUsername)
                    .HasColumnName("SMS_Username")
                    .HasMaxLength(100);

                entity.Property(e => e.SmtpAddress)
                    .HasColumnName("SMTP_Address")
                    .HasMaxLength(500);

                entity.Property(e => e.SmtpDisplayName)
                    .HasColumnName("SMTP_DisplayName")
                    .HasMaxLength(500);

                entity.Property(e => e.SmtpHost)
                    .HasColumnName("SMTP_HOST")
                    .HasMaxLength(500);

                entity.Property(e => e.SmtpIsSslEnabled).HasColumnName("SMTP_IsSslEnabled");

                entity.Property(e => e.SmtpPassword)
                    .HasColumnName("SMTP_Password")
                    .HasMaxLength(100);

                entity.Property(e => e.SmtpPort).HasColumnName("SMTP_PORT");

                entity.Property(e => e.WaslApiKey).HasMaxLength(1000);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.AgentId);

                entity.HasIndex(e => e.FleetId);

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.AgentId);

                entity.HasOne(d => d.Fleet)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.FleetId);
            });

            modelBuilder.Entity<UserInventory>(entity =>
            {
                entity.HasOne(d => d.Inventory)
                    .WithMany(p => p.UserInventory)
                    .HasForeignKey(d => d.InventoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserInventory_Inventory");
            });

            modelBuilder.Entity<UserPrivilege>(entity =>
            {
                entity.HasIndex(e => e.PrivilegeTypeId);

                entity.HasOne(d => d.PrivilegeType)
                    .WithMany(p => p.UserPrivilege)
                    .HasForeignKey(d => d.PrivilegeTypeId);
            });

            modelBuilder.Entity<UserWarehouse>(entity =>
            {
                entity.HasIndex(e => e.WarehouseId);

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.UserWarehouse)
                    .HasForeignKey(d => d.WarehouseId);
            });

            modelBuilder.Entity<Warehouse>(entity =>
            {
                entity.HasIndex(e => e.FleetId);

                entity.Property(e => e.Latitude).HasColumnType("decimal(9, 7)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(9, 7)");

                entity.HasOne(d => d.Fleet)
                    .WithMany(p => p.Warehouse)
                    .HasForeignKey(d => d.FleetId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
