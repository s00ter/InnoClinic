using InnoClinic.Prof.BusinessLogic.Dto.Receptionist;
using InnoClinic.Prof.BusinessLogic.Services.ReceptionistService;
using InnoClinic.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.ProfilesApi.Controllers;

[ApiController]
[Authorize(Policy = "OnlyForMembers")]
[Route("api/receptionists")]
public class ReceptionistController(
    IReceptionistService receptionistService
    ) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] QueryPaginationArguments queryPagination)
    {
        var res = await receptionistService.GetAllReceptionists(queryPagination);
        return Ok(res);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var res = await receptionistService.GetReceptionistInfo(id);
        return Ok(res);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RegistrationReceptionistRequest request)
    {
        var res = await receptionistService.CreateReceptionist(request);
        return CreatedAtAction(nameof(Get), new { id = res.Id }, res);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateReceptionistRequest request)
    {
        await receptionistService.UpdateReceptionist(id, request);
        return StatusCode(StatusCodes.Status204NoContent);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await receptionistService.DeleteReceptionist(id);
        return StatusCode(StatusCodes.Status204NoContent);
    }
}