using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class Measurement
{
    [Key]
    public int Id { get; init; }
    public DateOnly Date { get; init; }
    public int Systolic { get; init; }
    public int Diastolic { get; init; }
    public bool Seen { get; init; }
    
    // Foreign key to Patient
    [StringLength(20, MinimumLength = 10)]
    public string PatientSsn { get; set; } = null!;
    // Optional reference
    public Patient Patient { get; init; } = null!;
}