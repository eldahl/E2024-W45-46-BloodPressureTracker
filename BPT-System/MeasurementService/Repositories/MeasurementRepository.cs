using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Models;
using Polly;
using Polly.Retry;

namespace MeasurementService.Repositories;

public class MeasurementRepository : IMeasurementRepository
{
    private readonly BtpDbContext _context;
    private readonly AsyncRetryPolicy _retryPolicy;
    public MeasurementRepository(BtpDbContext context)
    {
        _context = context;
        _retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                (ex, timeSpan, retryCount) => {
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
    
    public async Task<Measurement> GetByIdAsync(int id, CancellationToken ct)
    {
        // Return patient with Patient.Id == id
        return await ExecuteAsync(async () => await _context.Measurements
            .Include(m => m.Patient)
            .Select(x => x)
            .FirstAsync(x => x.Id == id, ct));
    }

    public async Task AddAsync(Measurement measurement, CancellationToken ct)
    {
        await ExecuteAsync(async () =>
        {
            // Add to _context
            await _context.Measurements.AddAsync(measurement, ct);
            // Saves changes to db
            await _context.SaveChangesAsync(ct);
        });
    }

    public async Task UpdateAsync(Measurement measurement, CancellationToken ct)
    {
        await ExecuteAsync(async () =>
        {
            // Fetch row we want to update
            var toUpdate = await _context.Measurements.FirstAsync(m => m.Id == measurement.Id, ct);
            // Update
            toUpdate = measurement;
            _context.Measurements.Update(toUpdate);
            // Apply changes
            await _context.SaveChangesAsync(ct);
        });
    }

    public async Task DeleteAsync(Measurement measurement, CancellationToken ct)
    {
        await ExecuteAsync(async () =>
        {
            // Fetch row we want to delete
            var toDelete = await _context.Measurements.FirstAsync(m => m.Id == measurement.Id, ct);
            // Delete
            _context.Measurements.Remove(toDelete);
            // Apply changes
            await _context.SaveChangesAsync(ct);
        });
    }
    
    public async Task DeleteByIdAsync(int id, CancellationToken ct)
    {
        await ExecuteAsync(async () =>
        {
            // Fetch row we want to delete
            var toDelete = await _context.Measurements.FirstAsync(m => m.Id == id, ct);
            // Delete
            _context.Measurements.Remove(toDelete);
            // Apply changes
            await _context.SaveChangesAsync(ct);
        });
    }
}