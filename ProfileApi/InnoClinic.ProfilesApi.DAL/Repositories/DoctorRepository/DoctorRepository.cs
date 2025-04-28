using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Prof.DataAccess.Repositories.DoctorRepository;

public class DoctorRepository(InnoClinicProfContext context) : IDoctorRepository
{
    public async Task<Doctor> Add(Doctor doctor)
    {
        await context.Doctors.AddAsync(doctor);
        await context.SaveChangesAsync();
        return doctor;
    }

    public async Task<Doctor> Update(Doctor doctor)
    {
        context.Doctors.Update(doctor);
        await context.SaveChangesAsync();
        return doctor;
    }

    public Task UpdateRange(List<Doctor> doctors)
    {
        context.Doctors.UpdateRange(doctors);
        return context.SaveChangesAsync();
    }

    public async Task<Doctor?> Delete(Guid id)
    {
        var res = await context.Doctors.FirstOrDefaultAsync(x => x.Id == id);
        if (res == null)
        {
            return null;
        }
        context.Doctors.Remove(res);
        await context.SaveChangesAsync();
        return res;
    }
    
    public async Task<Doctor?> GetByIdAsync(Guid id)
    {
        var res =  await context.Doctors
            .FirstOrDefaultAsync(x => x.Id == id);
        return res;
    }
    
    public async Task<List<Doctor>> GetAllAsync(QueryPaginationArguments queryPagination)
    {
        var doctors = context.Doctors.AsQueryable();
        
        var skipNumber = (queryPagination.PageNumber - 1) * queryPagination.PageSize;

        return doctors.Skip(skipNumber).Take(queryPagination.PageSize).ToList();
    }
}