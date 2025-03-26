using System.Linq.Expressions;
using InnoClinic.Shared.Models;

namespace InnoClinic.AppointmentApi.DataAccess.GenericRepository;

public interface IGenericRepository<T> where T : class
{
    Task<IQueryable<T>> GetAllAsync(QueryPaginationArguments queryPagination, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includes);
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includes);
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    void Update(T entity);
    void Delete(T entity);
}