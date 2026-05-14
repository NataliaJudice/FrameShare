using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FrameShare.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataCadastro",
                table: "Missao",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Missao",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Foto",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataCadastro",
                table: "Missao");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Missao");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Foto");
        }
    }
}
