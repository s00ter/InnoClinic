using InnoClinic.AppointmentApi.DataAccess.Entity;
using InnoClinic.AppointmentApi.DataAccess.Models;

namespace InnoClinic.AppointmentApi.DataAccess.Repositories.AppointmentRepository;

public interface IAppointmentRepository
{
    Task<Appointment> Add(Appointment product);
    Task<Appointment> Update(Appointment product);
    Task UpdateRange(List<Appointment> products);
    Task<Appointment?> Delete(string id);
    Task<Appointment?> GetByIdAsync(string id);
    Task<List<Appointment>> GetAllAsync(QueryObject query);
}