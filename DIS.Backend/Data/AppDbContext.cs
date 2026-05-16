using DIS.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace DIS.Backend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Person> Persons => Set<Person>();
    public DbSet<Cycle> Cycles => Set<Cycle>();
    public DbSet<DailyLog> DailyLogs => Set<DailyLog>();
    public DbSet<FlowLevel> FlowLevels => Set<FlowLevel>();
    public DbSet<PhysicalSymptom> PhysicalSymptoms => Set<PhysicalSymptom>();
    public DbSet<DailyLogSymptom> DailyLogSymptoms => Set<DailyLogSymptom>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DailyLogSymptom>()
            .HasKey(x => new { x.DailyLogId, x.PhysicalSymptomId });

        modelBuilder.Entity<DailyLogSymptom>()
            .HasOne(x => x.DailyLog)
            .WithMany(x => x.DailyLogSymptoms)
            .HasForeignKey(x => x.DailyLogId);

        modelBuilder.Entity<DailyLogSymptom>()
            .HasOne(x => x.PhysicalSymptom)
            .WithMany(x => x.DailyLogSymptoms)
            .HasForeignKey(x => x.PhysicalSymptomId);

        modelBuilder.Entity<Person>().HasData(
            new Person
            {
                PersonId = 1,
                Name = "Test User",
                Gender = "Female",
                Birthdate = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );

        modelBuilder.Entity<Cycle>().HasData(
            new Cycle
            {
                CycleId = 1,
                CycleNumber = 1,
                StartDate = new DateTime(2026, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2026, 5, 28, 0, 0, 0, DateTimeKind.Utc),
                PersonId = 1
            }
        );

        modelBuilder.Entity<FlowLevel>().HasData(
            new FlowLevel { FlowLevelId = 1, Amount = "None" },
            new FlowLevel { FlowLevelId = 2, Amount = "Light" },
            new FlowLevel { FlowLevelId = 3, Amount = "Medium" },
            new FlowLevel { FlowLevelId = 4, Amount = "Heavy" }
        );

        modelBuilder.Entity<PhysicalSymptom>().HasData(
            new PhysicalSymptom { PhysicalSymptomId = 1, Name = "Headache" },
            new PhysicalSymptom { PhysicalSymptomId = 2, Name = "Cramps" },
            new PhysicalSymptom { PhysicalSymptomId = 3, Name = "Sore breasts" },
            new PhysicalSymptom { PhysicalSymptomId = 4, Name = "Tiredness" },
            new PhysicalSymptom { PhysicalSymptomId = 5, Name = "Back pain" }
        );

        modelBuilder.Entity<DailyLog>().HasData(
            new DailyLog
            {
                DailyLogId = 1,
                Date = new DateTime(2026, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                CycleDay = 1,
                CycleId = 1,
                FlowLevelId = 3
            },
            new DailyLog
            {
                DailyLogId = 2,
                Date = new DateTime(2026, 5, 2, 0, 0, 0, DateTimeKind.Utc),
                CycleDay = 2,
                CycleId = 1,
                FlowLevelId = 2
            }
        );

        modelBuilder.Entity<DailyLogSymptom>().HasData(
            new DailyLogSymptom
            {
                DailyLogId = 1,
                PhysicalSymptomId = 2
            },
            new DailyLogSymptom
            {
                DailyLogId = 1,
                PhysicalSymptomId = 4
            },
            new DailyLogSymptom
            {
                DailyLogId = 2,
                PhysicalSymptomId = 1
            }
        );
    }
}