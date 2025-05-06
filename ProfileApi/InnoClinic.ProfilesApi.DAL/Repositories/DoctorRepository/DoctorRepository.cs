using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Prof.DataAccess.Repositories.DoctorRepository;

public class DoctorRepository(InnoClinicProfContext context) : IDoctorRepository
{
    public async Task<Doctor> Add(Doctor doctor, CancellationToken cancellationToken)
    {
        await context.Doctors.AddAsync(doctor, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return doctor;
    }

    public async Task<Doctor> Update(Doctor doctor, CancellationToken cancellationToken)
    {
        context.Doctors.Update(doctor);
        await context.SaveChangesAsync(cancellationToken);
        return doctor;
    }

    public Task UpdateRange(List<Doctor> doctors, CancellationToken cancellationToken)
    {
        context.Doctors.UpdateRange(doctors);
        return context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Doctor> Delete(Guid id, CancellationToken cancellationToken)
    {
        var res = await context.Doctors.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken)
                  ?? throw new Exception("Doctor not found");
        context.Doctors.Remove(res);
        await context.SaveChangesAsync(cancellationToken);
        return res;
    }
    
    public async Task<Doctor> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var res =  await context.Doctors.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken)
                   ?? throw new Exception("Doctor not found");
        return res;
    }
    
    public async Task<List<Doctor>> GetAllAsync(QueryPaginationArguments queryPagination, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var doctors = context.Doctors.AsQueryable();
        
        var skipNumber = (queryPagination.PageNumber - 1) * queryPagination.PageSize;

        return doctors.Skip(skipNumber).Take(queryPagination.PageSize).ToList();
    }
}