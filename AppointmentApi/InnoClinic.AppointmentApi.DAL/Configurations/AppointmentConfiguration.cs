using InnoClinic.AppointmentApi.DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InnoClinic.AppointmentApi.DataAccess.Configurations;

internal class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.DoctorId).IsRequired();
        builder.Property(x => x.ServiceId).IsRequired();
        builder.Property(x => x.DateTimeOffset).IsRequired();
        
        builder.HasOne(a => a.Result)
            .WithOne(r => r.Appointment)
            .HasForeignKey<Result>(r => r.AppointmentId);
    }
}