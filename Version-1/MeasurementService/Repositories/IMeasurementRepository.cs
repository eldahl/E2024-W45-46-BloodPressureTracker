using Models;

namespace MeasurementService.Controllers;

public interface IMeasurementRepository
{
    Measurement GetById(int id);
    void Add(Measurement measurement);
    void Update(Measurement measurement);
    void Delete(Measurement measurement);
    void DeleteById(int id);
}