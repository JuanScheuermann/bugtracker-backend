using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Nofoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Etiquetas_EtiquetaId",
                table: "Comentarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Miembros_MiembroId",
                table: "Comentarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Etiquetas_Miembros_MiembroId",
                table: "Etiquetas");

            migrationBuilder.DropForeignKey(
                name: "FK_Etiquetas_Proyectos_ProyectoId",
                table: "Etiquetas");

            migrationBuilder.DropForeignKey(
                name: "FK_Miembros_Proyectos_ProyectoId",
                table: "Miembros");

            migrationBuilder.DropForeignKey(
                name: "FK_Miembros_Usuarios_UserId",
                table: "Miembros");

            migrationBuilder.DropForeignKey(
                name: "FK_Proyectos_Usuarios_AutorId",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "FotoUrl",
                table: "Usuarios");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Etiquetas_EtiquetaId",
                table: "Comentarios",
                column: "EtiquetaId",
                principalTable: "Etiquetas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Miembros_MiembroId",
                table: "Comentarios",
                column: "MiembroId",
                principalTable: "Miembros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Etiquetas_Miembros_MiembroId",
                table: "Etiquetas",
                column: "MiembroId",
                principalTable: "Miembros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Etiquetas_Proyectos_ProyectoId",
                table: "Etiquetas",
                column: "ProyectoId",
                principalTable: "Proyectos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Miembros_Proyectos_ProyectoId",
                table: "Miembros",
                column: "ProyectoId",
                principalTable: "Proyectos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Miembros_Usuarios_UserId",
                table: "Miembros",
                column: "UserId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Proyectos_Usuarios_AutorId",
                table: "Proyectos",
                column: "AutorId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Etiquetas_EtiquetaId",
                table: "Comentarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Miembros_MiembroId",
                table: "Comentarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Etiquetas_Miembros_MiembroId",
                table: "Etiquetas");

            migrationBuilder.DropForeignKey(
                name: "FK_Etiquetas_Proyectos_ProyectoId",
                table: "Etiquetas");

            migrationBuilder.DropForeignKey(
                name: "FK_Miembros_Proyectos_ProyectoId",
                table: "Miembros");

            migrationBuilder.DropForeignKey(
                name: "FK_Miembros_Usuarios_UserId",
                table: "Miembros");

            migrationBuilder.DropForeignKey(
                name: "FK_Proyectos_Usuarios_AutorId",
                table: "Proyectos");

            migrationBuilder.AddColumn<string>(
                name: "FotoUrl",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Etiquetas_EtiquetaId",
                table: "Comentarios",
                column: "EtiquetaId",
                principalTable: "Etiquetas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Miembros_MiembroId",
                table: "Comentarios",
                column: "MiembroId",
                principalTable: "Miembros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Etiquetas_Miembros_MiembroId",
                table: "Etiquetas",
                column: "MiembroId",
                principalTable: "Miembros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Etiquetas_Proyectos_ProyectoId",
                table: "Etiquetas",
                column: "ProyectoId",
                principalTable: "Proyectos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Miembros_Proyectos_ProyectoId",
                table: "Miembros",
                column: "ProyectoId",
                principalTable: "Proyectos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Miembros_Usuarios_UserId",
                table: "Miembros",
                column: "UserId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Proyectos_Usuarios_AutorId",
                table: "Proyectos",
                column: "AutorId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
