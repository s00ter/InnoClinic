using InnoClinic.ServiceApi.BusinessLogic.Dto.ServiceCategory;
using InnoClinic.ServiceApi.DataAccess.Entity;

namespace InnoClinic.ServiceApi.BusinessLogic.Mappers;

public static class ServiceCategoryMapper
{
    public static ShowServiceCategoryResponse MapShowServiceCategoryDto(this ServiceCategory serviceCategory)
    {
        return new ShowServiceCategoryResponse
        {
            Id = serviceCategory.Id,
            Name = serviceCategory.Name,
            TimeSlotSize = serviceCategory.TimeSlotSize
        };
    }
    
    public static ServiceCategoryInfoResponse MapServiceCategoryInfoDto(this ServiceCategory serviceCategory)
    {
        return new ServiceCategoryInfoResponse
        {
            Id = serviceCategory.Id,
            Name = serviceCategory.Name,
            TimeSlotSize = serviceCategory.TimeSlotSize
        };
    }
}