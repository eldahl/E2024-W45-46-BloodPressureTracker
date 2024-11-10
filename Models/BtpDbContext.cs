using Microsoft.EntityFrameworkCore;

namespace Models;

public class BtpDbContext : DbContext
{
    public virtual DbSet<Measurement> Measurements { get; set; }
    public virtual DbSet<Patient> Patients { get; set; }

    public BtpDbContext(DbContextOptions<BtpDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Measurement>(entity =>
        {
            // Primary key | Identifier of measurement
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();
            
            entity.Property(p => p.Date).IsRequired();
            entity.Property(p => p.Systolic).IsRequired();
            entity.Property(p => p.Diastolic).IsRequired();
            entity.Property(p => p.Seen);
        });
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
        
        DateOnly firstDayOf2024 = DateOnly.FromDateTime(new DateTime(2024, 1, 1));
        modelBuilder.Entity<Measurement>().HasData(
            new Measurement 
            {
                Date = firstDayOf2024,
                Diastolic = 10,
                Systolic = 10,
                Seen = false
            }
        );
    }
}