using InnoClinic.Shared.Constants;
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
            },
            new IdentityRole
            {
                Id = "e79c66dd-439c-464f-9203-f2a08088e838",
                Name = RoleConstants.Patient,
                NormalizedName = RoleConstants.Patient.ToUpper()
            },
            new IdentityRole
            {
                Id = "4ce77109-350d-4b17-a963-a630957068d2",
                Name = RoleConstants.Receptionist,
                NormalizedName = RoleConstants.Receptionist.ToUpper()
            },
            new IdentityRole
            {
                Id = "79daa1c6-e8c9-48a2-9d9f-b4d00ea73819",
                Name = RoleConstants.Doctor,
                NormalizedName = RoleConstants.Doctor.ToUpper()
            });
    }
}