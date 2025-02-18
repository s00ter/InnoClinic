using System.Collections.Frozen;
using InnoClinic.AppointmentApi.BL.Dto.Appointment;
using InnoClinic.AppointmentApi.BL.Mappers;
using InnoClinic.AppointmentApi.DataAccess.Entity;
using InnoClinic.AppointmentApi.DataAccess.Models;
using InnoClinic.AppointmentApi.DataAccess.UnitOfWork;

namespace InnoClinic.AppointmentApi.BL.Services.AppointmentService;

public class AppointmentService(
    IUnitOfWork unitOfWork
    ) : IAppointmentService
{
    public async Task<FrozenSet<ShowAppointmentResponse>> GetAllAppointments(
        QueryPaginationArguments queryPagination,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var appointments = await unitOfWork.Appointments.GetAllAsync(queryPagination, cancellationToken);
        var res = appointments.Select(x => x.MapShowAppointmentResponse()).ToFrozenSet();
        return res;
    }
    
    public async Task<AppointmentInfoResponse> GetAppointmentInfo(
        Guid id,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var appointment = await unitOfWork.Appointments.GetByIdAsync(id, cancellationToken);
        if (appointment is null)
        {
            throw new InvalidOperationException("Appointment not found");
        }
        return appointment.MapAppointmentInfoResponse();
    }
    
    public async Task CreateAppointment(
        CreateAppointmentRequest request,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var appointment = new Appointment
        {
            Id = Guid.NewGuid(),
            DoctorId = request.DoctorId,
            ServiceId = request.ServiceId,
            Date = request.Date,
            Time = request.Time,
            IsApproved = request.IsApproved
        };
            
        await unitOfWork.Appointments.AddAsync(appointment, cancellationToken);
        await unitOfWork.SaveChangesAsync();
    }
    
    public async Task UpdateAppointment(
        Guid id, 
        UpdateAppointmentRequest request, 
        CancellationToken cancellationToken
        )
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var appointment = await unitOfWork.Appointments.GetByIdAsync(id, cancellationToken);
        if (appointment == null)
        { 
            throw new InvalidOperationException("Appointment not found");
        }
        
        appointment.DoctorId = request.DoctorId;
        appointment.ServiceId = request.ServiceId;
        appointment.Date = request.Date;
        appointment.Time = request.Time;
        appointment.IsApproved = request.IsApproved;
        
        unitOfWork.Appointments.Update(appointment);
        await unitOfWork.SaveChangesAsync();
    }
    
    public async Task DeleteAppointment(
        Guid id, 
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var appointment = await unitOfWork.Appointments.GetByIdAsync(id, cancellationToken);
        if (appointment == null)
        {
            throw new InvalidOperationException("Appointment not found");
        }
        unitOfWork.Appointments.Delete(appointment);
        await unitOfWork.SaveChangesAsync();
    }
}