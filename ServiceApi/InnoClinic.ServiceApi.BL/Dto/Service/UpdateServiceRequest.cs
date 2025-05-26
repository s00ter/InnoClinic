namespace InnoClinic.ServiceApi.BusinessLogic.Dto.Service;

public class UpdateServiceRequest
{
    public Guid CategoryId { get; init; }
    public string Name { get; init; }
    public float Price { get; init; }
    public Guid SpecializationId { get; init; }
    public bool IsActive { get; init; }
}