using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Api.Persistencia.Migraciones
{
    /// <inheritdoc />
    public partial class DataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Rol",
                columns: new[] { "Id", "CreacionFecha", "CreacionUsuario", "Habilitado", "Nombre" },
                values: new object[,]
                {
                    { new Guid("2960ce73-3316-4c58-9931-2b202e14517e"), new DateTime(2024, 10, 16, 21, 18, 24, 505, DateTimeKind.Local).AddTicks(6435), "", true, "Legales" },
                    { new Guid("b78542ef-ed34-47e9-80be-ec42505439b9"), new DateTime(2024, 10, 16, 21, 18, 24, 505, DateTimeKind.Local).AddTicks(6421), "", true, "Administrador" },
                    { new Guid("e1209409-dfa1-48f4-b3ce-ef5eb580754f"), new DateTime(2024, 10, 16, 21, 18, 24, 505, DateTimeKind.Local).AddTicks(6423), "", true, "Vendedor" }
                });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "Contraseña", "CreacionFecha", "CreacionUsuario", "Email", "Habilitado", "Nombre", "NombreCompleto" },
                values: new object[,]
                {
                    { new Guid("891d716d-f5bd-430d-8272-8fcd3ec48a69"), "pass123", new DateTime(2024, 10, 16, 21, 18, 24, 505, DateTimeKind.Local).AddTicks(6341), "", "", true, "Pablo123", "Pablo Lopez" },
                    { new Guid("f5a42fca-12c4-4d1b-88d8-103cbd8010b4"), "pass123", new DateTime(2024, 10, 16, 21, 18, 24, 505, DateTimeKind.Local).AddTicks(6323), "", "", true, "Juan123", "Juan Perez" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rol",
                keyColumn: "Id",
                keyValue: new Guid("2960ce73-3316-4c58-9931-2b202e14517e"));

            migrationBuilder.DeleteData(
                table: "Rol",
                keyColumn: "Id",
                keyValue: new Guid("b78542ef-ed34-47e9-80be-ec42505439b9"));

            migrationBuilder.DeleteData(
                table: "Rol",
                keyColumn: "Id",
                keyValue: new Guid("e1209409-dfa1-48f4-b3ce-ef5eb580754f"));

            migrationBuilder.DeleteData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: new Guid("891d716d-f5bd-430d-8272-8fcd3ec48a69"));

            migrationBuilder.DeleteData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: new Guid("f5a42fca-12c4-4d1b-88d8-103cbd8010b4"));
        }
    }
}
