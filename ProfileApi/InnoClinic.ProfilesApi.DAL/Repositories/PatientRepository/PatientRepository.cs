using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Prof.DataAccess.Repositories.PatientRepository;

public class PatientRepository(InnoClinicProfContext context) : IPatientRepository
{
    public async Task<Patient> Add(Patient patient, CancellationToken cancellationToken)
    {
        await context.Patients.AddAsync(patient, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return patient;
    }

    public async Task<Patient> Update(Patient patient, CancellationToken cancellationToken)
    {
        context.Patients.Update(patient);
        await context.SaveChangesAsync(cancellationToken);
        return patient;
    }

    public Task UpdateRange(List<Patient> patients, CancellationToken cancellationToken)
    {
        context.Patients.UpdateRange(patients);
        return context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Patient> Delete(Guid id, CancellationToken cancellationToken)
    {
        var res = await context.Patients.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken)
            ?? throw new Exception("Patient not found");
        
        context.Patients.Remove(res);
        await context.SaveChangesAsync(cancellationToken);
        return res;
    }
    
    public async Task<Patient> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var res =  await context.Patients.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken)
            ?? throw new Exception("Patient not found");
        return res;
    }
    
    public async Task<List<Patient>> GetAllAsync(QueryPaginationArguments queryPagination, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var patients = context.Patients.AsQueryable();
        
        var skipNumber = (queryPagination.PageNumber - 1) * queryPagination.PageSize;

        return patients.Skip(skipNumber).Take(queryPagination.PageSize).ToList();
    }
}