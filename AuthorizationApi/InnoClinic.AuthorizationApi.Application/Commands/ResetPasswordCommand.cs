using InnoClinic.Application.Dto.Account;
using MediatR;

namespace InnoClinic.Application.Commands;

public record ResetPasswordCommand(ResetPasswordRequest Request) : IRequest;