using System.Linq.Expressions;
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
        CancellationToken cancellationToken,
        params Expression<Func<T, object>>[] includes
        )
    {
        var res = _dbSet.AsNoTracking().AsQueryable();

        res = includes.Aggregate(res, (current, include) => current.Include(include));

        var skipNumber = (queryPagination.PageNumber - 1) * queryPagination.PageSize;

        return res.Skip(skipNumber).Take(queryPagination.PageSize);
    }

    public async Task<T?> GetByIdAsync(
        Guid id, 
        CancellationToken cancellationToken,
        params Expression<Func<T, object>>[] includes
        )
    {
        var entityType = context.Model.FindEntityType(typeof(T));
        var primaryKey = entityType?.FindPrimaryKey();

        if (primaryKey == null)
        {
            throw new InvalidOperationException($"Entity {typeof(T).Name} does not have a primary key.");
        }
        
        var query = _dbSet.AsQueryable();

        query = includes.Aggregate(query, (current, include) => current.Include(include));

        var parameter = Expression.Parameter(typeof(T), "x");
        var keyProperty = Expression.Property(parameter, primaryKey.Properties.First().Name);
        var equalsExpression = Expression.Equal(keyProperty, Expression.Constant(id));

        var lambda = Expression.Lambda<Func<T, bool>>(equalsExpression, parameter);

        return await query.FirstOrDefaultAsync(lambda, cancellationToken);
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