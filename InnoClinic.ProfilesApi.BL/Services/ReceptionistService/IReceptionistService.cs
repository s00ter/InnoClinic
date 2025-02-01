using InnoClinic.Prof.BusinessLogic.Dto.Receptionist;
using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Prof.DataAccess.Models;

namespace InnoClinic.Prof.BusinessLogic.Services.ReceptionistService;

public interface IReceptionistService
{
    Task<List<ShowReceptionistDto>> GetAllReceptionists(QueryObject query);
    Task<ReceptionistInfoDto> GetReceptionistInfo(string id);
    Task<Receptionist> CreateReceptionist(CreateReceptionistDto dto);
    Task<ShowReceptionistDto> UpdateReceptionist(string id, UpdateReceptionistDto dto);
    Task<Receptionist?> DeleteReceptionist(string id);
}