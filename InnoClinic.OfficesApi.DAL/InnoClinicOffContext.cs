using Microsoft.EntityFrameworkCore;

namespace InnoClinic.OfficesApi.DataAccess;

public class InnoClinicOffContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Entities.Office> Offices { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Entities.Office>();
    }
}