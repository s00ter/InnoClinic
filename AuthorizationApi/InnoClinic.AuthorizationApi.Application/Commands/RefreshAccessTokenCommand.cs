using InnoClinic.Application.Dto.Account;
using MediatR;

namespace InnoClinic.Application.Commands;

public record RefreshAccessTokenCommand(RefreshTokenRequest Request) : IRequest<string>;