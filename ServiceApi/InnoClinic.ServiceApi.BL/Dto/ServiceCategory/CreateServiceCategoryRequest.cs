namespace InnoClinic.ServiceApi.BusinessLogic.Dto.ServiceCategory;

public class CreateServiceCategoryRequest
{
    public string Name { get; init; }
    public int TimeSlotSize { get; init; }
}