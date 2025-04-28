using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Prof.DataAccess.Repositories.ReceptionistRepository;

public class ReceptionistRepository(InnoClinicProfContext context) : IReceptionistRepository
{
    public async Task<Receptionist> Add(Receptionist patient)
    {
        await context.Receptionists.AddAsync(patient);
        await context.SaveChangesAsync();
        return patient;
    }

    public async Task<Receptionist> Update(Receptionist patient)
    {
        context.Receptionists.Update(patient);
        await context.SaveChangesAsync();
        return patient;
    }

    public Task UpdateRange(List<Receptionist> patients)
    {
        context.Receptionists.UpdateRange(patients);
        return context.SaveChangesAsync();
    }

    public async Task<Receptionist?> Delete(Guid id)
    {
        var res = await context.Receptionists.FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception("Receptionist not found");;
        context.Receptionists.Remove(res);
        await context.SaveChangesAsync();
        return res;
    }
    
    public async Task<Receptionist?> GetByIdAsync(Guid id)
    {
        var res =  await context.Receptionists.FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception("Receptionist not found");
        return res;
    }
    
    public async Task<List<Receptionist>> GetAllAsync(QueryPaginationArguments queryPagination)
    {
        var receptionists = context.Receptionists.AsQueryable();
        
        var skipNumber = (queryPagination.PageNumber - 1) * queryPagination.PageSize;

        return receptionists.Skip(skipNumber).Take(queryPagination.PageSize).ToList();
    }
}