using System.Collections.Frozen;
using InnoClinic.Prof.BusinessLogic.Dto.Doctor;
using InnoClinic.Prof.BusinessLogic.Mappers;
using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Prof.DataAccess.Repositories.DoctorRepository;
using InnoClinic.Shared.Constants;
using InnoClinic.Shared.Extensions;
using InnoClinic.Shared.IventTypes;
using InnoClinic.Shared.Models;
using MassTransit;

namespace InnoClinic.Prof.BusinessLogic.Services.DoctorService;

public class DoctorService(
    IDoctorRepository doctorRepository,
    ICurrentUserInfo currentUserInfo,
    IPublishEndpoint publishEndpoint
    ) : IDoctorService
{
    public async Task<FrozenSet<ShowDoctorResponse>> GetAllDoctors(QueryPaginationArguments queryPagination, CancellationToken cancellationToken)
    {
        var doctors = await doctorRepository.GetAllAsync(queryPagination, cancellationToken);
        var res = doctors.Select(x => x.MapShowDoctorDto()).ToFrozenSet();
        return res;
    }
    
    public async Task<DoctorInfoResponse> GetDoctorInfo(Guid id, CancellationToken cancellationToken)
    {
        var doctor = await doctorRepository.GetByIdAsync(id, cancellationToken);
        
        return doctor.MapDoctorInfoDto();
    }
    
    public async Task<Doctor> CreateDoctor(RegistrationDoctorRequest request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(currentUserInfo.GetUserId());
        
        var doctor = new Doctor
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            DateOfBirth = request.DateOfBirth.Value,
            SpecializationId = request.SpecializationId,
            OfficeId = request.OfficeId,
            CareerStartYear = request.CareerStartYear.Value,
            Status = request.Status
        };
        var res = await doctorRepository.Add(doctor, cancellationToken);
        
        await publishEndpoint.Publish<DoctorCreated>(new
        {
            UserId = res.UserId,
            Role = RoleConstants.Doctor
        }, cancellationToken);
        
        return res;
    }
    
    public async Task<ShowDoctorResponse> UpdateDoctor(Guid id, UpdateDoctorRequest request, CancellationToken cancellationToken)
    {
        var dbDoctor = await doctorRepository.GetByIdAsync(id, cancellationToken);
        
        dbDoctor.OfficeId = request.OfficeId;
        dbDoctor.FirstName = request.FirstName;
        dbDoctor.LastName = request.LastName;
        dbDoctor.MiddleName = request.MiddleName;
        dbDoctor.SpecializationId = request.SpecializationId;
        
        var res = await doctorRepository.Update(dbDoctor, cancellationToken);
        
        return res.MapShowDoctorDto();
    }
    
    public async Task<Doctor> DeleteDoctor(Guid id, CancellationToken cancellationToken)
    {
        var dbDoctor = await doctorRepository.Delete(id, cancellationToken);
        
        return dbDoctor;
    }
}