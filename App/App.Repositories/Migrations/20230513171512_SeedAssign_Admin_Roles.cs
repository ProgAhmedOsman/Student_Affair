using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class SeedAssign_Admin_Roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
                migrationBuilder.Sql("INSERT INTO [dbo].[AspNetUserRoles]\r\n           ([UserId]\r\n           ,[RoleId])\r\n     VALUES\r\n      " +
                    "     ('94fd922c-02da-45ba-9443-d83f57f1cc39', '72f30aa5-06ac-4942-b530-085e2185ab49'),\r\n     " +
                    "      ('94fd922c-02da-45ba-9443-d83f57f1cc39', 'b18b5b0f-b286-44a7-bfb9-6a7d38cded30'),\r\n      " +
                    "     ('94fd922c-02da-45ba-9443-d83f57f1cc39', '46a3790d-8143-4635-8dfa-640c7fa3269f')"
                    
                    );

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
              migrationBuilder.Sql("delete from [dbo].[AspNetUserRoles] where UserId ='94fd922c-02da-45ba-9443-d83f57f1cc39'\r\n");

        }
    }
}
