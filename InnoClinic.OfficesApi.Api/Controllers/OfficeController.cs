using InnoClinic.OfficesApi.BusinessLogic.Dto.Office;
using InnoClinic.OfficesApi.BusinessLogic.Services.OfficeService;
using InnoClinic.OfficesApi.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace InnoClinic.OfficesApi.Api.Controllers;

[ApiController]
[Route("api/office")]
public class OfficeController(
    IOfficeService officeService
    ) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] QueryObject query)
    {
        var res = await officeService.GetAllOffices(query);
        return Ok(res);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] string id)
    {
        var res = await officeService.GetOfficeInfo(new ObjectId(id));
        return Ok(res);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOfficeDto dto)
    {
        var res = await officeService.CreateOffice(dto);
        return Ok(res);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateOfficeDto dto)
    {
        var res = await officeService.UpdateOffice(new ObjectId(id), dto);
        return Ok(res);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] string id)
    {
        var res = await officeService.DeleteOffice(new ObjectId(id));
        return Ok(res);
    }
}