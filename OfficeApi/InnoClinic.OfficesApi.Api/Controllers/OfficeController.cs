using InnoClinic.Office.BusinessLogic.Dto.Office;
using InnoClinic.Office.BusinessLogic.Services.OfficeService;
using InnoClinic.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Office.Api.Controllers;

[ApiController]
[Authorize(Policy = "OnlyForMembers")]
[Route("api/offices")]
public class OfficeController(
    IOfficeService officeService
    ) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] QueryPaginationArguments query, 
        CancellationToken cancellationToken)
    {
        var res = await officeService.GetAllOfficesAsync(query, cancellationToken);
        return Ok(res);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] string id, 
        CancellationToken cancellationToken)
    {
        var res = await officeService.GetOfficeInfoAsync(id, cancellationToken);
        return Ok(res);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOfficeRequest request, 
        CancellationToken cancellationToken)
    {
        var res = await officeService.CreateOfficeAsync(request, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = res.Id }, res);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateOfficeRequest request, 
        CancellationToken cancellationToken)
    {
        await officeService.UpdateOfficeAsync(id, request, cancellationToken);
        return StatusCode(StatusCodes.Status204NoContent);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] string id, 
        CancellationToken cancellationToken)
    {
        await officeService.DeleteOfficeAsync(id, cancellationToken);
        return StatusCode(StatusCodes.Status204NoContent);
    }
}