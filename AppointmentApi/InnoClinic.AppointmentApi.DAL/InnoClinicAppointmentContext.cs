using InnoClinic.AppointmentApi.DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.AppointmentApi.DataAccess;

public class InnoClinicAppointmentContext(DbContextOptions<InnoClinicAppointmentContext> options) : DbContext(options)
{
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Result> Results { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
    }
}