using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Prof.DataAccess.Models;
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

    public async Task<Patient?> Delete(string id)
    {
        var res = await context.Patients.FirstOrDefaultAsync(x => x.Id == id);
        if (res == null)
        {
            return null;
        }
        context.Patients.Remove(res);
        await context.SaveChangesAsync();
        return res;
    }
    
    public async Task<Patient?> GetByIdAsync(string id)
    {
        var res =  await context.Patients
            .FirstOrDefaultAsync(x => x.Id == id);
        return res;
    }
    
    public async Task<List<Patient>> GetAllAsync(QueryObject query)
    {
        var doctors = context.Patients.AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(query.ByName))
        {
            doctors = doctors.Where(x => (x.FirstName + x.MiddleName + x.LastName).Contains(query.ByName));
        }
        
        var skipNumber = (query.PageNumber - 1) * query.PageSize;
        
        return await Queryable.Take(Queryable.Skip(doctors, skipNumber), query.PageSize).ToListAsync();
    }
}