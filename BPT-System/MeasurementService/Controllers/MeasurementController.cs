using Microsoft.AspNetCore.Mvc;
using Models;
using MeasurementService.Repositories;

namespace MeasurementService.Controllers;

[ApiController]
[Route("[controller]")]
public class MeasurementController(IMeasurementRepository measurementRepository) : ControllerBase
{
    [HttpGet("GetMeasurementById")]
    public async Task<ActionResult<Measurement>> GetMeasurementById([FromQuery] int id, CancellationToken ct) 
    {
        return await measurementRepository.GetByIdAsync(id, ct);
    }

    [HttpPost("AddMeasurement")]
    public async Task<IActionResult> AddMeasurement([FromBody] Measurement measurement, CancellationToken ct)
    {
        await measurementRepository.AddAsync(measurement, ct);
        return Ok();
    }

    [HttpPut("UpdateMeasurement")]
    public async Task<IActionResult> UpdateMeasurement([FromBody] Measurement measurement, CancellationToken ct)
    {
        await measurementRepository.UpdateAsync(measurement, ct);
        return Ok();
    }
    
    [HttpDelete("DeleteMeasurement")]
    public async Task<IActionResult> DeleteMeasurement([FromBody] Measurement measurement, CancellationToken ct)
    {
        await measurementRepository.DeleteAsync(measurement, ct);
        return Ok();
    }

    [HttpDelete("DeleteMeasurementById")]
    public async Task<IActionResult> DeleteMeasurementById([FromQuery] int id, CancellationToken ct)
    {
        await measurementRepository.DeleteByIdAsync(id, ct);
        return Ok();
    }
}
