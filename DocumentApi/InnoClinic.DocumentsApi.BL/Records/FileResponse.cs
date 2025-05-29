namespace InnoClinic.DocumentsApi.BL.Records;

public record FileResponse (Stream Stream,string ContentType, string? FileName);