namespace InnoClinic.ServiceApi.BusinessLogic.Dto.Service;

public class ShowServiceResponse
{
    public Guid Id { get; init; }
    public Guid CategoryId { get; init; }
    public string Name { get; init; }
    public decimal Price { get; init; }
    public Guid SpecializationId { get; init; }
    public bool IsActive { get; init; }
}