using InnoClinic.AppointmentApi.DataAccess.Entity;
using InnoClinic.AppointmentApi.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.AppointmentApi.DataAccess.Repositories.ResultRepository;

public class ResultRepository(InnoClinicAppointmentContext context) : IResultRepository
{
    public async Task<Result> Add(Result doctor)
    {
        await context.Results.AddAsync(doctor);
        await context.SaveChangesAsync();
        return doctor;
    }

    public async Task<Result> Update(Result doctor)
    {
        context.Results.Update(doctor);
        await context.SaveChangesAsync();
        return doctor;
    }

    public Task UpdateRange(List<Result> doctors)
    {
        context.Results.UpdateRange(doctors);
        return context.SaveChangesAsync();
    }

    public async Task<Result?> Delete(string id)
    {
        var res = await context.Results.FirstOrDefaultAsync(x => x.Id == id);
        if (res == null)
        {
            return null;
        }
        context.Results.Remove(res);
        await context.SaveChangesAsync();
        return res;
    }
    
    public async Task<Result?> GetByIdAsync(string id)
    {
        var res =  await context.Results
            .FirstOrDefaultAsync(x => x.Id == id);
        return res;
    }
    
    public async Task<List<Result>> GetAllAsync(QueryObject query)
    {
        var doctors = context.Results.AsQueryable();
        
        var skipNumber = (query.PageNumber - 1) * query.PageSize;
        
        return await Queryable.Take(Queryable.Skip(doctors, skipNumber), query.PageSize).ToListAsync();
    }
}