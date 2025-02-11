using InnoClinic.AppointmentApi.BL.Dto.Appointment;
using InnoClinic.AppointmentApi.BL.Services.AppointmentService;
using InnoClinic.AppointmentApi.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.AppointmentApi.Api.Controllers;

[ApiController]
[Route("api/appointment")]
public class AppointmentController(IAppointmentService appointmentService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] QueryObject query)
    {
        var res = await appointmentService.GetAllAppointments(query);
        return Ok(res);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var doctorInfo = await appointmentService.GetAppointmentInfo(id.ToString());
        return Ok(doctorInfo);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAppointmentRequest request)
    {
        var res = await appointmentService.CreateAppointment(request);

        return Ok(res);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateAppointmentRequest request)
    {
        var prod = await appointmentService.UpdateAppointment(id.ToString(), request);
        return Ok(prod);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var res = await appointmentService.DeleteAppointment(id.ToString());
        return Ok(res);
    }
}