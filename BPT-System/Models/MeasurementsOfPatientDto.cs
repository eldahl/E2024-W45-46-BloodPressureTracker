namespace Models;

public class MeasurementsOfPatientDto
{
    public Patient Patient { get; set; } = null!;
    public List<MeasurementClean> Measurements { get; set; } = null!;
}
