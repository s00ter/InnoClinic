using InnoClinic.ServiceApi.BusinessLogic.Dto.Service;
using InnoClinic.ServiceApi.BusinessLogic.Services.ServiceService;
using InnoClinic.ServiceApi.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.ServiceApi.Api.Controllers;

[ApiController]
[Route("api/service")]
public class ServiceController(IServiceService serviceService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] QueryObject query)
    {
        var res = await serviceService.GetAllServices(query);
        return Ok(res);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var res = await serviceService.GetServiceInfo(id.ToString());
        return Ok(res);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateServiceDto dto)
    {
        var res = await serviceService.CreateService(dto);
        return Ok(res);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateServiceDto dto)
    {
        var res = await serviceService.UpdateService(id.ToString(), dto);
        return Ok(res);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var res = await serviceService.DeleteService(id.ToString());
        return Ok(res);
    }
}