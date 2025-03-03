using System.Collections.Frozen;
using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using InnoClinic.AppointmentApi.BL.Dto.AppointmentDto;
using InnoClinic.AppointmentApi.BL.Mappers;
using InnoClinic.AppointmentApi.DataAccess.Entity;
using InnoClinic.AppointmentApi.DataAccess.UnitOfWork;
using InnoClinic.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace InnoClinic.AppointmentApi.BL.Services.AppointmentService;

public class AppointmentService(
    IUnitOfWork unitOfWork,
    IServiceProvider serviceProvider
    ) : IAppointmentService
{
    public async Task<FrozenSet<ShowAppointmentResponse>> GetAllAppointments(
        QueryPaginationArguments queryPagination,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var appointments = await unitOfWork.Appointments.GetAllAsync(
            queryPagination, 
            cancellationToken
            );
        var res = appointments.Select(x => x.MapShowAppointmentResponse()).ToFrozenSet();
        return res;
    }
    
    public async Task<AppointmentInfoResponse> GetAppointmentInfo(
        Guid id,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var appointment = await unitOfWork.Appointments.GetByIdAsync(
            id, 
            cancellationToken, 
            x=> x.Result) 
                          ?? throw new ProblemDetailsException(new ProblemDetails() { Title = "Appointment not found" });
        
        return appointment.MapAppointmentInfoResponse();
    }
    
    public async Task<Appointment> CreateAppointment(
        CreateAppointmentRequest request,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var validator = serviceProvider.GetRequiredService<IValidator<CreateAppointmentRequest>>();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            var problemDetails = new ValidationProblemDetails(validationResult.ToDictionary())
            {
                Title = "Validation Failed",
                Status = StatusCodes.Status400BadRequest
            };
            throw new ProblemDetailsException(problemDetails);
        }
        
        var appointment = new Appointment
        {
            Id = Guid.NewGuid(),
            DoctorId = request.DoctorId,
            ServiceId = request.ServiceId,
            DateTimeOffset = request.DateTimeOffset,
            IsApproved = request.IsApproved
        };
            
        await unitOfWork.Appointments.AddAsync(appointment, cancellationToken);
        await unitOfWork.SaveChangesAsync();

        return await unitOfWork.Appointments.GetByIdAsync(appointment.Id, cancellationToken) 
               ?? throw new ProblemDetailsException(new ProblemDetails() { Title = "Appointment not found" });
    }
    
    public async Task UpdateAppointment(
        Guid id, 
        UpdateAppointmentRequest request, 
        CancellationToken cancellationToken
        )
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var validator = serviceProvider.GetRequiredService<IValidator<UpdateAppointmentRequest>>();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            var problemDetails = new ValidationProblemDetails(validationResult.ToDictionary())
            {
                Title = "Validation Failed",
                Status = StatusCodes.Status400BadRequest
            };
            throw new ProblemDetailsException(problemDetails);
        }
        
        var appointment = await unitOfWork.Appointments.GetByIdAsync(id, cancellationToken)
            ?? throw new ProblemDetailsException(new ProblemDetails() { Title = "Appointment not found" });
        
        appointment.DoctorId = request.DoctorId;
        appointment.ServiceId = request.ServiceId;
        appointment.DateTimeOffset = request.DateTimeOffset;
        appointment.IsApproved = request.IsApproved;
        
        unitOfWork.Appointments.Update(appointment);
        await unitOfWork.SaveChangesAsync();
    }
    
    public async Task DeleteAppointment(
        Guid id, 
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var appointment = await unitOfWork.Appointments.GetByIdAsync(id, cancellationToken) 
                          ?? throw new ProblemDetailsException(new ProblemDetails() { Title = "Appointment not found" });
        
        unitOfWork.Appointments.Delete(appointment);
        await unitOfWork.SaveChangesAsync();
    }
}