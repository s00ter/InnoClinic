using MediatR;
using Microsoft.AspNetCore.Http;

namespace InnoClinic.Application.Commands;

public record RefreshAccessTokenCommand(HttpContext Context) : IRequest<Unit>;