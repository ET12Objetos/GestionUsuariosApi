using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Persistencia.Migraciones
{
    /// <inheritdoc />
    public partial class CambioNombreForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolUsuario_Rol_RolesId",
                table: "RolUsuario");

            migrationBuilder.DropForeignKey(
                name: "FK_RolUsuario_Usuario_UsuariosId",
                table: "RolUsuario");

            migrationBuilder.RenameColumn(
                name: "UsuariosId",
                table: "RolUsuario",
                newName: "IdUsuario");

            migrationBuilder.RenameColumn(
                name: "RolesId",
                table: "RolUsuario",
                newName: "IdRol");

            migrationBuilder.RenameIndex(
                name: "IX_RolUsuario_UsuariosId",
                table: "RolUsuario",
                newName: "IX_RolUsuario_IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_RolUsuario_Rol_IdRol",
                table: "RolUsuario",
                column: "IdRol",
                principalTable: "Rol",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolUsuario_Usuario_IdUsuario",
                table: "RolUsuario",
                column: "IdUsuario",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolUsuario_Rol_IdRol",
                table: "RolUsuario");

            migrationBuilder.DropForeignKey(
                name: "FK_RolUsuario_Usuario_IdUsuario",
                table: "RolUsuario");

            migrationBuilder.RenameColumn(
                name: "IdUsuario",
                table: "RolUsuario",
                newName: "UsuariosId");

            migrationBuilder.RenameColumn(
                name: "IdRol",
                table: "RolUsuario",
                newName: "RolesId");

            migrationBuilder.RenameIndex(
                name: "IX_RolUsuario_IdUsuario",
                table: "RolUsuario",
                newName: "IX_RolUsuario_UsuariosId");

            migrationBuilder.AddForeignKey(
                name: "FK_RolUsuario_Rol_RolesId",
                table: "RolUsuario",
                column: "RolesId",
                principalTable: "Rol",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolUsuario_Usuario_UsuariosId",
                table: "RolUsuario",
                column: "UsuariosId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
