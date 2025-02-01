using InnoClinic.OfficesApi.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace InnoClinic.OfficesApi.DataAccess.Repositories.OfficeRepository;

public class OfficeRepository(InnoClinicOffContext context) : IOfficeRepository
{
    public async Task<Entities.Office> Add(Entities.Office office)
    {
        await context.Offices.AddAsync(office);
        await context.SaveChangesAsync();
        return office;
    }

    public async Task<Entities.Office> Update(Entities.Office office)
    {
        context.Offices.Update(office);
        await context.SaveChangesAsync();
        return office;
    }

    public Task UpdateRange(List<Entities.Office> offices)
    {
        context.Offices.UpdateRange(offices);
        return context.SaveChangesAsync();
    }

    public async Task<Entities.Office?> Delete(ObjectId id)
    {
        var res = await context.Offices.FirstOrDefaultAsync(x => x.Id == id);
        if (res == null)
        {
            return null;
        }
        context.Offices.Remove(res);
        await context.SaveChangesAsync();
        return res;
    }
    
    public async Task<Entities.Office?> GetByIdAsync(ObjectId id)
    {
        var res =  await context.Offices
            .FirstOrDefaultAsync(x => x.Id == id);
        return res;
    }
    
    public async Task<List<Entities.Office>> GetAllAsync(QueryObject query)
    {
        var offices = context.Offices.AsQueryable();
        
        var skipNumber = (query.PageNumber - 1) * query.PageSize;
        
        return await Queryable.Take(Queryable.Skip(offices, skipNumber), query.PageSize).ToListAsync();
    }
}