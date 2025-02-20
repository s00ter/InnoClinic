using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnoClinic.AppointmentApi.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddDateTimeProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Appointments");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateTimeOffset",
                table: "Appointments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTimeOffset",
                table: "Appointments");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "Appointments",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "Time",
                table: "Appointments",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }
    }
}
