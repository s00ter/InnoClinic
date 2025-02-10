using InnoClinic.Prof.BusinessLogic.Dto.Patient;
using InnoClinic.Prof.BusinessLogic.Services.PatientService;
using InnoClinic.Prof.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.ProfilesApi.Controllers;

[ApiController]
[Route("api/patient")]
public class PatientController(
    IPatientService patientService
    ) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] QueryObject query)
    {
        var res = await patientService.GetAllPatients(query);
        return Ok(res);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var res = await patientService.GetPatientInfo(id.ToString());
        return Ok(res);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RegistrationPatientDto dto)
    {
        var res = await patientService.CreatePatient(dto);
        return Ok(res);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdatePatientDto dto)
    {
        var res = await patientService.UpdatePatient(id.ToString(), dto);
        return Ok(res);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var res = await patientService.DeletePatient(id.ToString());
        return Ok(res);
    }
}