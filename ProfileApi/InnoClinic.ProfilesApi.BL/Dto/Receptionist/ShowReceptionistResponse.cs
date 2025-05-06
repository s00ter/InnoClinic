namespace InnoClinic.Prof.BusinessLogic.Dto.Receptionist;

public class ShowReceptionistResponse
{
    public Guid Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string MiddleName { get; init; }
    public Guid AccountId { get; init; }
    public string OfficeId { get; init; }
}