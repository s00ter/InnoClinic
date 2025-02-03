using InnoClinic.Prof.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Prof.DataAccess;

public class InnoClinicProfContext(DbContextOptions<InnoClinicProfContext> options) : DbContext(options)
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Specialization> Specializations { get; set; }
    public DbSet<Receptionist> Receptionists { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
    }
}