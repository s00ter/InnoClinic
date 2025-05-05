using InnoClinic.Application.Dto.Account;
using MediatR;

namespace InnoClinic.Application.Commands;

public record UserAuthenticationCommand(UserAuthenticationRequest Request) : IRequest<TokenResponse>;