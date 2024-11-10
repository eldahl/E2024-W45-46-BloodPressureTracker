using Microsoft.EntityFrameworkCore;
using Models;

namespace PatientService.Controllers;

public class PatientRepository : IPatientRepository
{
    private readonly BtpDbContext _context;

    public PatientRepository(BtpDbContext context)
    {
        _context = context;
    }
    
    public Patient GetBySsn(string ssn)
    {
        return _context.Patients.Select(x => x).First(x => x.SSN == ssn);
    }

    public void Add(Patient patient)
    {
        _context.Patients.Add(patient);
    }

    public void Update(Patient patient)
    {
        _context.Patients.Update(patient);
    }

    public void Delete(Patient patient)
    {
        _context.Patients.Remove(patient);
    }
    
    public void DeleteBySsn(string ssn)
    {
        Patient toDelete = _context.Patients.First(x => x.SSN == ssn);
        _context.Patients.Remove(toDelete);
    }
}