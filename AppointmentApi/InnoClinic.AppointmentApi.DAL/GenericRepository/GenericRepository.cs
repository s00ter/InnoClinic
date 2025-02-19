using InnoClinic.AppointmentApi.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.AppointmentApi.DataAccess.GenericRepository;

public class GenericRepository<T>(
    InnoClinicAppointmentContext context
    ) : IGenericRepository<T>
    where T : class
{
    private readonly DbSet<T> _dbSet = context.Set<T>();

    public async Task<IQueryable<T>> GetAllAsync(
        QueryPaginationArguments queryPagination, 
        CancellationToken cancellationToken)
    {
        var appointments = _dbSet.AsQueryable();

        var skipNumber = (queryPagination.PageNumber - 1) * queryPagination.PageSize;

        return appointments.Skip(skipNumber).Take(queryPagination.PageSize);
    }

    public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbSet.FindAsync(id, cancellationToken) ?? throw new InvalidOperationException();
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }
}