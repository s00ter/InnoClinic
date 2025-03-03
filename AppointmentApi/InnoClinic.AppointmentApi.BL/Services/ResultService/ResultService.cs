using System.Collections.Frozen;
using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using InnoClinic.AppointmentApi.BL.Dto.ResultDto;
using InnoClinic.AppointmentApi.BL.Mappers;
using InnoClinic.AppointmentApi.DataAccess.Entity;
using InnoClinic.AppointmentApi.DataAccess.UnitOfWork;
using InnoClinic.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace InnoClinic.AppointmentApi.BL.Services.ResultService;

public class ResultService(
    IUnitOfWork unitOfWork,
    IServiceProvider serviceProvider
    ) : IResultService
{
    public async Task<FrozenSet<ShowResultResponse>> GetAllResults(
        QueryPaginationArguments queryPagination, 
        CancellationToken cancellationToken)
    {
        var results = await unitOfWork.Results.GetAllAsync(queryPagination, cancellationToken);
        var res = results.Select(x => x.MapShowResultResponse()).ToFrozenSet();
        return res;
    }
    
    public async Task<ResultInfoResponse> GetResultInfo(
        Guid id, 
        CancellationToken cancellationToken)
    {
        var result = await unitOfWork.Results.GetByIdAsync(
            id, 
            cancellationToken, 
            x=> x.Appointment) 
                     ?? throw new ProblemDetailsException(new ProblemDetails() { Title = "Result not found" });
        
        
        return result.MapResultInfoResponse();
    }
    
    public async Task<Result> CreateResult(
        CreateResultRequest request, 
        CancellationToken cancellationToken)
    {
        var validator = serviceProvider.GetRequiredService<IValidator<CreateResultRequest>>();
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
        
        var result = new Result
        {
            Id = Guid.NewGuid(),
            Complaints = request.Complaints,
            Conclusion = request.Conclusion,
            Recommendations = request.Recommendations,
            AppointmentId = request.AppointmentId
        };
            
        await unitOfWork.Results.AddAsync(result, cancellationToken);
        await unitOfWork.SaveChangesAsync();

        return await unitOfWork.Results.GetByIdAsync(result.Id, cancellationToken)
               ?? throw new ProblemDetailsException(new ProblemDetails() { Title = "Result not found" });
    }
    
    public async Task UpdateResult(
        Guid id, 
        UpdateResultRequest request, 
        CancellationToken cancellationToken)
    {
        var validator = serviceProvider.GetRequiredService<IValidator<UpdateResultRequest>>();
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
        
        var result = await unitOfWork.Results.GetByIdAsync(id, cancellationToken) 
                     ?? throw new ProblemDetailsException(new ProblemDetails() { Title = "Result not found" });
        
        result.Complaints = request.Complaints;
        result.Conclusion = request.Conclusion;
        result.Recommendations = request.Recommendations;
        result.AppointmentId = request.AppointmentId;
        
        unitOfWork.Results.Update(result);
        await unitOfWork.SaveChangesAsync();
    }
    
    public async Task DeleteResult(
        Guid id, 
        CancellationToken cancellationToken)
    {
        var result = await unitOfWork.Results.GetByIdAsync(id, cancellationToken) 
                     ?? throw new ProblemDetailsException(new ProblemDetails() { Title = "Result not found" });
        
        unitOfWork.Results.Delete(result);
        await unitOfWork.SaveChangesAsync();
    }
}