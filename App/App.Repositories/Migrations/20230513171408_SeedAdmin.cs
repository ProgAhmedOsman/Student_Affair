using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[AspNetUsers]\r\n           ([Id]\r\n           ,[FirstName]\r\n           ,[LastName]\r\n           ,[Mobile]\r\n           ,[UserName]\r\n           ,[NormalizedUserName]\r\n           ,[Email]\r\n           ,[NormalizedEmail]\r\n           ,[EmailConfirmed]\r\n           ,[PasswordHash]\r\n           ,[SecurityStamp]\r\n           ,[ConcurrencyStamp]\r\n           ,[PhoneNumber]\r\n           ,[PhoneNumberConfirmed]\r\n           ,[TwoFactorEnabled]\r\n           ,[LockoutEnd]\r\n           ,[LockoutEnabled]\r\n           ,[AccessFailedCount])\r\n     VALUES\r\n            ('94fd922c-02da-45ba-9443-d83f57f1cc39','Admin','','12345678',\t'Admin','ADMIN',\t\r\n\t\t\t'Admin@Admin.com','ADMIN@ADMIN.COM',\t'False',\r\n\t\t\t'AQAAAAIAAYagAAAAEMVYhcEOoleERLBdcUgSaJMzwzjDAHraKz5lzD1U9XPRWw6ejRU8NNF7ie0ESMHb0Q==',\r\n\t\t\t'OXTONSRCROCTVKQAXFFCYFF7XGQWRULM',\t'24989f72-53d6-42d3-a9ec-4c09b280f947',NULL\t,'False',\t'False',\r\n\t\t\tNULL,\t'True',\t0)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [dbo].[AspNetUsers] where [Id] = '94fd922c-02da-45ba-9443-d83f57f1cc39'\r\n");

        }
    }
}
