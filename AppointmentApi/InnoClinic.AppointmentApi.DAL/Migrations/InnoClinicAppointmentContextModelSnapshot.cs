﻿// <auto-generated />
using System;
using InnoClinic.AppointmentApi.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InnoClinic.AppointmentApi.DataAccess.Migrations
{
    [DbContext(typeof(InnoClinicAppointmentContext))]
    partial class InnoClinicAppointmentContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("InnoClinic.AppointmentApi.DAL.Entity.Appointment", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DoctorId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("boolean");

                    b.Property<string>("ServiceId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Time")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("InnoClinic.AppointmentApi.DAL.Entity.Result", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("AppointmentId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Complaints")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Conclusion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Recomindations")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Results");
                });
#pragma warning restore 612, 618
        }
    }
}
