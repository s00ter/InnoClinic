using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Prof.DataAccess.Repositories.PatientRepository;

public class PatientRepository(InnoClinicProfContext context) : IPatientRepository
{
    public async Task<Patient> Add(Patient patient)
    {
        await context.Patients.AddAsync(patient);
        await context.SaveChangesAsync();
        return patient;
    }

    public async Task<Patient> Update(Patient patient)
    {
        context.Patients.Update(patient);
        await context.SaveChangesAsync();
        return patient;
    }

    public Task UpdateRange(List<Patient> patients)
    {
        context.Patients.UpdateRange(patients);
        return context.SaveChangesAsync();
    }

    public async Task<Patient> Delete(Guid id)
    {
        var res = await context.Patients.FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception("Patient not found");
        
        context.Patients.Remove(res);
        await context.SaveChangesAsync();
        return res;
    }
    
    public async Task<Patient> GetByIdAsync(Guid id)
    {
        var res =  await context.Patients.FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception("Patient not found");
        return res;
    }
    
    public async Task<List<Patient>> GetAllAsync(QueryPaginationArguments queryPagination)
    {
        var patients = context.Patients.AsQueryable();
        
        var skipNumber = (queryPagination.PageNumber - 1) * queryPagination.PageSize;

        return patients.Skip(skipNumber).Take(queryPagination.PageSize).ToList();
    }
}