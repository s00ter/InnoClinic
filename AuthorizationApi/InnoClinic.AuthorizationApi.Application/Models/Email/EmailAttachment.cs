namespace InnoClinic.Application.Models.Email;

public class EmailAttachment(string fileName, byte[] content, string contentType)
{
    public string FileName { get; init; } = fileName;
    public byte[] Content { get; init; } = content;
    public string ContentType { get; init; } = contentType;
}