using InnoClinic.Shared.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace InnoClinic.Office.DataAccess.Repositories.OfficeRepository;

public class OfficeRepository(InnoClinicOffContext context) : IOfficeRepository
{
    public async Task<Entities.Office> Add(Entities.Office office, 
        CancellationToken cancellationToken)
    {
        await context.Offices.AddAsync(office, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return office;
    }

    public async Task Update(Entities.Office office, 
        CancellationToken cancellationToken)
    {
        context.Offices.Update(office);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(string id, 
        CancellationToken cancellationToken)
    {
        var res = await context.Offices.FirstOrDefaultAsync(x => x.Id == new ObjectId(id), cancellationToken: cancellationToken)
            ?? throw new Exception("Office not found");
        
        context.Offices.Remove(res);
        await context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<Entities.Office?> GetByIdAsync(string id, 
        CancellationToken cancellationToken)
    {
        var res =  await context.Offices.FirstOrDefaultAsync(x => x.Id == new ObjectId(id), cancellationToken: cancellationToken)
            ?? throw new Exception("Office not found");
        
        return res;
    }
    
    public async Task<List<Entities.Office>> GetAllAsync(QueryPaginationArguments queryPagination, 
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var offices = context.Offices.AsQueryable();
        
        var skipNumber = (queryPagination.PageNumber - 1) * queryPagination.PageSize;
        
        return offices.Skip(skipNumber).Take(queryPagination.PageSize).ToList();
    }
}