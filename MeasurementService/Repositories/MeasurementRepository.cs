using Microsoft.EntityFrameworkCore;
using Models;

namespace MeasurementService.Controllers;

public class MeasurementRepository : IMeasurementRepository
{
    private readonly BtpDbContext _context;

    public MeasurementRepository(BtpDbContext context)
    {
        _context = context;
    }
    
    public Measurement GetById(int id)
    {
        return _context.Measurements
            .Include(m => m.Patient)
            .Select(x => x)
            .First(x => x.Id == id);
    }

    public void Add(Measurement measurement)
    {
        _context.Measurements.Add(measurement);
    }

    public void Update(Measurement measurement)
    {
        _context.Measurements.Update(measurement);
    }

    public void Delete(Measurement measurement)
    {
        _context.Measurements.Remove(measurement);
    }
    
    public void DeleteById(int id)
    {
        Measurement toDelete = _context.Measurements.First(x => x.Id == id);
        _context.Measurements.Remove(toDelete);
    }
}