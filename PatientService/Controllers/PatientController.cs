using Microsoft.AspNetCore.Mvc;
using Models;
using PatientService.Repositories;

namespace PatientService.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientController : ControllerBase
{
    private readonly IPatientRepository _patientRepository;
    
    public PatientController(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    [HttpGet("GetPatientBySsn")]
    public ActionResult<Patient> GetPatientBySsn([FromQuery] string ssn)
    {
        return _patientRepository.GetBySsn(ssn);
    }
    
    [HttpGet("GetPatientMeasurementsBySsn")]
    public ActionResult<MeasurementsOfPatientDto> GetPatientMeasurementsBySsn([FromQuery] string ssn)
    {
        return _patientRepository.GetMeasurementsOfPatient(ssn);
    }
 

    [HttpPost("AddPatient")]
    public IActionResult AddPatient([FromBody] Patient patient)
    {
        _patientRepository.Add(patient);
        return Ok();
    }

    [HttpPut("UpdatePatient")]
    public IActionResult UpdatePatient([FromBody] Patient patient)
    {
        _patientRepository.Update(patient);
        return Ok();
    }
    
    [HttpDelete("DeletePatient")]
    public IActionResult DeletePatient([FromBody] Patient patient) {
        _patientRepository.Delete(patient);
        return Ok();
    }
    
    [HttpDelete("DeletePatientBySsn")]
    public IActionResult DeletePatientBySsn([FromQuery] string ssn) {
        _patientRepository.DeleteBySsn(ssn);
        return Ok();
    }
    
}