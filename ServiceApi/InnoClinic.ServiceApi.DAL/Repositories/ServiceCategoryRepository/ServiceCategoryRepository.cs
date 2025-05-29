using InnoClinic.ServiceApi.DataAccess.Entity;
using InnoClinic.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.ServiceApi.DataAccess.Repositories.ServiceCategoryRepository;

public class ServiceCategoryRepository(
    InnoClinicServContext context
    ) : IServiceCategoryRepository
{
    public async Task<ServiceCategory> AddAsync(ServiceCategory service, 
        CancellationToken cancellationToken)
    {
        await context.ServiceCategories.AddAsync(service, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return service;
    }

    public async Task<ServiceCategory> Update(ServiceCategory service, 
        CancellationToken cancellationToken)
    {
        context.ServiceCategories.Update(service);
        await context.SaveChangesAsync(cancellationToken);
        return service;
    }

    public async Task<ServiceCategory> Delete(Guid id, 
        CancellationToken cancellationToken)
    {
        var res = await context.ServiceCategories.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken)
            ?? throw new Exception("Service category not found");
        
        context.ServiceCategories.Remove(res);
        await context.SaveChangesAsync(cancellationToken);
        return res;
    }
    
    public async Task<ServiceCategory> GetByIdAsync(Guid id, 
        CancellationToken cancellationToken)
    {
        var res =  await context.ServiceCategories.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken)
            ?? throw new Exception("Service category not found");
        
        return res;
    }
    
    public async Task<List<ServiceCategory>> GetAllAsync(QueryPaginationArguments query, 
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var services = context.ServiceCategories.AsQueryable();
        
        var skipNumber = (query.PageNumber - 1) * query.PageSize;
        
        return services.Skip(skipNumber).Take(query.PageSize).ToList();
    }
}