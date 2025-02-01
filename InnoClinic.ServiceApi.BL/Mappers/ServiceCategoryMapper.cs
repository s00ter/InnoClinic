using InnoClinic.ServiceApi.BusinessLogic.Dto.ServiceCategory;
using InnoClinic.ServiceApi.DataAccess.Entity;

namespace InnoClinic.ServiceApi.BusinessLogic.Mappers;

public static class ServiceCategoryMapper
{
    public static ShowServiceCategoryDto MapShowServiceCategoryDto(this ServiceCategory serviceCategory)
    {
        return new ShowServiceCategoryDto
        {
            Id = serviceCategory.Id,
            Name = serviceCategory.Name,
            TimeSlotSize = serviceCategory.TimeSlotSize
        };
    }
    
    public static ServiceCategoryInfoDto MapServiceCategoryInfoDto(this ServiceCategory serviceCategory)
    {
        return new ServiceCategoryInfoDto
        {
            Id = serviceCategory.Id,
            Name = serviceCategory.Name,
            TimeSlotSize = serviceCategory.TimeSlotSize
        };
    }
}