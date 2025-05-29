namespace InnoClinic.ServiceApi.BusinessLogic.Dto.ServiceCategory;

public class ServiceCategoryInfoResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public int TimeSlotSize { get; init; }
}