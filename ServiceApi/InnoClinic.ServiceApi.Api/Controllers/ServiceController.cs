using InnoClinic.ServiceApi.BusinessLogic.Dto.Service;
using InnoClinic.ServiceApi.BusinessLogic.Services.ServiceService;
using InnoClinic.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.ServiceApi.Api.Controllers;

[ApiController]
[Authorize(Policy = "OnlyForMembers")]
[Route("api/services")]
public class ServiceController(
    IServiceService serviceService
    ) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] QueryPaginationArguments query, 
        CancellationToken cancellationToken)
    {
        var res = await serviceService.GetAllServicesAsync(query, cancellationToken);
        return Ok(res);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id, 
        CancellationToken cancellationToken)
    {
        var res = await serviceService.GetServiceInfoAsync(id, cancellationToken);
        return Ok(res);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateServiceRequest request, 
        CancellationToken cancellationToken)
    {
        var res = await serviceService.CreateServiceAsync(request, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = res.Id }, res);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateServiceRequest request, 
        CancellationToken cancellationToken)
    {
        await serviceService.UpdateServiceAsync(id, request, cancellationToken);
        return StatusCode(StatusCodes.Status204NoContent);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, 
        CancellationToken cancellationToken)
    {
        await serviceService.DeleteServiceAsync(id, cancellationToken);
        return StatusCode(StatusCodes.Status204NoContent);
    }
}