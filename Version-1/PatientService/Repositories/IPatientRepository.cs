using Models;

namespace PatientService.Repositories;

public interface IPatientRepository
{
    Patient GetBySsn(string ssn);
    MeasurementsOfPatientDto GetMeasurementsOfPatient(string patientSsn); 
    void Add(Patient patient);
    void Update(Patient patient);
    void Delete(Patient patient);
    void DeleteBySsn(string ssn);
}