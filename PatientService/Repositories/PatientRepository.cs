using Microsoft.EntityFrameworkCore;
using Models;

namespace PatientService.Repositories;

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
    
    public MeasurementsOfPatientDto GetMeasurementsOfPatient(string patientSsn)
    {
        Patient patient = GetBySsn(patientSsn);
        List<MeasurementClean> measurements = new List<MeasurementClean>();
        
        // Get Measurements and convert to list of measurements without patient data.
        // (As that is included already...)
        _context.Measurements
            .Select(m => m).Where(m => m.PatientSsn == patientSsn)
            .ToList()
            .ForEach(m => measurements.Add(new MeasurementClean() 
            {
                Id = m.Id,
                Date = m.Date,
                Diastolic = m.Diastolic,
                Systolic = m.Systolic,
                Seen = m.Seen
            }));
        
        return new MeasurementsOfPatientDto() 
        {
            Patient = patient,
            Measurements = measurements
        };
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