using InnoClinic.AppointmentApi.BL.Dto.Appointment;
using InnoClinic.AppointmentApi.BL.Mappers;
using InnoClinic.AppointmentApi.DataAccess.Entity;
using InnoClinic.AppointmentApi.DataAccess.Models;
using InnoClinic.AppointmentApi.DataAccess.Repositories.AppointmentRepository;

namespace InnoClinic.AppointmentApi.BL.Services.AppointmentService;

public class AppointmentService(IAppointmentRepository appointmentRepository) : IAppointmentService
{
    public async Task<List<ShowAppointmentResponse>> GetAllAppointments(QueryObject query)
    {
        var doctors = await appointmentRepository.GetAllAsync(query);
        var res = doctors.Select(x => x.MapShowAppointmentResponse()).ToList();
        return res;
    }
    
    public async Task<AppointmentInfoResponse> GetAppointmentInfo(string id)
    {
        var doctor = await appointmentRepository.GetByIdAsync(id);
        if (doctor is null)
        {
            throw new NullReferenceException("Doctor not found");
        }
        return doctor.MapAppointmentInfoResponse();
    }
    
    public async Task<Appointment> CreateAppointment(CreateAppointmentRequest request)
    {
        var doctor = new Appointment
        {
            Id = Guid.NewGuid().ToString(),
            DoctorId = request.DoctorId,
            ServiceId = request.ServiceId,
            Date = request.Date,
            Time = request.Time,
            IsApproved = request.IsApproved
        };
            
        return await appointmentRepository.Add(doctor);
    }
    
    public async Task<ShowAppointmentResponse> UpdateAppointment(string id, UpdateAppointmentRequest request)
    {
        var dbDoctor = await appointmentRepository.GetByIdAsync(id);
        if (dbDoctor == null)
        { 
            throw new NullReferenceException("Appointment not found");
        }
        
        dbDoctor.DoctorId = request.DoctorId;
        dbDoctor.ServiceId = request.ServiceId;
        dbDoctor.Date = request.Date;
        dbDoctor.Time = request.Time;
        dbDoctor.IsApproved = request.IsApproved;
        
        var res = await appointmentRepository.Update(dbDoctor);

        return res.MapShowAppointmentResponse();
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