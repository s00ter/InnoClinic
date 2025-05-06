using Dapper;
using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Shared.Models;

namespace InnoClinic.Prof.DataAccess.Repositories.SpecializationRepository;

public class SpecializationRepository(
    DapperContext dapperContext
    ) : ISpecializationRepository
{
    public async Task<Specialization> Add(Specialization specialization, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var query = "INSERT INTO Specializations (Id, Name, IsActive) VALUES (@Id, @Name, @IsActive)";
        using var connection = dapperContext.CreateConnection();
        connection.Open();

        using var transaction = connection.BeginTransaction();
        try
        {
            await connection.ExecuteAsync(query, specialization, transaction);
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
        var result = await GetByIdAsync(specialization.Id, cancellationToken);
        return result;
    }
    
    public async Task<Specialization> Update(Specialization specialization, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var query = "UPDATE Specializations SET Name = @Name, IsActive = @IsActive WHERE Id = @Id";
        using var connection = dapperContext.CreateConnection();
        await connection.ExecuteAsync(query, specialization);
        return await GetByIdAsync(specialization.Id, cancellationToken);
    }

    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var query = "DELETE FROM Specializations WHERE Id = @Id";
        using var connection = dapperContext.CreateConnection();
        await connection.ExecuteAsync(query, new { Id = id });
    }
    
    public async Task<Specialization> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var query = "SELECT * FROM Specializations WHERE Id = @Id";
        using var connection = dapperContext.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Specialization>(query, new { Id = id })
               ?? throw new Exception("Specialization not found");
    }
    
    public async Task<List<Specialization>> GetAllAsync(QueryPaginationArguments queryPagination, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var skipNumber = (queryPagination.PageNumber - 1) * queryPagination.PageSize;
        
        var query = "SELECT * FROM Specializations";
        using var connection = dapperContext.CreateConnection();
        return connection.QueryAsync<Specialization>(query).Result
            .Skip(skipNumber)
            .Take(queryPagination.PageSize)
            .ToList();;
    }
}