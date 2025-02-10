using InnoClinic.BusinessLogic.Contants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InnoClinic.DataAccess.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole
            {
                Id = "c98d83a2-6874-4890-ae31-7bee7a653fbd",
                Name = RoleConstants.Admin,
                NormalizedName = RoleConstants.Admin.ToUpper()
            },
            new IdentityRole
            {
                Id = "c562541c-15f3-4866-b00f-8ea79185552f",
                Name = RoleConstants.User,
                NormalizedName = RoleConstants.User.ToUpper()
            });
    }
}