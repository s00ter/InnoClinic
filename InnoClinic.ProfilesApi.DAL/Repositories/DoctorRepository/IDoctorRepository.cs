using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Prof.DataAccess.Models;

namespace InnoClinic.Prof.DataAccess.Repositories.DoctorRepository;

public interface IDoctorRepository
{
    Task<Doctor> Add(Doctor product);
    Task<Doctor> Update(Doctor product);
    Task UpdateRange(List<Doctor> products);
    Task<Doctor?> Delete(string id);
    Task<Doctor?> GetByIdAsync(string id);
    Task<List<Doctor>> GetAllAsync(QueryObject query);
}