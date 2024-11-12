using Models;

namespace MeasurementService.Repositories;

public interface IMeasurementRepository
{
    Task<Measurement> GetByIdAsync(int id, CancellationToken ct);
    Task AddAsync(Measurement measurement, CancellationToken ct);
    Task UpdateAsync(Measurement measurement, CancellationToken ct);
    Task DeleteAsync(Measurement measurement, CancellationToken ct);
    Task DeleteByIdAsync(int id, CancellationToken ct);
}