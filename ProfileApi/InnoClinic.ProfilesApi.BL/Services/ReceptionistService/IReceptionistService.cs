using System.Collections.Frozen;
using InnoClinic.Prof.BusinessLogic.Dto.Receptionist;
using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Shared.Models;

namespace InnoClinic.Prof.BusinessLogic.Services.ReceptionistService;

public interface IReceptionistService
{
    Task<FrozenSet<ShowReceptionistResponse>> GetAllReceptionists(QueryPaginationArguments queryPagination);
    Task<ReceptionistInfoResponse> GetReceptionistInfo(Guid id);
    Task<Receptionist> CreateReceptionist(RegistrationReceptionistRequest request);
    Task<ShowReceptionistResponse> UpdateReceptionist(Guid id, UpdateReceptionistRequest request);
    Task<Receptionist?> DeleteReceptionist(Guid id);
}