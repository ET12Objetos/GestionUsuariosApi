using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Api.Persistencia.Migraciones
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Habilitado = table.Column<bool>(type: "boolean", nullable: false),
                    CreacionUsuario = table.Column<string>(type: "text", nullable: false),
                    CreacionFecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NombreCompleto = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Contraseña = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Habilitado = table.Column<bool>(type: "boolean", nullable: false),
                    CreacionUsuario = table.Column<string>(type: "text", nullable: false),
                    CreacionFecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolUsuario",
                columns: table => new
                {
                    IdRol = table.Column<Guid>(type: "uuid", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolUsuario", x => new { x.IdRol, x.IdUsuario });
                    table.ForeignKey(
                        name: "FK_RolUsuario_Rol_IdRol",
                        column: x => x.IdRol,
                        principalTable: "Rol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolUsuario_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Rol",
                columns: new[] { "Id", "CreacionFecha", "CreacionUsuario", "Habilitado", "Nombre" },
                values: new object[,]
                {
                    { new Guid("546cb1f1-0ce9-4074-a410-502e000c4f8b"), new DateTime(2024, 10, 17, 23, 46, 41, 342, DateTimeKind.Utc).AddTicks(2188), "", true, "Legales" },
                    { new Guid("8c68b8a6-69f2-48a8-bcee-aea9daca0e7d"), new DateTime(2024, 10, 17, 23, 46, 41, 342, DateTimeKind.Utc).AddTicks(2178), "", true, "Vendedor" },
                    { new Guid("e0bd340d-bd4b-4c3b-a5b1-dc2ae8b73ebf"), new DateTime(2024, 10, 17, 23, 46, 41, 342, DateTimeKind.Utc).AddTicks(2168), "", true, "Administrador" }
                });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "Contraseña", "CreacionFecha", "CreacionUsuario", "Email", "Habilitado", "Nombre", "NombreCompleto" },
                values: new object[,]
                {
                    { new Guid("09a97ece-7e03-43b8-813f-39456bab8a00"), "pass123", new DateTime(2024, 10, 17, 23, 46, 41, 342, DateTimeKind.Utc).AddTicks(2109), "", "", true, "Juan123", "Juan Perez" },
                    { new Guid("2a985321-0fa5-46d5-b63b-ede41b5de713"), "pass123", new DateTime(2024, 10, 17, 23, 46, 41, 342, DateTimeKind.Utc).AddTicks(2121), "", "", true, "Pablo123", "Pablo Lopez" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolUsuario_IdUsuario",
                table: "RolUsuario",
                column: "IdUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolUsuario");

            migrationBuilder.DropTable(
                name: "Rol");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
