using InnoClinic.BusinessLogic.Entities;

namespace InnoClinic.Application.IService;

public interface ITokenService
{
    string CreateToken(User user, IList<string> roles);
}