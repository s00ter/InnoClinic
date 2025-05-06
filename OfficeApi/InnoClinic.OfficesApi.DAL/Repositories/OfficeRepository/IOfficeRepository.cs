using InnoClinic.Shared.Models;

namespace InnoClinic.Office.DataAccess.Repositories.OfficeRepository;

public interface IOfficeRepository
{
    Task<Entities.Office> Add(Entities.Office office, 
        CancellationToken cancellationToken = default);
    Task Update(Entities.Office product, 
        CancellationToken cancellationToken = default);
    Task Delete(string id, 
        CancellationToken cancellationToken = default);
    Task<Entities.Office?> GetByIdAsync(string id, 
        CancellationToken cancellationToken = default);
    Task<List<Entities.Office>> GetAllAsync(QueryPaginationArguments queryPagination, 
        CancellationToken cancellationToken = default);
}