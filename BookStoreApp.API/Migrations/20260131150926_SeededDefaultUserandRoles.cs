using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStoreApp.API.Migrations
{
    /// <inheritdoc />
    public partial class SeededDefaultUserandRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "159fb287-1bca-4bd6-bfc6-e69f0bdacd47", null, "Administrator", "ADMINISTRATOR" },
                    { "bd0422bb-c842-401e-ab11-0aa4545a8b4b", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "442b5852-0a3e-4bc3-8aba-a4c957ba4b55", 0, "21e26be9-526b-4e06-8e23-c6045e657c06", "admin@bookstore.com", false, "System", "Admin", false, null, "ADMIN@BOOKSTORE.COM", "ADMIN@BOOKSTORE.COM", "AQAAAAIAAYagAAAAEMpUQ1ZLpS/jTyYr78gyPziuKC85Ng8go0ERJNKWfmBeefaBgW7YLgXKtDM2vUVLSA==", null, false, "c44e39b3-d5a6-4a4d-83f6-ea57116bd55b", false, "admin@bookstore.com" },
                    { "4b562b69-831e-46f2-8ffd-6cbae43bb4bd", 0, "63c8e884-cd32-40a0-ae96-45a750d87a8d", "user@bookstore.com", false, "System", "User", false, null, "USER@BOOKSTORE.COM", "USER@BOOKSTORE.COM", "AQAAAAIAAYagAAAAEJcHmIWRYHF6xFHZLnoxAqO/xZHbPNzSJ8xU15YUSgwAixQfpceLcUE0UqDGv9xnxw==", null, false, "e26ac0ad-56ea-416b-aae9-b7ffdc784f5a", false, "user@bookstore.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "159fb287-1bca-4bd6-bfc6-e69f0bdacd47", "442b5852-0a3e-4bc3-8aba-a4c957ba4b55" },
                    { "bd0422bb-c842-401e-ab11-0aa4545a8b4b", "4b562b69-831e-46f2-8ffd-6cbae43bb4bd" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "159fb287-1bca-4bd6-bfc6-e69f0bdacd47", "442b5852-0a3e-4bc3-8aba-a4c957ba4b55" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "bd0422bb-c842-401e-ab11-0aa4545a8b4b", "4b562b69-831e-46f2-8ffd-6cbae43bb4bd" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "159fb287-1bca-4bd6-bfc6-e69f0bdacd47");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd0422bb-c842-401e-ab11-0aa4545a8b4b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "442b5852-0a3e-4bc3-8aba-a4c957ba4b55");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4b562b69-831e-46f2-8ffd-6cbae43bb4bd");
        }
    }
}
