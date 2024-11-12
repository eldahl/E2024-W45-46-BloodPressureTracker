using Microsoft.EntityFrameworkCore;
using Models;

namespace PatientService.Repositories;

public class PatientRepository(BtpDbContext context) : IPatientRepository
{
    public async Task<Patient> GetBySsnAsync(string ssn, CancellationToken ct)
    {
        // Return matching unique SSN
        return await context.Patients.Select(x => x).FirstAsync(x => x.SSN == ssn, cancellationToken: ct);
    }
    
    public async Task<MeasurementsOfPatientDto> GetMeasurementsOfPatientAsync(string patientSsn, CancellationToken ct)
    {
        Patient patient = await GetBySsnAsync(patientSsn, ct);
        List<MeasurementClean> measurements = [];
        
        // Get Measurements and convert to list of measurements without patient data.
        // (As that is included already...)
        context.Measurements
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

    public async Task AddAsync(Patient patient, CancellationToken ct)
    {
        // Add to context
        await context.Patients.AddAsync(patient, ct);
        // Save changes
        await context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Patient patient, CancellationToken ct)
    {
        // Fetch row we want to update
        var toUpdate = await context.Patients.FirstAsync(p => p.SSN == patient.SSN, ct);
        // Update
        toUpdate = patient;
        // Apply changes
        await context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Patient patient, CancellationToken ct)
    {
        // Fetch row we want to delete
        var toDelete = await context.Patients.FirstAsync(p => p.SSN == patient.SSN, ct);
        // Delete
        context.Patients.Remove(toDelete);
        // Apply changes
        await context.SaveChangesAsync(ct);
    }
    
    public async Task DeleteBySsnAsync(string ssn, CancellationToken ct)
    {
        // Fetch row we want to delete
        var toDelete = await context.Patients.FirstAsync(p => p.SSN == ssn, ct);
        // Delete
        context.Patients.Remove(toDelete);
        // Apply changes
        await context.SaveChangesAsync(ct);
    }
}