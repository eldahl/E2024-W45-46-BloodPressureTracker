using System.Diagnostics;
using FeatureHubSDK;
using Microsoft.EntityFrameworkCore;
using Models;
using Polly;
using Polly.Retry;

namespace PatientService.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly BtpDbContext _context;
    private readonly AsyncRetryPolicy _retryPolicy;
    public PatientRepository(BtpDbContext context)
    {
        _context = context;
        _retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                (ex, timeSpan, retryCount) =>
                {
                    Debug.WriteLine($"Retry {retryCount} after {timeSpan.TotalSeconds} seconds due to {ex.Message}");
                });
    }

    private async Task<T> ExecuteAsync<T>(Func<Task<T>> action)
    {
        return await _retryPolicy.ExecuteAsync(action);
    }
    private async Task ExecuteAsync(Func<Task> action) 
    {
        await _retryPolicy.ExecuteAsync(action);
    }
    
    public async Task<Patient> GetBySsnAsync(string ssn, CancellationToken ct)
    {
        // Return matching unique SSN
        return await ExecuteAsync(async () => await _context.Patients.FirstAsync(x => x.SSN == ssn, ct));
    }
    
    public async Task<MeasurementsOfPatientDto> GetMeasurementsOfPatientAsync(string patientSsn, CancellationToken ct)
    {
        return await ExecuteAsync(async () =>
        {
            Patient patient = await GetBySsnAsync(patientSsn, ct);
            List<MeasurementClean> measurements = [];

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

        });
    }

    public async Task AddAsync(Patient patient, CancellationToken ct)
    {
        await ExecuteAsync(async () =>
        {
            // Add to _context
            await _context.Patients.AddAsync(patient, ct);
            // Save changes
            await _context.SaveChangesAsync(ct);
        });
    }

    public async Task UpdateAsync(Patient patient, CancellationToken ct)
    {
        await ExecuteAsync(async () =>
        {
            // Fetch row we want to update
            var toUpdate = await _context.Patients.FirstAsync(p => p.SSN == patient.SSN, ct);
            // Update
            toUpdate = patient;
            _context.Patients.Update(toUpdate);
            // Apply changes
            await _context.SaveChangesAsync(ct);
        });
    }

    public async Task DeleteAsync(Patient patient, CancellationToken ct)
    {
        await ExecuteAsync(async () =>
        {
            // Fetch row we want to delete
            var toDelete = await _context.Patients.FirstAsync(p => p.SSN == patient.SSN, ct);
            // Delete
            _context.Patients.Remove(toDelete);
            // Apply changes
            await _context.SaveChangesAsync(ct);
        });
    }
    
    public async Task DeleteBySsnAsync(string ssn, CancellationToken ct)
    {
        await ExecuteAsync(async () =>
        {
            // Fetch row we want to delete
            var toDelete = await _context.Patients.FirstAsync(p => p.SSN == ssn, ct);
            // Delete
            _context.Patients.Remove(toDelete);
            // Apply changes
            await _context.SaveChangesAsync(ct);
        });
    }
}