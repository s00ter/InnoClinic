using Dapper;
using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Prof.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Prof.DataAccess.Repositories.SpecializationRepository;

public class SpecializationRepository(
    InnoClinicProfContext context,
    DapperContext dapperContext
    ) : ISpecializationRepository
{
    public async Task<Specialization> Add(Specialization specialization)
    {
        var query = "INSERT INTO Specializations (Id, Name, IsActive) VALUES (@Id, @Name, @IsActive)";
        using var connection = dapperContext.CreateConnection();
        await connection.ExecuteAsync(query, specialization);
        return await GetByIdAsync(specialization.Id);
    }

    public async Task<Specialization> Update(Specialization specialization)
    {
        context.Specializations.Update(specialization);
        await context.SaveChangesAsync();
        return specialization;
    }

    public Task UpdateRange(List<Specialization> specializations)
    {
        context.Specializations.UpdateRange(specializations);
        return context.SaveChangesAsync();
    }

    public async Task<Specialization> Delete(Guid id)
    {
        var res = await context.Specializations.FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception("Specialization not found");
        context.Specializations.Remove(res);
        await context.SaveChangesAsync();
        return res;
    }
    
    public async Task<Specialization> GetByIdAsync(Guid id)
    {
        var res =  await context.Specializations.FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception("Specialization not found");
        return res;
    }
    
    public async Task<List<Specialization>> GetAllAsync(QueryObject query)
    {
        var specializations = context.Specializations.AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(query.ByName))
        {
            specializations = specializations.Where(x => x.Name.Contains(query.ByName));
        }
        
        var skipNumber = (query.PageNumber - 1) * query.PageSize;
        
        return await Queryable.Take(Queryable.Skip(specializations, skipNumber), query.PageSize).ToListAsync();
    }
}