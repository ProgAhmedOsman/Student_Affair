using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class migration_Student_ClassRoom_Subject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClassRoom",
                columns: table => new
                {
                    Key = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2(7)", precision: 7, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2(7)", precision: 7, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassRoom", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    Key = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2(7)", precision: 7, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2(7)", precision: 7, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Key = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassRoom_key = table.Column<int>(type: "int", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2(7)", precision: 7, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2(7)", precision: 7, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Key);
                    table.ForeignKey(
                        name: "FK_Student_ClassRoom_ClassRoom_key",
                        column: x => x.ClassRoom_key,
                        principalTable: "ClassRoom",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentSubject",
                columns: table => new
                {
                    Key = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Subject_Key = table.Column<int>(type: "int", nullable: true),
                    Student_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2(7)", precision: 7, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2(7)", precision: 7, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSubject", x => x.Key);
                    table.ForeignKey(
                        name: "FK_StudentSubject_Student_Student_Key",
                        column: x => x.Student_Key,
                        principalTable: "Student",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentSubject_Subject_Subject_Key",
                        column: x => x.Subject_Key,
                        principalTable: "Subject",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Student_ClassRoom_key",
                table: "Student",
                column: "ClassRoom_key");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubject_Student_Key",
                table: "StudentSubject",
                column: "Student_Key");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubject_Subject_Key",
                table: "StudentSubject",
                column: "Subject_Key");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentSubject");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "ClassRoom");
        }
    }
}
