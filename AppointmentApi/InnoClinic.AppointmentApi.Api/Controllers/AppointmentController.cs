using InnoClinic.AppointmentApi.BL.Dto.Appointment;
using InnoClinic.AppointmentApi.BL.Services.AppointmentService;
using InnoClinic.AppointmentApi.DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.AppointmentApi.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/appointment")]
public class AppointmentController(
    IAppointmentService appointmentService
    ) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] QueryPaginationArguments queryPagination, CancellationToken cancellationToken)
    {
        var res = await appointmentService.GetAllAppointments(queryPagination, cancellationToken);
        return Ok(res);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var res = await appointmentService.GetAppointmentInfo(id, cancellationToken);
        return Ok(res);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAppointmentRequest request, CancellationToken cancellationToken)
    {
        var res = await appointmentService.CreateAppointment(request, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = res.Id }, res);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateAppointmentRequest request, CancellationToken cancellationToken)
    {
        await appointmentService.UpdateAppointment(id, request, cancellationToken);
        return StatusCode(StatusCodes.Status204NoContent);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await appointmentService.DeleteAppointment(id, cancellationToken);
        return StatusCode(StatusCodes.Status204NoContent);
    }
}