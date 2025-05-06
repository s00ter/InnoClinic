using System.Collections.Frozen;
using InnoClinic.Prof.BusinessLogic.Dto.Receptionist;
using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Shared.Models;

namespace InnoClinic.Prof.BusinessLogic.Services.ReceptionistService;

public interface IReceptionistService
{
    Task<FrozenSet<ShowReceptionistResponse>> GetAllReceptionists(QueryPaginationArguments queryPagination, 
        CancellationToken cancellationToken = default);
    Task<ReceptionistInfoResponse> GetReceptionistInfo(Guid id, 
        CancellationToken cancellationToken = default);
    Task<Receptionist> CreateReceptionist(RegistrationReceptionistRequest request, 
        CancellationToken cancellationToken = default);
    Task<ShowReceptionistResponse> UpdateReceptionist(Guid id, UpdateReceptionistRequest request, 
        CancellationToken cancellationToken = default);
    Task<Receptionist?> DeleteReceptionist(Guid id, 
        CancellationToken cancellationToken = default);
}