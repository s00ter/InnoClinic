namespace InnoClinic.ServiceApi.BusinessLogic.Dto.Service;

public class ServiceInfoResponse
{
    public Guid Id { get; init; }
    public Guid CategoryId { get; init; }
    public string Name { get; init; }
    public float Price { get; init; }
    public Guid SpecializationId { get; init; }
    public bool IsActive { get; init; }
}