using InnoClinic.Application.Dto.Account;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace InnoClinic.Application.Commands;

public record UserAuthenticationCommand(UserAuthenticationRequest Request, HttpContext Context) : IRequest<Unit>;