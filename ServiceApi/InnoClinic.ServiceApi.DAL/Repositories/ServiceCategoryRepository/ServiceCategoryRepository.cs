using InnoClinic.ServiceApi.DataAccess.Entity;
using InnoClinic.ServiceApi.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.ServiceApi.DataAccess.Repositories.ServiceCategoryRepository;

public class ServiceCategoryRepository(
    InnoClinicServContext context
    ) : IServiceCategoryRepository
{
    public async Task<ServiceCategory> Add(ServiceCategory service)
    {
        await context.ServiceCategories.AddAsync(service);
        await context.SaveChangesAsync();
        return service;
    }

    public async Task<ServiceCategory> Update(ServiceCategory service)
    {
        context.ServiceCategories.Update(service);
        await context.SaveChangesAsync();
        return service;
    }

    public Task UpdateRange(List<ServiceCategory> services)
    {
        context.ServiceCategories.UpdateRange(services);
        return context.SaveChangesAsync();
    }

    public async Task<ServiceCategory?> Delete(string id)
    {
        var res = await context.ServiceCategories.FirstOrDefaultAsync(x => x.Id == id);
        if (res == null)
        {
            return null;
        }
        context.ServiceCategories.Remove(res);
        await context.SaveChangesAsync();
        return res;
    }
    
    public async Task<ServiceCategory?> GetByIdAsync(string id)
    {
        var res =  await context.ServiceCategories
            .FirstOrDefaultAsync(x => x.Id == id);
        return res;
    }
    
    public async Task<List<ServiceCategory>> GetAllAsync(QueryObject query)
    {
        var services = context.ServiceCategories.AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(query.ByName))
        {
            services = services.Where(x => x.Name.Contains(query.ByName));
        }
        
        var skipNumber = (query.PageNumber - 1) * query.PageSize;
        
        return await Queryable.Take(Queryable.Skip(services, skipNumber), query.PageSize).ToListAsync();
    }
}