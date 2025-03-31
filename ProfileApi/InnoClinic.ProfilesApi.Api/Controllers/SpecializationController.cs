using InnoClinic.Prof.BusinessLogic.Dto.Specialization;
using InnoClinic.Prof.BusinessLogic.Services.SpecializationService;
using InnoClinic.Prof.DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.ProfilesApi.Controllers;

[ApiController]
[Authorize(Policy = "OnlyForMembers")]
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
        var res = await specializationService.GetSpecializationInfo(id);
        return Ok(res);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSpecializationRequest request)
    {
        var res = await specializationService.CreateSpecialization(request);
        return Ok(res);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateSpecializationRequest request)
    {
        var res = await specializationService.UpdateSpecialization(id, request);
        return Ok(res);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var res = await specializationService.DeleteSpecialization(id);
        return Ok(res);
    }
}