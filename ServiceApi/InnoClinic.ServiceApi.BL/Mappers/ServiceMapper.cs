using InnoClinic.ServiceApi.BusinessLogic.Dto.Service;
using InnoClinic.ServiceApi.DataAccess.Entity;

namespace InnoClinic.ServiceApi.BusinessLogic.Mappers;

public static class ServiceMapper
{
    public static ShowServiceDto MapShowServiceDto(this Service service)
    {
        return new ShowServiceDto
        {
            Id = service.Id,
            CategoryId = service.CategoryId,
            Name = service.Name,
            Price = service.Price,
            SpecializationId = service.SpecializationId,
            IsActive = service.IsActive
        };
    }
    
    public static ServiceInfoDto MapServiceInfoDto(this Service service)
    {
        return new ServiceInfoDto
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