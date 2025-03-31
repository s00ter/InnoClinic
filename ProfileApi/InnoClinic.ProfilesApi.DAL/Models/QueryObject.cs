namespace InnoClinic.Prof.DataAccess.Models;

public class QueryObject
{
    public string? ByName { get; init; } = null;
    public bool? SortBySpecialization { get; init; } = null;
    public bool? SortByOffice { get; init; } = null;
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}