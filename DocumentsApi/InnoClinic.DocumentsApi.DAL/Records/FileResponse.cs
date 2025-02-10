namespace InnoClinic.DocumentsApi.DAL.Records;

public record FileResponse (Stream Stream,string ContentType, string? FileName);