using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Prof.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Prof.DataAccess.Repositories.SpecializationRepository;

public class SpecializationRepository(
    InnoClinicProfContext context
    ) : ISpecializationRepository
{
    public async Task<Specialization> Add(Specialization specialization)
    {
        await context.Specializations.AddAsync(specialization);
        await context.SaveChangesAsync();
        return specialization;
    }

    public async Task<Specialization> Update(Specialization specialization)
    {
        context.Specializations.Update(specialization);
        await context.SaveChangesAsync();
        return specialization;
    }

    public Task UpdateRange(List<Specialization> specializations)
    {
        context.Specializations.UpdateRange(specializations);
        return context.SaveChangesAsync();
    }

    public async Task<Specialization?> Delete(string id)
    {
        var res = await context.Specializations.FirstOrDefaultAsync(x => x.Id == id);
        if (res == null)
        {
            return null;
        }
        context.Specializations.Remove(res);
        await context.SaveChangesAsync();
        return res;
    }
    
    public async Task<Specialization?> GetByIdAsync(string id)
    {
        var res =  await context.Specializations
            .FirstOrDefaultAsync(x => x.Id == id);
        return res;
    }
    
    public async Task<List<Specialization>> GetAllAsync(QueryObject query)
    {
        var specializations = context.Specializations.AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(query.ByName))
        {
            specializations = specializations.Where(x => x.Name.Contains(query.ByName));
        }
        
        var skipNumber = (query.PageNumber - 1) * query.PageSize;
        
        return await Queryable.Take(Queryable.Skip(specializations, skipNumber), query.PageSize).ToListAsync();
    }
}