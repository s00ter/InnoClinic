using InnoClinic.AppointmentApi.BL.Dto.ResultDto;
using InnoClinic.AppointmentApi.BL.Services.ResultService;
using InnoClinic.Shared;
using InnoClinic.Shared.Constants;
using InnoClinic.Shared.Middlewares;
using InnoClinic.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.AppointmentApi.Api.Controllers;

[ApiController]
[Authorize(Policy = "OnlyForMembers")]
[Route("api/results")]
public class ResultController(IResultService resultService) : ControllerBase
{
    [HttpGet]
    [HasPermission(Permissions.Read)]
    public async Task<IActionResult> Get([FromQuery] QueryPaginationArguments queryPagination, CancellationToken cancellationToken)
    {
        var res = await resultService.GetAllResults(queryPagination, cancellationToken);
        return Ok(res);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var res = await resultService.GetResultInfo(id, cancellationToken);
        return Ok(res);
    }
    
    [HttpPost]
    [TargetRequestType(typeof(CreateResultRequest))]
    public async Task<IActionResult> Create([FromBody] CreateResultRequest request, CancellationToken cancellationToken)
    {
        var res = await resultService.CreateResult(request, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = res.Id }, res);
    }
    
    [HttpPut("{id:guid}")]
    [TargetRequestType(typeof(UpdateResultRequest))]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateResultRequest request, CancellationToken cancellationToken)
    {
        await resultService.UpdateResult(id, request, cancellationToken);
        return StatusCode(StatusCodes.Status204NoContent);
    }
    
    [HttpDelete("{id:guid}")]
    [HasPermission(Permissions.Delete)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await resultService.DeleteResult(id, cancellationToken);
        return StatusCode(StatusCodes.Status204NoContent);
    }
}