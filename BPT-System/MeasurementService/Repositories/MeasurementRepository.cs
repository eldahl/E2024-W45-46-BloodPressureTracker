using Microsoft.EntityFrameworkCore;
using Models;

namespace MeasurementService.Repositories;

public class MeasurementRepository(BtpDbContext context) : IMeasurementRepository
{
    public async Task<Measurement> GetByIdAsync(int id, CancellationToken ct)
    {
        // Return patient with Patient.Id == id
        return await context.Measurements
            .Include(m => m.Patient)
            .Select(x => x)
            .FirstAsync(x => x.Id == id, ct);
    }

    public async Task AddAsync(Measurement measurement, CancellationToken ct)
    {
        // Add to context
        await context.Measurements.AddAsync(measurement, ct);
        // Saves changes to db
        await context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Measurement measurement, CancellationToken ct)
    {
        // Fetch row we want to update
        var toUpdate = await context.Measurements.FirstAsync(m => m.Id == measurement.Id, ct);
        // Update
        toUpdate = measurement;
        context.Measurements.Update(toUpdate);
        // Apply changes
        await context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Measurement measurement, CancellationToken ct)
    {
        // Fetch row we want to delete
        var toDelete = await context.Measurements.FirstAsync(m => m.Id == measurement.Id, ct);
        // Delete
        context.Measurements.Remove(toDelete);
        // Apply changes
        await context.SaveChangesAsync(ct);
    }
    
    public async Task DeleteByIdAsync(int id, CancellationToken ct)
    {
        // Fetch row we want to delete
        var toDelete = await context.Measurements.FirstAsync(m => m.Id == id, ct);
        // Delete
        context.Measurements.Remove(toDelete);
        // Apply changes
        await context.SaveChangesAsync(ct);
    }
}