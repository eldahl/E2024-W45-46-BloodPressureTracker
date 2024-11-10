using Microsoft.EntityFrameworkCore;

namespace Models;

public class BtpDbContext : DbContext
{
    public virtual DbSet<Measurement> Measurements { get; set; }
    public virtual DbSet<Patient> Patients { get; set; }
    
    public BtpDbContext(){}
    public BtpDbContext(DbContextOptions<BtpDbContext> options) : base(options) { }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = Environment.GetEnvironmentVariable("MySQLDbConnection")!;
        optionsBuilder.UseMySQL(connectionString ?? throw new NullReferenceException("Environment variable MySQLDbConnection not supplied...!"));
    }
    
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

        modelBuilder.Entity<Patient>().HasData(
            new Patient 
            {
                SSN = "462-59-4864",
                Name = "Wilber Ma",
                Mail = "WMA@mail.com"
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
            }
        );
        
        
    }
}