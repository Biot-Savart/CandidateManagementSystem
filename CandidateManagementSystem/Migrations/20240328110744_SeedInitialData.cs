using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CandidateManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Candidates",
                columns: new[] { "CandidateId", "Archived", "Created", "Email", "Name", "Phone", "Updated" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2024, 3, 28, 11, 7, 44, 489, DateTimeKind.Utc).AddTicks(5697), "john.doe@example.com", "John Doe", "1234567890", null },
                    { 2, null, new DateTime(2024, 3, 28, 11, 7, 44, 489, DateTimeKind.Utc).AddTicks(5747), "jane.doe@example.com", "Jane Doe", "0987654321", null }
                });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "PositionId", "Archived", "Created", "Description", "Title", "Updated" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2024, 3, 28, 11, 7, 44, 489, DateTimeKind.Utc).AddTicks(5512), "Develops software.", "Software Developer", null },
                    { 2, null, new DateTime(2024, 3, 28, 11, 7, 44, 489, DateTimeKind.Utc).AddTicks(5517), "Works with data.", "Data Scientist", null }
                });

            migrationBuilder.InsertData(
                table: "CandidatePositions",
                columns: new[] { "CandidateId", "PositionId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CandidatePositions",
                keyColumns: new[] { "CandidateId", "PositionId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "CandidatePositions",
                keyColumns: new[] { "CandidateId", "PositionId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "Candidates",
                keyColumn: "CandidateId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Candidates",
                keyColumn: "CandidateId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "PositionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "PositionId",
                keyValue: 2);
        }
    }
}
