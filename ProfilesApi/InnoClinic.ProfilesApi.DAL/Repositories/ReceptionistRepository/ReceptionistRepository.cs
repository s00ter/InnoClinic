using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Prof.DataAccess.Models;
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

    public async Task<Receptionist?> Delete(string id)
    {
        var res = await context.Receptionists.FirstOrDefaultAsync(x => x.Id == id);
        if (res == null)
        {
            return null;
        }
        context.Receptionists.Remove(res);
        await context.SaveChangesAsync();
        return res;
    }
    
    public async Task<Receptionist?> GetByIdAsync(string id)
    {
        var res =  await context.Receptionists
            .FirstOrDefaultAsync(x => x.Id == id);
        return res;
    }
    
    public async Task<List<Receptionist>> GetAllAsync(QueryObject query)
    {
        var doctors = context.Receptionists.AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(query.ByName))
        {
            doctors = doctors.Where(x => (x.FirstName + x.MiddleName + x.LastName).Contains(query.ByName));
        }
        
        var skipNumber = (query.PageNumber - 1) * query.PageSize;
        
        return await Queryable.Take(Queryable.Skip(doctors, skipNumber), query.PageSize).ToListAsync();
    }
}