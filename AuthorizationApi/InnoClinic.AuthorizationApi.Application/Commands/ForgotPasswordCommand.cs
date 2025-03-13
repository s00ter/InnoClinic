using InnoClinic.Application.Dto.Account;
using MediatR;

namespace InnoClinic.Application.Commands;

public record ForgotPasswordCommand(ForgotPasswordRequest Request) : IRequest;