using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models;

public class Measurement
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }
    public DateOnly Date { get; init; }
    public int Systolic { get; init; }
    public int Diastolic { get; init; }
    public bool Seen { get; init; }
    
    // Foreign key to Patient
    [JsonIgnore]
    [StringLength(20, MinimumLength = 10)]
    public string PatientSsn { get; set; } = null!;
    // Optional reference
    public Patient Patient { get; init; } = null!;
}