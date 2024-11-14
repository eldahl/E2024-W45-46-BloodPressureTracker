using FeatureHubSDK;
using Microsoft.AspNetCore.Mvc;
using Models;
using PatientService.Repositories;

namespace PatientService.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientController(IPatientRepository patientRepository, IClientContext fh) : ControllerBase
{

    [HttpGet("GetPatientBySsn")]
    public async Task<ActionResult<Patient>> GetPatientBySsn([FromQuery] string ssn, CancellationToken ct)
    {
        if (fh["PatientServiceOff"].IsEnabled) {
            return NoContent();
        }

        return await patientRepository.GetBySsnAsync(ssn, ct);
    }
    
    [HttpGet("GetPatientMeasurementsBySsn")]
    public async Task<ActionResult<MeasurementsOfPatientDto>> GetPatientMeasurementsBySsn([FromQuery] string ssn, CancellationToken ct)
    {
        if (fh["PatientServiceOff"].IsEnabled) {
            return NoContent();
        }
        
        return await patientRepository.GetMeasurementsOfPatientAsync(ssn, ct);
    }
 

    [HttpPost("AddPatient")]
    public async Task<IActionResult> AddPatient([FromBody] Patient patient, CancellationToken ct)
    {
        if (fh["PatientServiceOff"].IsEnabled) {
            return NoContent();
        }
        
        await patientRepository.AddAsync(patient, ct);
        return Ok();
    }

    [HttpPut("UpdatePatient")]
    public async Task<IActionResult> UpdatePatient([FromBody] Patient patient, CancellationToken ct)
    {
        if (fh["PatientServiceOff"].IsEnabled) {
            return NoContent();
        }
        
        await patientRepository.UpdateAsync(patient, ct);
        return Ok();
    }
    
    [HttpDelete("DeletePatient")]
    public async Task<IActionResult> DeletePatient([FromBody] Patient patient, CancellationToken ct) 
    {
        if (fh["PatientServiceOff"].IsEnabled) {
            return NoContent();
        }
        
        await patientRepository.DeleteAsync(patient, ct);
        return Ok();
    }
    
    [HttpDelete("DeletePatientBySsn")]
    public async Task<IActionResult> DeletePatientBySsn([FromQuery] string ssn, CancellationToken ct) 
    {
        await patientRepository.DeleteBySsnAsync(ssn, ct);
        return Ok();
    }
    
}