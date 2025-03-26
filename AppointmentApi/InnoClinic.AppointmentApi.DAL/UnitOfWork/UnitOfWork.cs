using InnoClinic.AppointmentApi.DataAccess.Entity;
using InnoClinic.AppointmentApi.DataAccess.GenericRepository;

namespace InnoClinic.AppointmentApi.DataAccess.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly InnoClinicAppointmentContext _context;
    public IGenericRepository<Appointment> Appointments { get; }
    public IGenericRepository<Result> Results { get; }

    public UnitOfWork(InnoClinicAppointmentContext context)
    {
        _context = context;
        Appointments = new GenericRepository<Appointment>(_context);
        Results = new GenericRepository<Result>(_context);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}