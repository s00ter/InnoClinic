using InnoClinic.BusinessLogic.Entities;
using InnoClinic.DataAccess.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.DataAccess;

public class InnoClinicAuthContext(DbContextOptions<InnoClinicAuthContext> options) : IdentityDbContext<User>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new RoleConfiguration());
    }
}