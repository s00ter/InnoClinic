using InnoClinic.AppointmentApi.BL.Dto.Result;
using InnoClinic.AppointmentApi.BL.Services.ResultService;
using InnoClinic.AppointmentApi.DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.AppointmentApi.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/result")]
public class ResultController(IResultService resultService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] QueryPaginationArguments queryPagination)
    {
        var res = await resultService.GetAllResults(queryPagination);
        return Ok(res);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var res = await resultService.GetResultInfo(id);
        return Ok(res);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateResultRequest request)
    {
        await resultService.CreateResult(request);
        return StatusCode(StatusCodes.Status201Created);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateResultRequest request)
    {
        await resultService.UpdateResult(id, request);
        return StatusCode(StatusCodes.Status204NoContent);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await resultService.DeleteResult(id);
        return StatusCode(StatusCodes.Status204NoContent);
    }
}