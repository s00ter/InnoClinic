namespace InnoClinic.ServiceApi.DataAccess.Models;

public class QueryObject
{
    public string? ByName { get; set; } = null;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}