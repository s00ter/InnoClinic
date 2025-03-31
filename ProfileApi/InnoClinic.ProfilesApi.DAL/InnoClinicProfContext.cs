using InnoClinic.Prof.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Prof.DataAccess;

public class InnoClinicProfContext(DbContextOptions<InnoClinicProfContext> options) : DbContext(options)
{
    public DbSet<Doctor> Doctors { get; private set; }
    public DbSet<Patient> Patients { get; private set; }
    public DbSet<Specialization> Specializations { get; private set; }
    public DbSet<Receptionist> Receptionists { get; private set; }
}