using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Models.Migrations
{
    /// <inheritdoc />
    public partial class Initialbaseschema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    SSN = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Mail = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.SSN);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Measurements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Systolic = table.Column<int>(type: "int", nullable: false),
                    Diastolic = table.Column<int>(type: "int", nullable: false),
                    Seen = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PatientSsn = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Measurements_Patients_PatientSsn",
                        column: x => x.PatientSsn,
                        principalTable: "Patients",
                        principalColumn: "SSN",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "SSN", "Mail", "Name" },
                values: new object[] { "462-59-4864", "WMA@mail.com", "Wilber Ma" });

            migrationBuilder.InsertData(
                table: "Measurements",
                columns: new[] { "Id", "Date", "Diastolic", "PatientSsn", "Seen", "Systolic" },
                values: new object[] { 1, new DateOnly(2024, 1, 1), 170, "462-59-4864", false, 67 });

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_PatientSsn",
                table: "Measurements",
                column: "PatientSsn",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Measurements");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
