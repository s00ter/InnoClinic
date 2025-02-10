using InnoClinic.AppointmentApi.BL.Dto.Appointment;
using InnoClinic.AppointmentApi.BL.Mappers;
using InnoClinic.AppointmentApi.DataAccess.Entity;
using InnoClinic.AppointmentApi.DataAccess.Models;
using InnoClinic.AppointmentApi.DataAccess.Repositories.AppointmentRepository;

namespace InnoClinic.AppointmentApi.BL.Services.AppointmentService;

public class AppointmentService(IAppointmentRepository appointmentRepository) : IAppointmentService
{
    public async Task<List<ShowAppointmentDto>> GetAllAppointments(QueryObject query)
    {
        var doctors = await appointmentRepository.GetAllAsync(query);
        var res = doctors.Select(x => x.MapShowAppointmentDto()).ToList();
        return res;
    }
    
    public async Task<AppointmentInfoDto> GetAppointmentInfo(string id)
    {
        var doctor = await appointmentRepository.GetByIdAsync(id);
        if (doctor is null)
        {
            throw new NullReferenceException("Doctor not found");
        }
        return doctor.MapAppointmentInfoDto();
    }
    
    public async Task<Appointment> CreateAppointment(CreateAppointmentDto dto)
    {
        var doctor = new Appointment
        {
            Id = Guid.NewGuid().ToString(),
            DoctorId = dto.DoctorId,
            ServiceId = dto.ServiceId,
            Date = dto.Date,
            Time = dto.Time,
            IsApproved = dto.IsApproved
        };
            
        return await appointmentRepository.Add(doctor);
    }
    
    public async Task<ShowAppointmentDto> UpdateAppointment(string id, UpdateAppointmentDto dto)
    {
        var dbDoctor = await appointmentRepository.GetByIdAsync(id);
        if (dbDoctor == null)
        { 
            throw new NullReferenceException("Appointment not found");
        }
        
        dbDoctor.DoctorId = dto.DoctorId;
        dbDoctor.ServiceId = dto.ServiceId;
        dbDoctor.Date = dto.Date;
        dbDoctor.Time = dto.Time;
        dbDoctor.IsApproved = dto.IsApproved;
        
        var res = await appointmentRepository.Update(dbDoctor);

        return res.MapShowAppointmentDto();
    }
    
    public async Task<Appointment?> DeleteAppointment(string id)
    {
        var dbDoctor = await appointmentRepository.Delete(id);
        if (dbDoctor == null)
        {
            throw new NullReferenceException("Appointment not found");
        }
        return dbDoctor;
    }
}