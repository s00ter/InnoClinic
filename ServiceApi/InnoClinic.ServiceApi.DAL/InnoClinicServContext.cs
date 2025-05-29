using InnoClinic.ServiceApi.DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.ServiceApi.DataAccess;

public class InnoClinicServContext(DbContextOptions<InnoClinicServContext> options) : DbContext(options)
{
    public DbSet<Service> Services { get; set; }
    public DbSet<ServiceCategory> ServiceCategories { get; set; }
}