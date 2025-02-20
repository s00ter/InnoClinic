using System.Collections.Frozen;
using InnoClinic.AppointmentApi.BL.Dto.AppointmentDto;
using InnoClinic.AppointmentApi.DataAccess.Entity;
using InnoClinic.AppointmentApi.DataAccess.Models;

namespace InnoClinic.AppointmentApi.BL.Services.AppointmentService;

public interface IAppointmentService
{
    Task<FrozenSet<ShowAppointmentResponse>> GetAllAppointments(QueryPaginationArguments queryPagination, CancellationToken cancellationToken = default);
    Task<AppointmentInfoResponse> GetAppointmentInfo(Guid id, CancellationToken cancellationToken = default);
    Task<Appointment> CreateAppointment(CreateAppointmentRequest request, CancellationToken cancellationToken = default);
    Task UpdateAppointment(Guid id, UpdateAppointmentRequest request, CancellationToken cancellationToken = default);
    Task DeleteAppointment(Guid id, CancellationToken cancellationToken = default);
}