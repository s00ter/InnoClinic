using InnoClinic.Prof.BusinessLogic.Dto.Doctor;
using InnoClinic.Prof.BusinessLogic.Mappers;
using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Prof.DataAccess.Models;
using InnoClinic.Prof.DataAccess.Repositories.DoctorRepository;
using InnoClinic.Shared.Constants;
using InnoClinic.Shared.Extensions;
using InnoClinic.Shared.IventTypes;
using MassTransit;

namespace InnoClinic.Prof.BusinessLogic.Services.DoctorService;

public class DoctorService(
    IDoctorRepository doctorRepository,
    ICurrentUserInfo currentUserInfo,
    IPublishEndpoint publishEndpoint
    ) : IDoctorService
{
    public async Task<List<ShowDoctorResponse>> GetAllDoctors(QueryObject query)
    {
        var doctors = await doctorRepository.GetAllAsync(query);
        var res = doctors.Select(x => x.MapShowDoctorDto()).ToList();
        return res;
    }
    
    public async Task<DoctorInfoResponse> GetDoctorInfo(Guid id)
    {
        var doctor = await doctorRepository.GetByIdAsync(id) 
                     ?? throw new Exception("Doctor not found");
        
        return doctor.MapDoctorInfoDto();
    }
    
    public async Task<Doctor> CreateDoctor(RegistrationDoctorRequest request)
    {
        var userId = Guid.Parse(currentUserInfo.GetUserId());
        
        var doctor = new Doctor
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            DateOfBirth = request.DateOfBirth,
            SpecializationId = request.SpecializationId,
            OfficeId = request.OfficeId,
            CareerStartYear = request.CareerStartYear,
            Status = request.Status
        };
        var res = await doctorRepository.Add(doctor);
        
        await publishEndpoint.Publish<IDoctorCreated>(new
        {
            UserId = res.UserId,
            Role = RoleConstants.Doctor
        });
        
        return res;
    }
    
    public async Task<ShowDoctorResponse> UpdateDoctor(Guid id, UpdateDoctorRequest request)
    {
        var dbDoctor = await doctorRepository.GetByIdAsync(id) ?? 
                       throw new Exception("Doctor not found");
        
        dbDoctor.OfficeId = request.OfficeId;
        dbDoctor.FirstName = request.FirstName;
        dbDoctor.LastName = request.LastName;
        dbDoctor.MiddleName = request.MiddleName;
        dbDoctor.SpecializationId = request.SpecializationId;
        
        var res = await doctorRepository.Update(dbDoctor);
        
        return res.MapShowDoctorDto();
    }
    
    public async Task<Doctor?> DeleteDoctor(Guid id)
    {
        var dbDoctor = await doctorRepository.Delete(id) ?? 
                       throw new Exception("Doctor not found");
        
        return dbDoctor;
    }
}