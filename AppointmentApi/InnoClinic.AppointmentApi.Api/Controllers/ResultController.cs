using InnoClinic.AppointmentApi.BL.Dto.Result;
using InnoClinic.AppointmentApi.BL.Services.ResultService;
using InnoClinic.AppointmentApi.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.AppointmentApi.Api.Controllers;

[ApiController]
[Route("api/result")]
public class ResultController(IResultService resultService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] QueryObject query)
    {
        var res = await resultService.GetAllResults(query);
        return Ok(res);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var doctorInfo = await resultService.GetResultInfo(id.ToString());
        return Ok(doctorInfo);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateResultDto dto)
    {
        var res = await resultService.CreateResult(dto);

        return Ok(res);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateResultDto dto)
    {
        var prod = await resultService.UpdateResult(id.ToString(), dto);
        return Ok(prod);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var res = await resultService.DeleteResult(id.ToString());
        return Ok(res);
    }
}