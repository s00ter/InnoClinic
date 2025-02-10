using InnoClinic.Application.Models.Email;

namespace InnoClinic.Application.IService;

public interface IEmailService
{
    Task SendEmail(Message message);
}