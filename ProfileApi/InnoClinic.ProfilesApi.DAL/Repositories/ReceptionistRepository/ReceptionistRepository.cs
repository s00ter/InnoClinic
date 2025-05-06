using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Prof.DataAccess.Repositories.ReceptionistRepository;

public class ReceptionistRepository(InnoClinicProfContext context) : IReceptionistRepository
{
    public async Task<Receptionist> Add(Receptionist patient, CancellationToken cancellationToken)
    {
        await context.Receptionists.AddAsync(patient, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return patient;
    }

    public async Task<Receptionist> Update(Receptionist patient, CancellationToken cancellationToken)
    {
        context.Receptionists.Update(patient);
        await context.SaveChangesAsync(cancellationToken);
        return patient;
    }

    public async Task<Receptionist> Delete(Guid id, CancellationToken cancellationToken)
    {
        var res = await context.Receptionists.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken)
            ?? throw new Exception("Receptionist not found");
        
        context.Receptionists.Remove(res);
        await context.SaveChangesAsync(cancellationToken);
        return res;
    }
    
    public async Task<Receptionist> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var res =  await context.Receptionists.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken)
            ?? throw new Exception("Receptionist not found");
        
        return res;
    }
    
    public async Task<List<Receptionist>> GetAllAsync(QueryPaginationArguments queryPagination, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var receptionists = context.Receptionists.AsQueryable();
        
        var skipNumber = (queryPagination.PageNumber - 1) * queryPagination.PageSize;

        return receptionists.Skip(skipNumber).Take(queryPagination.PageSize).ToList();
    }
}