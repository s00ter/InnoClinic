using InnoClinic.AppointmentApi.DataAccess.Entity;
using InnoClinic.AppointmentApi.DataAccess.GenericRepository;

namespace InnoClinic.AppointmentApi.DataAccess.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Appointment> Appointments { get; }
    IGenericRepository<Result> Results { get; }
    
    Task<int> SaveChangesAsync();
}