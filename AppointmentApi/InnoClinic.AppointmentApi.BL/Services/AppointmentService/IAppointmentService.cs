using System.Collections.Frozen;
using InnoClinic.AppointmentApi.BL.Dto.Appointment;
using InnoClinic.AppointmentApi.DataAccess.Models;

namespace InnoClinic.AppointmentApi.BL.Services.AppointmentService;

public interface IAppointmentService
{
    Task<FrozenSet<ShowAppointmentResponse>> GetAllAppointments(QueryPaginationArguments queryPagination, CancellationToken cancellationToken = default);
    Task<AppointmentInfoResponse> GetAppointmentInfo(Guid id, CancellationToken cancellationToken = default);
    Task CreateAppointment(CreateAppointmentRequest request, CancellationToken cancellationToken = default);
    Task UpdateAppointment(Guid id, UpdateAppointmentRequest request, CancellationToken cancellationToken = default);
    Task DeleteAppointment(Guid id, CancellationToken cancellationToken = default);
}