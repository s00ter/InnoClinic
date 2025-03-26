using InnoClinic.AppointmentApi.DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.AppointmentApi.DataAccess;

public class InnoClinicAppointmentContext(DbContextOptions<InnoClinicAppointmentContext> options) : DbContext(options)
{
    public DbSet<Appointment> Appointments { get; private set; }
    public DbSet<Result> Results { get; private set; }
}