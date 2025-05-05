using InnoClinic.Application.Dto.Account;
using MediatR;

namespace InnoClinic.Application.Commands;

public record RefreshAccessTokenCommand(TokenResponse Request) : IRequest<TokenResponse>;