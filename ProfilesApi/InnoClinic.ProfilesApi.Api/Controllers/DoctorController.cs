using InnoClinic.Prof.BusinessLogic.Dto.Doctor;
using InnoClinic.Prof.BusinessLogic.Services.DoctorService;
using InnoClinic.Prof.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.ProfilesApi.Controllers;

[ApiController]
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
        var doctorInfo = await doctorService.GetDoctorInfo(id.ToString());
        return Ok(doctorInfo);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RegistrationDoctorDto dto)
    {
        var res = await doctorService.CreateDoctor(dto);

        return Ok(res);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateDoctorDto dto)
    {
        var prod = await doctorService.UpdateDoctor(id.ToString(), dto);
        return Ok(prod);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var res = await doctorService.DeleteDoctor(id.ToString());
        return Ok(res);
    }
}