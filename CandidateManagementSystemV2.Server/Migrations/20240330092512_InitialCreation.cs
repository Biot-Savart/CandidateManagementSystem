using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CandidateManagementSystemV2.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    CandidateId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Experience = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Archived = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.CandidateId);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    PositionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Archived = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.PositionId);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    SkillId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    YearsOfExperience = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Archived = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CandidateId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.SkillId);
                    table.ForeignKey(
                        name: "FK_Skills_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "CandidateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CandidatePositions",
                columns: table => new
                {
                    CandidateId = table.Column<int>(type: "integer", nullable: false),
                    PositionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidatePositions", x => new { x.CandidateId, x.PositionId });
                    table.ForeignKey(
                        name: "FK_CandidatePositions_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "CandidateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidatePositions_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "PositionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Candidates",
                columns: new[] { "CandidateId", "Archived", "Created", "Email", "Experience", "Name", "Phone", "Updated" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2024, 3, 30, 9, 25, 12, 12, DateTimeKind.Utc).AddTicks(170), "john.doe@example.com", 5, "John Doe", "1234567890", null },
                    { 2, null, new DateTime(2024, 3, 30, 9, 25, 12, 12, DateTimeKind.Utc).AddTicks(245), "jane.doe@example.com", 1, "Jane Doe", "0987654321", null }
                });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "PositionId", "Archived", "Created", "Description", "Title", "Updated" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2024, 3, 30, 9, 25, 12, 11, DateTimeKind.Utc).AddTicks(9954), "Develops software.", "Software Developer", null },
                    { 2, null, new DateTime(2024, 3, 30, 9, 25, 12, 11, DateTimeKind.Utc).AddTicks(9959), "Works with data.", "Data Scientist", null }
                });

            migrationBuilder.InsertData(
                table: "CandidatePositions",
                columns: new[] { "CandidateId", "PositionId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "SkillId", "Archived", "CandidateId", "Created", "Name", "Updated", "YearsOfExperience" },
                values: new object[,]
                {
                    { 1, null, 1, new DateTime(2024, 3, 30, 9, 25, 12, 12, DateTimeKind.Utc).AddTicks(294), "C#", null, 5 },
                    { 2, null, 1, new DateTime(2024, 3, 30, 9, 25, 12, 12, DateTimeKind.Utc).AddTicks(302), "JavaScript", null, 3 },
                    { 3, null, 2, new DateTime(2024, 3, 30, 9, 25, 12, 12, DateTimeKind.Utc).AddTicks(304), "SQL", null, 4 },
                    { 4, null, 2, new DateTime(2024, 3, 30, 9, 25, 12, 12, DateTimeKind.Utc).AddTicks(306), "Python", null, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidatePositions_PositionId",
                table: "CandidatePositions",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_CandidateId",
                table: "Skills",
                column: "CandidateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidatePositions");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Candidates");
        }
    }
}
