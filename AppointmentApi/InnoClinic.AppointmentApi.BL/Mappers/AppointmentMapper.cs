using InnoClinic.AppointmentApi.BL.Dto.Appointment;
using InnoClinic.AppointmentApi.DataAccess.Entity;

namespace InnoClinic.AppointmentApi.BL.Mappers;

public static class AppointmentMapper
{
    public static ShowAppointmentDto MapShowAppointmentDto(this Appointment doctor)
    {
        return new ShowAppointmentDto
        {
            Id = doctor.Id,
            DoctorId = doctor.DoctorId,
            ServiceId = doctor.ServiceId,
            Date = doctor.Date,
            Time = doctor.Time,
            IsApproved = doctor.IsApproved
        };
    }
    
    public static AppointmentInfoDto MapAppointmentInfoDto(this Appointment doctor)
    {
        return new AppointmentInfoDto
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