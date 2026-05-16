using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DIS.ApiTwo.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FlowLevels",
                columns: table => new
                {
                    FlowLevelId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Amount = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowLevels", x => x.FlowLevelId);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: false),
                    Birthdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "PhysicalSymptoms",
                columns: table => new
                {
                    PhysicalSymptomId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalSymptoms", x => x.PhysicalSymptomId);
                });

            migrationBuilder.CreateTable(
                name: "Cycles",
                columns: table => new
                {
                    CycleId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CycleNumber = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PersonId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cycles", x => x.CycleId);
                    table.ForeignKey(
                        name: "FK_Cycles_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DailyLogs",
                columns: table => new
                {
                    DailyLogId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CycleDay = table.Column<int>(type: "integer", nullable: false),
                    CycleId = table.Column<int>(type: "integer", nullable: false),
                    FlowLevelId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyLogs", x => x.DailyLogId);
                    table.ForeignKey(
                        name: "FK_DailyLogs_Cycles_CycleId",
                        column: x => x.CycleId,
                        principalTable: "Cycles",
                        principalColumn: "CycleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DailyLogs_FlowLevels_FlowLevelId",
                        column: x => x.FlowLevelId,
                        principalTable: "FlowLevels",
                        principalColumn: "FlowLevelId");
                });

            migrationBuilder.CreateTable(
                name: "DailyLogSymptoms",
                columns: table => new
                {
                    DailyLogId = table.Column<int>(type: "integer", nullable: false),
                    PhysicalSymptomId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyLogSymptoms", x => new { x.DailyLogId, x.PhysicalSymptomId });
                    table.ForeignKey(
                        name: "FK_DailyLogSymptoms_DailyLogs_DailyLogId",
                        column: x => x.DailyLogId,
                        principalTable: "DailyLogs",
                        principalColumn: "DailyLogId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DailyLogSymptoms_PhysicalSymptoms_PhysicalSymptomId",
                        column: x => x.PhysicalSymptomId,
                        principalTable: "PhysicalSymptoms",
                        principalColumn: "PhysicalSymptomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "FlowLevels",
                columns: new[] { "FlowLevelId", "Amount" },
                values: new object[,]
                {
                    { 1, "None" },
                    { 2, "Light" },
                    { 3, "Medium" },
                    { 4, "Heavy" }
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "PersonId", "Birthdate", "Gender", "Name" },
                values: new object[] { 1, new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Female", "Test User" });

            migrationBuilder.InsertData(
                table: "PhysicalSymptoms",
                columns: new[] { "PhysicalSymptomId", "Name" },
                values: new object[,]
                {
                    { 1, "Headache" },
                    { 2, "Cramps" },
                    { 3, "Sore breasts" },
                    { 4, "Tiredness" },
                    { 5, "Back pain" }
                });

            migrationBuilder.InsertData(
                table: "Cycles",
                columns: new[] { "CycleId", "CycleNumber", "EndDate", "PersonId", "StartDate" },
                values: new object[] { 1, 1, new DateTime(2026, 5, 28, 0, 0, 0, 0, DateTimeKind.Utc), 1, new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "DailyLogs",
                columns: new[] { "DailyLogId", "CycleDay", "CycleId", "Date", "FlowLevelId" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 2, 2, 1, new DateTime(2026, 5, 2, 0, 0, 0, 0, DateTimeKind.Utc), 2 }
                });

            migrationBuilder.InsertData(
                table: "DailyLogSymptoms",
                columns: new[] { "DailyLogId", "PhysicalSymptomId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 1, 4 },
                    { 2, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cycles_PersonId",
                table: "Cycles",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyLogs_CycleId",
                table: "DailyLogs",
                column: "CycleId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyLogs_FlowLevelId",
                table: "DailyLogs",
                column: "FlowLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyLogSymptoms_PhysicalSymptomId",
                table: "DailyLogSymptoms",
                column: "PhysicalSymptomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyLogSymptoms");

            migrationBuilder.DropTable(
                name: "DailyLogs");

            migrationBuilder.DropTable(
                name: "PhysicalSymptoms");

            migrationBuilder.DropTable(
                name: "Cycles");

            migrationBuilder.DropTable(
                name: "FlowLevels");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
