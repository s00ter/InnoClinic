using InnoClinic.Prof.BusinessLogic.Dto.Doctor;
using InnoClinic.Prof.BusinessLogic.Services.DoctorService;
using InnoClinic.Prof.DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.ProfilesApi.Controllers;

[ApiController]
[Authorize(Policy = "OnlyForMembers")]
[Route("api/doctor")]
public class DoctorController(
    IDoctorService doctorService
    ) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] QueryObject query)
    {
        var res = await doctorService.GetAllDoctors(query);
        return Ok(res);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var doctorInfo = await doctorService.GetDoctorInfo(id);
        return Ok(doctorInfo);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RegistrationDoctorRequest request)
    {
        var res = await doctorService.CreateDoctor(request);
        return Ok(res);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateDoctorRequest request)
    {
        var prod = await doctorService.UpdateDoctor(id, request);
        return Ok(prod);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var res = await doctorService.DeleteDoctor(id);
        return Ok(res);
    }
}