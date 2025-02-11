using InnoClinic.AppointmentApi.BL.Dto.Appointment;
using InnoClinic.AppointmentApi.DataAccess.Entity;

namespace InnoClinic.AppointmentApi.BL.Mappers;

public static class AppointmentMapper
{
    public static ShowAppointmentResponse MapShowAppointmentResponse(this Appointment doctor)
    {
        return new ShowAppointmentResponse
        {
            Id = doctor.Id,
            DoctorId = doctor.DoctorId,
            ServiceId = doctor.ServiceId,
            Date = doctor.Date,
            Time = doctor.Time,
            IsApproved = doctor.IsApproved
        };
    }
    
    public static AppointmentInfoResponse MapAppointmentInfoResponse(this Appointment doctor)
    {
        return new AppointmentInfoResponse
        {
            Id = doctor.Id,
            DoctorId = doctor.DoctorId,
            ServiceId = doctor.ServiceId,
            Date = doctor.Date,
            Time = doctor.Time,
            IsApproved = doctor.IsApproved
        };
    }
}