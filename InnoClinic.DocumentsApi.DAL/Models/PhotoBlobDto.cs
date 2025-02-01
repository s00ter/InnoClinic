namespace InnoClinic.DocumentsApi.DAL.Models;

public class PhotoBlobDto
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? ContentType { get; set; }
    public long? ByteSize { get; set; }
}