using InnoClinic.Prof.BusinessLogic.Dto.Receptionist;
using InnoClinic.Prof.BusinessLogic.Services.ReceptionistService;
using InnoClinic.Prof.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.ProfilesApi.Controllers;

[ApiController]
[Route("api/receptionist")]
public class ReceptionistController(
    IReceptionistService receptionistService
    ) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] QueryObject query)
    {
        var res = await receptionistService.GetAllReceptionists(query);
        return Ok(res);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var res = await receptionistService.GetReceptionistInfo(id.ToString());
        return Ok(res);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateReceptionistDto dto)
    {
        var res = await receptionistService.CreateReceptionist(dto);
        return Ok(res);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateReceptionistDto dto)
    {
        var res = await receptionistService.UpdateReceptionist(id.ToString(), dto);
        return Ok(res);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var res = await receptionistService.DeleteReceptionist(id.ToString());
        return Ok(res);
    }
}