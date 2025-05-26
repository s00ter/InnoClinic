using InnoClinic.ServiceApi.BusinessLogic.Dto.Service;
using InnoClinic.ServiceApi.DataAccess.Entity;

namespace InnoClinic.ServiceApi.BusinessLogic.Mappers;

public static class ServiceMapper
{
    public static ShowServiceResponse MapShowServiceDto(this Service service)
    {
        return new ShowServiceResponse
        {
            Id = service.Id,
            CategoryId = service.CategoryId,
            Name = service.Name,
            Price = service.Price,
            SpecializationId = service.SpecializationId,
            IsActive = service.IsActive
        };
    }
    
    public static ServiceInfoResponse MapServiceInfoDto(this Service service)
    {
        return new ServiceInfoResponse
        {
            Id = service.Id,
            CategoryId = service.CategoryId,
            Name = service.Name,
            Price = service.Price,
            SpecializationId = service.SpecializationId,
            IsActive = service.IsActive
        };
    }
}