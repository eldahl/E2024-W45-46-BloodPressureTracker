using FeatureHubSDK;
using Microsoft.AspNetCore.Mvc;
using Models;
using MeasurementService.Repositories;

namespace MeasurementService.Controllers;

[ApiController]
[Route("[controller]")]
public class MeasurementController(IMeasurementRepository measurementRepository, IClientContext fh) : ControllerBase
{
    [HttpGet("GetMeasurementById")]
    public async Task<ActionResult<Measurement>> GetMeasurementById([FromQuery] int id, CancellationToken ct) 
    {
        if (fh["MeasurementServiceOff"].IsEnabled) {
            return NoContent();
        }
        return await measurementRepository.GetByIdAsync(id, ct);
    }

    [HttpPost("AddMeasurement")]
    public async Task<IActionResult> AddMeasurement([FromBody] Measurement measurement, CancellationToken ct)
    {
        if (fh["MeasurementServiceOff"].IsEnabled) {
            return NoContent();
        }
        await measurementRepository.AddAsync(measurement, ct);
        return Ok();
    }

    [HttpPut("UpdateMeasurement")]
    public async Task<IActionResult> UpdateMeasurement([FromBody] Measurement measurement, CancellationToken ct)
    {
        if (fh["MeasurementServiceOff"].IsEnabled) {
            return NoContent();
        }
        await measurementRepository.UpdateAsync(measurement, ct);
        return Ok();
    }
    
    [HttpDelete("DeleteMeasurement")]
    public async Task<IActionResult> DeleteMeasurement([FromBody] Measurement measurement, CancellationToken ct)
    {
        if (fh["MeasurementServiceOff"].IsEnabled) {
            return NoContent();
        }
        await measurementRepository.DeleteAsync(measurement, ct);
        return Ok();
    }

    [HttpDelete("DeleteMeasurementById")]
    public async Task<IActionResult> DeleteMeasurementById([FromQuery] int id, CancellationToken ct)
    {
        if (fh["MeasurementServiceOff"].IsEnabled) {
            return NoContent();
        }
        await measurementRepository.DeleteByIdAsync(id, ct);
        return Ok();
    }
}
