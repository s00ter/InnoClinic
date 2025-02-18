using InnoClinic.AppointmentApi.DataAccess.Models;

namespace InnoClinic.AppointmentApi.DataAccess.GenericRepository;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(QueryPaginationArguments queryPagination, CancellationToken cancellationToken = default);
    Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    void Update(T entity);
    void Delete(T entity);
}