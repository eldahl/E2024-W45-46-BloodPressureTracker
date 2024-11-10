using System.ComponentModel.DataAnnotations;

namespace Models;

public class Patient
{
    [Key]
    [StringLength(20, MinimumLength = 10)]
    public string SSN { get; set; }
    [StringLength(255, MinimumLength = 4)]
    public string Mail { get; set; }
    [StringLength(255, MinimumLength = 4)]
    public string Name { get; set; }
}