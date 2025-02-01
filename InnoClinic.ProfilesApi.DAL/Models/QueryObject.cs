namespace InnoClinic.Prof.DataAccess.Models;

public class QueryObject
{
    public string? ByName { get; set; } = null;
    public bool? SortBySpecialization { get; set; } = null;
    public bool? SortByOffice { get; set; } = null;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}