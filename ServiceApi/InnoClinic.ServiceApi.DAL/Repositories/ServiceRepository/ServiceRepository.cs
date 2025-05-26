using InnoClinic.ServiceApi.DataAccess.Entity;
using InnoClinic.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.ServiceApi.DataAccess.Repositories.ServiceRepository;

public class ServiceRepository(InnoClinicServContext context) : IServiceRepository
{
    public async Task<Service> Add(Service service,
        CancellationToken cancellationToken)
    {
        await context.Services.AddAsync(service, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return service;
    }

    public async Task<Service> Update(Service service,
        CancellationToken cancellationToken)
    {
        context.Services.Update(service);
        await context.SaveChangesAsync(cancellationToken);
        return service;
    }

    public async Task<Service> Delete(Guid id,
        CancellationToken cancellationToken)
    {
        var res = await context.Services.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken)
            ?? throw new Exception("Service not found");

        context.Services.Remove(res);
        await context.SaveChangesAsync(cancellationToken);
        return res;
    }

    public async Task<Service> GetByIdAsync(Guid id,
        CancellationToken cancellationToken)
    {
        var res = await context.Services.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken)
            ?? throw new Exception("Service not found");

        return res;
    }

    public async Task<List<Service>> GetAllAsync(QueryPaginationArguments query,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var services = context.Services.AsQueryable();

        var skipNumber = (query.PageNumber - 1) * query.PageSize;

        return services.Skip(skipNumber).Take(query.PageSize).ToList();
    }
}