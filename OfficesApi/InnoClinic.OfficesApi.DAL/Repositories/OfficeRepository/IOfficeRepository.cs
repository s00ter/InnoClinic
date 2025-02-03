using InnoClinic.OfficesApi.DataAccess.Models;
using MongoDB.Bson;

namespace InnoClinic.OfficesApi.DataAccess.Repositories.OfficeRepository;

public interface IOfficeRepository
{
    Task<Entities.Office> Add(Entities.Office office);
    Task<Entities.Office> Update(Entities.Office product);
    Task UpdateRange(List<Entities.Office> products);
    Task<Entities.Office?> Delete(ObjectId id);
    Task<Entities.Office?> GetByIdAsync(ObjectId id);
    Task<List<Entities.Office>> GetAllAsync(QueryObject query);
}