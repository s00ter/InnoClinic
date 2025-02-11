using InnoClinic.AppointmentApi.DataAccess.Entity;
using InnoClinic.AppointmentApi.DataAccess.Models;

namespace InnoClinic.AppointmentApi.DataAccess.Repositories.ResultRepository;

public interface IResultRepository
{
    Task<Result> Add(Result product);
    Task<Result> Update(Result product);
    Task UpdateRange(List<Result> products);
    Task<Result?> Delete(string id);
    Task<Result?> GetByIdAsync(string id);
    Task<List<Result>> GetAllAsync(QueryObject query);
}