namespace InnoClinic.Shared.Models;

public class QueryPaginationArguments
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}