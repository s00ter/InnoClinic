using InnoClinic.Application.Dto.Account;
using MediatR;

namespace InnoClinic.Application.Commands;

public record EmailVerificationCommand(EmailVerificationRequest Request) : IRequest<Unit>;