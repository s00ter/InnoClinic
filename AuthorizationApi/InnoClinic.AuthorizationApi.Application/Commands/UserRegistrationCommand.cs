using InnoClinic.Application.Dto.Account;
using MediatR;

namespace InnoClinic.Application.Commands;

public record UserRegistrationCommand(UserRegistrationRequest Request) : IRequest<bool>;