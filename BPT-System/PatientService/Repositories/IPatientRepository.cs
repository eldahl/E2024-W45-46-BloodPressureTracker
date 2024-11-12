using Models;

namespace PatientService.Repositories;

public interface IPatientRepository
{
    Task<Patient> GetBySsnAsync(string ssn, CancellationToken ct);
    Task<MeasurementsOfPatientDto> GetMeasurementsOfPatientAsync(string patientSsn, CancellationToken ct); 
    Task AddAsync(Patient patient, CancellationToken ct);
    Task UpdateAsync(Patient patient, CancellationToken ct);
    Task DeleteAsync(Patient patient, CancellationToken ct);
    Task DeleteBySsnAsync(string ssn, CancellationToken ct);
}