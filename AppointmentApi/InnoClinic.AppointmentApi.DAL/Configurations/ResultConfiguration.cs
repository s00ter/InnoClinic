using InnoClinic.AppointmentApi.DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InnoClinic.AppointmentApi.DataAccess.Configurations;

internal class ResultConfiguration : IEntityTypeConfiguration<Result>
{
    public void Configure(EntityTypeBuilder<Result> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Complaints).IsRequired();
        builder.Property(x => x.Conclusion).IsRequired();
        builder.Property(x => x.Recommendations).IsRequired();
        builder.Property(x => x.AppointmentId).IsRequired();
    }
}