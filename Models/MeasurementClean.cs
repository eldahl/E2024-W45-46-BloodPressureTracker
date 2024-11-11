namespace Models;

public class MeasurementClean
{
    public int Id { get; init; }
    public DateOnly Date { get; init; }
    public int Systolic { get; init; }
    public int Diastolic { get; init; }
    public bool Seen { get; init; }
}