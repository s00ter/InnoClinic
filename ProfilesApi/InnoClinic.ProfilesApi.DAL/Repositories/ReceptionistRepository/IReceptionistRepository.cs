using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Prof.DataAccess.Models;

namespace InnoClinic.Prof.DataAccess.Repositories.ReceptionistRepository;

public interface IReceptionistRepository
{
    Task<Receptionist> Add(Receptionist patient);
    Task<Receptionist> Update(Receptionist patients);
    Task UpdateRange(List<Receptionist> patients);
    Task<Receptionist?> Delete(string id);
    Task<Receptionist?> GetByIdAsync(string id);
    Task<List<Receptionist>> GetAllAsync(QueryObject query);
}