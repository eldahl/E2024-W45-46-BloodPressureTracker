using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Models.Migrations
{
    /// <inheritdoc />
    public partial class MeasuermentIdidentitymoredataseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Measurements",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.UpdateData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "SSN", "Mail", "Name" },
                values: new object[,]
                {
                    { "144-75-2929", "PAO@mail.com", "Rebeca Pao" },
                    { "178-14-0036", "RHusson@mail.com", "Raghallach Husson" },
                    { "500-29-2239", "xy@mail.com", "Xena Yun" },
                    { "509-90-5304", "Clayton@mail.com", "Helana Clayton" }
                });

            migrationBuilder.InsertData(
                table: "Measurements",
                columns: new[] { "Id", "Date", "Diastolic", "PatientSsn", "Seen", "Systolic" },
                values: new object[,]
                {
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 200, "144-75-2929", false, 72 },
                    { 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 198, "178-14-0036", false, 64 },
                    { 4, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 120, "500-29-2239", false, 76 },
                    { 5, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 174, "509-90-5304", false, 89 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "SSN",
                keyValue: "144-75-2929");

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "SSN",
                keyValue: "178-14-0036");

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "SSN",
                keyValue: "500-29-2239");

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "SSN",
                keyValue: "509-90-5304");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                table: "Measurements",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.UpdateData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateOnly(2024, 1, 1));
        }
    }
}
