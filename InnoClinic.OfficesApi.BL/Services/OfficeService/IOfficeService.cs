using InnoClinic.OfficesApi.BusinessLogic.Dto.Office;
using InnoClinic.OfficesApi.DataAccess.Models;
using MongoDB.Bson;

namespace InnoClinic.OfficesApi.BusinessLogic.Services.OfficeService;

public interface IOfficeService
{
    Task<List<ShowOfficeDto>> GetAllOffices(QueryObject query);
    Task<OfficeInfoDto> GetOfficeInfo(ObjectId id);
    Task<OfficeDto> CreateOffice(CreateOfficeDto dto);
    Task<ShowOfficeDto> UpdateOffice(ObjectId id, UpdateOfficeDto dto);
    Task<OfficeDto?> DeleteOffice(ObjectId id);
}