using InnoClinic.AppointmentApi.BL.Dto.AppointmentDto;
using InnoClinic.AppointmentApi.DataAccess.Entity;

namespace InnoClinic.AppointmentApi.BL.Mappers;

public static class AppointmentMapper
{
    public static ShowAppointmentResponse MapShowAppointmentResponse(this Appointment appointment)
    {
        return new ShowAppointmentResponse
        {
            Id = appointment.Id,
            DoctorId = appointment.DoctorId,
            ServiceId = appointment.ServiceId,
            DateTimeOffset = appointment.DateTimeOffset,
            IsApproved = appointment.IsApproved
        };
    }
    
    public static AppointmentInfoResponse MapAppointmentInfoResponse(this Appointment appointment)
    {
        return new AppointmentInfoResponse
        {
            Id = appointment.Id,
            DoctorId = appointment.DoctorId,
            ServiceId = appointment.ServiceId,
            AppointmentDate = appointment.DateTimeOffset,
            IsApproved = appointment.IsApproved,
            Result = appointment.Result
        };
    }
}