using InnoClinic.Prof.BusinessLogic.Dto.Patient;
using InnoClinic.Prof.BusinessLogic.Services.PatientService;
using InnoClinic.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.ProfilesApi.Controllers;

[ApiController]
[Authorize(Policy = "OnlyForMembers")]
[Route("api/patients")]
public class PatientController(
    IPatientService patientService
    ) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] QueryPaginationArguments queryPagination)
    {
        var res = await patientService.GetAllPatients(queryPagination);
        return Ok(res);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var res = await patientService.GetPatientInfo(id);
        return Ok(res);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RegistrationPatientRequest request)
    {
        var res = await patientService.CreatePatient(request);
        return CreatedAtAction(nameof(Get), new { id = res.Id }, res);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdatePatientRequest request)
    {
        var res = await patientService.UpdatePatient(id, request);
        return StatusCode(StatusCodes.Status204NoContent);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var res = await patientService.DeletePatient(id);
        return StatusCode(StatusCodes.Status204NoContent);
    }
}