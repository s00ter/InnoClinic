using InnoClinic.AppointmentApi.BL.Dto.Appointment;
using InnoClinic.AppointmentApi.DataAccess.Entity;
using InnoClinic.AppointmentApi.DataAccess.Models;

namespace InnoClinic.AppointmentApi.BL.Services.AppointmentService;

public interface IAppointmentService
{
    Task<List<ShowAppointmentResponse>> GetAllAppointments(QueryObject query);
    Task<AppointmentInfoResponse> GetAppointmentInfo(string id);
    Task<Appointment> CreateAppointment(CreateAppointmentRequest request);
    Task<ShowAppointmentResponse> UpdateAppointment(string id, UpdateAppointmentRequest request);
    Task<Appointment?> DeleteAppointment(string id);
}