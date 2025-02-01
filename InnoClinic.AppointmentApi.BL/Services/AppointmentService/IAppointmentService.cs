using InnoClinic.AppointmentApi.BL.Dto.Appointment;
using InnoClinic.AppointmentApi.DataAccess.Entity;
using InnoClinic.AppointmentApi.DataAccess.Models;

namespace InnoClinic.AppointmentApi.BL.Services.AppointmentService;

public interface IAppointmentService
{
    Task<List<ShowAppointmentDto>> GetAllAppointments(QueryObject query);
    Task<AppointmentInfoDto> GetAppointmentInfo(string id);
    Task<Appointment> CreateAppointment(CreateAppointmentDto dto);
    Task<ShowAppointmentDto> UpdateAppointment(string id, UpdateAppointmentDto dto);
    Task<Appointment?> DeleteAppointment(string id);
}