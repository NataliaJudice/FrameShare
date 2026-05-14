using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FrameShare.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class addtituloo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Titulo",
                table: "Missao",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Titulo",
                table: "Missao");
        }
    }
}
