using MimeKit;

namespace InnoClinic.Application.Models.Email;

public class Message
{
    public List<MailboxAddress> To { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
    public List<EmailAttachment>? Attachments { get; set; }
    
    public Message(string?[] to, string subject, string content, List<EmailAttachment>? attachments)
    {
        To = to.Select(x => new MailboxAddress(string.Empty, x)).ToList();
        Subject = subject;
        Content = content;
        Attachments = attachments;
    }
}