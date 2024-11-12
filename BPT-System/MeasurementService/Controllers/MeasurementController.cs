using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace MeasurementService.Controllers;

[ApiController]
[Route("[controller]")]
public class MeasurementController : ControllerBase
{
    private readonly IMeasurementRepository _measurementRepository;
    
    public MeasurementController(IMeasurementRepository measurementRepository)
    {
        _measurementRepository = measurementRepository;
    }

    [HttpGet("GetMeasurementById")]
    public ActionResult<Measurement> GetMeasurementById([FromQuery] int id)
    {
        return _measurementRepository.GetById(id);
    }

    [HttpPost("AddMeasurement")]
    public IActionResult AddMeasurement([FromBody] Measurement measurement)
    {
        _measurementRepository.Add(measurement);
        return Ok();
    }

    [HttpPut("UpdateMeasurement")]
    public IActionResult UpdateMeasurement([FromBody] Measurement measurement)
    {
        _measurementRepository.Update(measurement);
        return Ok();
    }
    
    [HttpDelete("DeleteMeasurement")]
    public IActionResult DeleteMeasurement([FromBody] Measurement measurement)
    {
        _measurementRepository.Delete(measurement);
        return Ok();
    }

    [HttpDelete("DeleteMeasurementById")]
    public IActionResult DeleteMeasurementById([FromQuery] int id)
    {
        _measurementRepository.DeleteById(id);
        return Ok();
    }
}
