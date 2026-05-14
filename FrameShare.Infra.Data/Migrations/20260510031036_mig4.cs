using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FrameShare.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "EventoId", "NomeCompleto", "Role", "SlugLogin" },
                values: new object[,]
                {
                    { 1, 1, "Natália Judice de Novais", "Convidado", "natalianovais" },
                    { 2, 1, "Admin FrameShare", "Admin", "adminframeshare" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
