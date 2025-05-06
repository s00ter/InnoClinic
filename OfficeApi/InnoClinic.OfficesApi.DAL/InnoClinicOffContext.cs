using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Office.DataAccess;

public class InnoClinicOffContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Entities.Office> Offices { get; init; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Entities.Office>();
    }
}