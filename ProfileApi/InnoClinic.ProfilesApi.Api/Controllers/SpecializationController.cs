using InnoClinic.Prof.BusinessLogic.Dto.Specialization;
using InnoClinic.Prof.BusinessLogic.Services.SpecializationService;
using InnoClinic.Shared.Models;
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
    public async Task<IActionResult> Get([FromQuery] QueryPaginationArguments queryPagination, CancellationToken cancellationToken)
    {
        var res = await specializationService.GetAllSpecializations(queryPagination, cancellationToken);
        return Ok(res);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var res = await specializationService.GetSpecializationInfo(id, cancellationToken);
        return Ok(res);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSpecializationRequest request, CancellationToken cancellationToken)
    {
        var res = await specializationService.CreateSpecialization(request, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = res.Id }, res);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateSpecializationRequest request, CancellationToken cancellationToken)
    {
        await specializationService.UpdateSpecialization(id, request, cancellationToken);
        return StatusCode(StatusCodes.Status204NoContent);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await specializationService.DeleteSpecialization(id, cancellationToken);
        return StatusCode(StatusCodes.Status204NoContent);
    }
}