using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CandidateManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddSkillsModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "Candidates",
                keyColumn: "CandidateId",
                keyValue: 1,
                columns: new[] { "Created", "Experience" },
                values: new object[] { new DateTime(2024, 3, 28, 11, 25, 54, 811, DateTimeKind.Utc).AddTicks(1385), 5 });

            migrationBuilder.UpdateData(
                table: "Candidates",
                keyColumn: "CandidateId",
                keyValue: 2,
                columns: new[] { "Created", "Experience" },
                values: new object[] { new DateTime(2024, 3, 28, 11, 25, 54, 811, DateTimeKind.Utc).AddTicks(1390), 1 });

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "PositionId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2024, 3, 28, 11, 25, 54, 811, DateTimeKind.Utc).AddTicks(1128));

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "PositionId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2024, 3, 28, 11, 25, 54, 811, DateTimeKind.Utc).AddTicks(1133));

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "SkillId", "Archived", "CandidateId", "Created", "Name", "Updated", "YearsOfExperience" },
                values: new object[,]
                {
                    { 1, null, 1, new DateTime(2024, 3, 28, 11, 25, 54, 811, DateTimeKind.Utc).AddTicks(1464), "C#", null, 5 },
                    { 2, null, 1, new DateTime(2024, 3, 28, 11, 25, 54, 811, DateTimeKind.Utc).AddTicks(1468), "JavaScript", null, 3 },
                    { 3, null, 2, new DateTime(2024, 3, 28, 11, 25, 54, 811, DateTimeKind.Utc).AddTicks(1470), "SQL", null, 4 },
                    { 4, null, 2, new DateTime(2024, 3, 28, 11, 25, 54, 811, DateTimeKind.Utc).AddTicks(1472), "Python", null, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Skills_CandidateId",
                table: "Skills",
                column: "CandidateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.UpdateData(
                table: "Candidates",
                keyColumn: "CandidateId",
                keyValue: 1,
                columns: new[] { "Created", "Experience" },
                values: new object[] { new DateTime(2024, 3, 28, 11, 14, 26, 698, DateTimeKind.Utc).AddTicks(4588), 0 });

            migrationBuilder.UpdateData(
                table: "Candidates",
                keyColumn: "CandidateId",
                keyValue: 2,
                columns: new[] { "Created", "Experience" },
                values: new object[] { new DateTime(2024, 3, 28, 11, 14, 26, 698, DateTimeKind.Utc).AddTicks(4590), 0 });

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "PositionId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2024, 3, 28, 11, 14, 26, 698, DateTimeKind.Utc).AddTicks(4460));

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "PositionId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2024, 3, 28, 11, 14, 26, 698, DateTimeKind.Utc).AddTicks(4462));
        }
    }
}
