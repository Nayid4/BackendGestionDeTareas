using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructura.Persistencia.Migraciones
{
    /// <inheritdoc />
    public partial class AjusteDeFechaDeCreacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaDeCreacion",
                table: "ListaDeTareas",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaDeCreacion",
                table: "ListaDeTareas");
        }
    }
}
