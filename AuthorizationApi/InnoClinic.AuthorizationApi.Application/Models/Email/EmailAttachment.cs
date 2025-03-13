namespace InnoClinic.Application.Models.Email;

public class EmailAttachment(string fileName, byte[] content, string contentType)
{
    public string FileName { get; set; } = fileName;
    public byte[] Content { get; set; } = content;
    public string ContentType { get; set; } = contentType;
}