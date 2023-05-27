using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { "72f30aa5-06ac-4942-b530-085e2185ab49", "SuperAdmin", "SuperAdmin".ToUpper(), "72f30aa5-06ac-4942-b530-085e2185ab49" }
            );
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { "b18b5b0f-b286-44a7-bfb9-6a7d38cded30", "Admin", "Admin".ToUpper(), "b18b5b0f-b286-44a7-bfb9-6a7d38cded30" }
            );
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { "46a3790d-8143-4635-8dfa-640c7fa3269f", "RegularUser", "RegularUser".ToUpper(), "46a3790d-8143-4635-8dfa-640c7fa3269f" }
            );

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [AspNetRoles]");
        }
    }
}
