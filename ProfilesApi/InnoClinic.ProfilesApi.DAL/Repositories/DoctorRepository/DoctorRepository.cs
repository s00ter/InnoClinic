using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Prof.DataAccess.Models;
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

    public async Task<Doctor?> Delete(string id)
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
    
    public async Task<Doctor?> GetByIdAsync(string id)
    {
        var res =  await context.Doctors
            .FirstOrDefaultAsync(x => x.Id == id);
        return res;
    }
    
    public async Task<List<Doctor>> GetAllAsync(QueryObject query)
    {
        var doctors = context.Doctors.AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(query.ByName))
        {
            doctors = doctors.Where(x => (x.FirstName + x.MiddleName + x.LastName).Contains(query.ByName));
        }
        
        if (query.SortByOffice.HasValue)
        {
            if (query.SortByOffice.Value)
            {
                doctors = doctors.OrderBy(x => x.OfficeId);
            }
            else
            {
                doctors = doctors.OrderByDescending(x => x.OfficeId);
            }
        }
        
        if (query.SortBySpecialization.HasValue)
        {
            if (query.SortBySpecialization.Value)
            {
                doctors = doctors.OrderBy(x => x.SpecializationId);
            }
            else
            {
                doctors = doctors.OrderByDescending(x => x.SpecializationId);
            }
        }
        
        var skipNumber = (query.PageNumber - 1) * query.PageSize;
        
        return await Queryable.Take(Queryable.Skip(doctors, skipNumber), query.PageSize).ToListAsync();
    }
}