using Microsoft.EntityFrameworkCore.Migrations;

namespace Leilao.Migrations
{
    public partial class Modificacao_Data_Annotations_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Usuarios",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CpfOrCnpj",
                table: "Usuarios",
                type: "varchar(14)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(14)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "ValorMinimo",
                table: "Lotes",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "ValorMaximo",
                table: "Lotes",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Usuarios",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "CpfOrCnpj",
                table: "Usuarios",
                type: "varchar(14)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(14)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ValorMinimo",
                table: "Lotes",
                type: "decimal(18, 2)",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ValorMaximo",
                table: "Lotes",
                type: "decimal(18, 2)",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}
