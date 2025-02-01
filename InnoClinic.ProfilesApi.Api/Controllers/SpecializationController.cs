using InnoClinic.Prof.BusinessLogic.Dto.Specialization;
using InnoClinic.Prof.BusinessLogic.Services.SpecializationService;
using InnoClinic.Prof.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.ProfilesApi.Controllers;

[ApiController]
[Route("api/specialization")]
public class SpecializationController(
    ISpecializationService specializationService
    ) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] QueryObject query)
    {
        var res = await specializationService.GetAllSpecializations(query);
        return Ok(res);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var res = await specializationService.GetSpecializationInfo(id.ToString());
        return Ok(res);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSpecializationDto dto)
    {
        var res = await specializationService.CreateSpecialization(dto);
        return Ok(res);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateSpecializationDto dto)
    {
        var res = await specializationService.UpdateSpecialization(id.ToString(), dto);
        return Ok(res);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var res = await specializationService.DeleteSpecialization(id.ToString());
        return Ok(res);
    }
}