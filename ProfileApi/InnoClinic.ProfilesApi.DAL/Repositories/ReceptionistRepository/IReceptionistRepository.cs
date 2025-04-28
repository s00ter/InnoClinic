using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Shared.Models;

namespace InnoClinic.Prof.DataAccess.Repositories.ReceptionistRepository;

public interface IReceptionistRepository
{
    Task<Receptionist> Add(Receptionist patient);
    Task<Receptionist> Update(Receptionist patients);
    Task UpdateRange(List<Receptionist> patients);
    Task<Receptionist?> Delete(Guid id);
    Task<Receptionist?> GetByIdAsync(Guid id);
    Task<List<Receptionist>> GetAllAsync(QueryPaginationArguments queryPagination);
}