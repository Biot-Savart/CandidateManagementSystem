using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidateManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddExperienceToCandidate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Experience",
                table: "Candidates",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Experience",
                table: "Candidates");

            migrationBuilder.UpdateData(
                table: "Candidates",
                keyColumn: "CandidateId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2024, 3, 28, 11, 7, 44, 489, DateTimeKind.Utc).AddTicks(5697));

            migrationBuilder.UpdateData(
                table: "Candidates",
                keyColumn: "CandidateId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2024, 3, 28, 11, 7, 44, 489, DateTimeKind.Utc).AddTicks(5747));

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "PositionId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2024, 3, 28, 11, 7, 44, 489, DateTimeKind.Utc).AddTicks(5512));

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "PositionId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2024, 3, 28, 11, 7, 44, 489, DateTimeKind.Utc).AddTicks(5517));
        }
    }
}
