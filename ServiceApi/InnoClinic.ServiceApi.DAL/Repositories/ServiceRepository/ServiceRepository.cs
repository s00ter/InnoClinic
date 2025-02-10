using InnoClinic.ServiceApi.DataAccess.Entity;
using InnoClinic.ServiceApi.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.ServiceApi.DataAccess.Repositories.ServiceRepository;

public class ServiceRepository(InnoClinicServContext context) : IServiceRepository
{
    public async Task<Service> Add(Service service)
    {
        await context.Services.AddAsync(service);
        await context.SaveChangesAsync();
        return service;
    }

    public async Task<Service> Update(Service service)
    {
        context.Services.Update(service);
        await context.SaveChangesAsync();
        return service;
    }

    public Task UpdateRange(List<Service> services)
    {
        context.Services.UpdateRange(services);
        return context.SaveChangesAsync();
    }

    public async Task<Service?> Delete(string id)
    {
        var res = await context.Services.FirstOrDefaultAsync(x => x.Id == id);
        if (res == null)
        {
            return null;
        }
        context.Services.Remove(res);
        await context.SaveChangesAsync();
        return res;
    }
    
    public async Task<Service?> GetByIdAsync(string id)
    {
        var res =  await context.Services
            .FirstOrDefaultAsync(x => x.Id == id);
        return res;
    }
    
    public async Task<List<Service>> GetAllAsync(QueryObject query)
    {
        var services = context.Services.AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(query.ByName))
        {
            services = services.Where(x => x.Name.Contains(query.ByName));
        }
        
        var skipNumber = (query.PageNumber - 1) * query.PageSize;
        
        return await Queryable.Take(Queryable.Skip(services, skipNumber), query.PageSize).ToListAsync();
    }
}