using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Models;

public class BtpDbContext : DbContext
{
    public virtual DbSet<Measurement> Measurements { get; set; }
    public virtual DbSet<Patient> Patients { get; set; }

    public BtpDbContext() { }
    public BtpDbContext(DbContextOptions<BtpDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = Environment.GetEnvironmentVariable("MySQLDbConnection")!;
        optionsBuilder.UseMySQL(connectionString ?? throw new NullReferenceException("Environment variable MySQLDbConnection not supplied...!"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var dateOnlyToFromDateTimeConverter = new ValueConverter<DateOnly, DateTime>(
            dateOnlyValue => new DateTime(dateOnlyValue, new TimeOnly(0)),
            dateTimeValue => DateOnly.FromDateTime(dateTimeValue)
        );

        modelBuilder.Entity<Measurement>(entity =>
        {
            // Primary key | Identifier of measurement
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            entity.Property(p => p.Date)
                .IsRequired()
                .HasConversion(dateOnlyToFromDateTimeConverter);
            entity.Property(p => p.Systolic).IsRequired();
            entity.Property(p => p.Diastolic).IsRequired();
            entity.Property(p => p.Seen);
        });

        // Should an index be added? No, this is done automatically for foreign keys in InnoDB.
        // https://stackoverflow.com/questions/304317/does-mysql-index-foreign-key-columns-automatically
        modelBuilder.Entity<Measurement>()
            .HasOne(m => m.Patient)
            .WithOne()
            .HasForeignKey<Measurement>(m => m.PatientSsn);

        modelBuilder.Entity<Patient>(entity =>
        {
            // Primary key | Social Security Number of patient
            entity.HasKey(p => p.SSN);
            entity.Property(p => p.SSN).IsRequired();

            entity.Property(p => p.Mail).IsRequired();
            entity.Property(p => p.Name).IsRequired();
        });

        modelBuilder.Entity<Patient>().HasData(
            new Patient
            {
                SSN = "462-59-4864",
                Name = "Wilber Ma",
                Mail = "WMA@mail.com"
            },
            new Patient
            {
                SSN = "144-75-2929",
                Name = "Rebeca Pao",
                Mail = "PAO@mail.com"
            },
            new Patient
            {
                SSN = "178-14-0036",
                Name = "Raghallach Husson",
                Mail = "RHusson@mail.com"
            },
            new Patient
            {
                SSN = "500-29-2239",
                Name = "Xena Yun",
                Mail = "xy@mail.com"
            },
            new Patient
            {
                SSN = "509-90-5304",
                Name = "Helana Clayton",
                Mail = "Clayton@mail.com"
            }
        );

        DateOnly firstDayOf2024 = DateOnly.FromDateTime(new DateTime(2024, 1, 1));
        modelBuilder.Entity<Measurement>().HasData(
            new Measurement
            {
                Id = 1,
                Date = firstDayOf2024,
                Diastolic = 170,
                Systolic = 67,
                Seen = false,
                PatientSsn = "462-59-4864"
            },
            new Measurement
            {
                Id = 2,
                Date = firstDayOf2024,
                Diastolic = 200,
                Systolic = 72,
                Seen = false,
                PatientSsn = "144-75-2929"
            },
            new Measurement
            {
                Id = 3,
                Date = firstDayOf2024,
                Diastolic = 198,
                Systolic = 64,
                Seen = false,
                PatientSsn = "178-14-0036"
            },
            new Measurement
            {
                Id = 4,
                Date = firstDayOf2024.AddDays(1),
                Diastolic = 120,
                Systolic = 76,
                Seen = false,
                PatientSsn = "500-29-2239"
            },
            new Measurement
            {
                Id = 5,
                Date = firstDayOf2024.AddDays(1),
                Diastolic = 174,
                Systolic = 89,
                Seen = false,
                PatientSsn = "509-90-5304"
            }
        );


    }
}
