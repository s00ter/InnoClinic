using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Prof.DataAccess.Models;

namespace InnoClinic.Prof.DataAccess.Repositories.PatientRepository;

public interface IPatientRepository
{
    Task<Patient> Add(Patient patient);
    Task<Patient> Update(Patient patients);
    Task UpdateRange(List<Patient> patients);
    Task<Patient?> Delete(string id);
    Task<Patient?> GetByIdAsync(string id);
    Task<List<Patient>> GetAllAsync(QueryObject query);
}