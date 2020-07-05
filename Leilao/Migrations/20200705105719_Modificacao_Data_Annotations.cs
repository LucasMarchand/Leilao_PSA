using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Leilao.Migrations
{
    public partial class Modificacao_Data_Annotations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Foto",
                table: "Produtos");

            migrationBuilder.AlterColumn<string>(
                name: "FK_Login",
                table: "Usuarios",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ValorMinimo",
                table: "Lotes",
                type: "decimal(18, 2)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ValorMaximo",
                table: "Lotes",
                type: "decimal(18, 2)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FK_Login",
                table: "Usuarios",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Foto",
                table: "Produtos",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "ValorMinimo",
                table: "Lotes",
                type: "float",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "ValorMaximo",
                table: "Lotes",
                type: "float",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)",
                oldNullable: true);
        }
    }
}
