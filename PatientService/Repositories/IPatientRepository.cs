using Models;

namespace PatientService.Controllers;

public interface IPatientRepository
{
    Patient GetBySsn(string ssn);
    void Add(Patient patient);
    void Update(Patient patient);
    void Delete(Patient patient);
    void DeleteBySsn(string ssn);
}