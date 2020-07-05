using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Leilao.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    ID_Usuario = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: true),
                    CpfOrCnpj = table.Column<string>(type: "varchar(14)", nullable: true),
                    FK_Login = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.ID_Usuario);
                });

            migrationBuilder.CreateTable(
                name: "Leiloes",
                columns: table => new
                {
                    ID_Leilao = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "varchar(100)", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(500)", nullable: true),
                    Natureza = table.Column<string>(type: "varchar(30)", nullable: false),
                    Forma = table.Column<string>(type: "varchar(30)", nullable: false),
                    Inicio = table.Column<DateTime>(type: "datetime", nullable: false),
                    Termino = table.Column<DateTime>(type: "datetime", nullable: false),
                    FK_Responsavel = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leiloes", x => x.ID_Leilao);
                    table.ForeignKey(
                        name: "FK_Leiloes_Usuarios_FK_Responsavel",
                        column: x => x.FK_Responsavel,
                        principalTable: "Usuarios",
                        principalColumn: "ID_Usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lances",
                columns: table => new
                {
                    ID_Lance = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_Leilao = table.Column<int>(nullable: false),
                    FK_Usuario = table.Column<int>(nullable: false),
                    Valor = table.Column<double>(nullable: false),
                    Data = table.Column<DateTime>(type: "datetime", nullable: false),
                    FlagVencedor = table.Column<string>(type: "char(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lances", x => x.ID_Lance);
                    table.ForeignKey(
                        name: "FK_Lances_Leiloes_FK_Leilao",
                        column: x => x.FK_Leilao,
                        principalTable: "Leiloes",
                        principalColumn: "ID_Leilao",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lances_Usuarios_FK_Usuario",
                        column: x => x.FK_Usuario,
                        principalTable: "Usuarios",
                        principalColumn: "ID_Usuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lotes",
                columns: table => new
                {
                    ID_Lote = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_Leilao = table.Column<int>(nullable: false),
                    LeilaoID_Leilao = table.Column<int>(nullable: true),
                    ValorMinimo = table.Column<double>(nullable: true),
                    ValorMaximo = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lotes", x => x.ID_Lote);
                    table.ForeignKey(
                        name: "FK_Lotes_Leiloes_LeilaoID_Leilao",
                        column: x => x.LeilaoID_Leilao,
                        principalTable: "Leiloes",
                        principalColumn: "ID_Leilao",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    ID_Produto = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_Lote = table.Column<int>(nullable: false),
                    DescricaoCurta = table.Column<string>(type: "varchar(100)", nullable: false),
                    DescricaoLonga = table.Column<string>(type: "varchar(1000)", nullable: false),
                    Categoria = table.Column<string>(type: "varchar(30)", nullable: false),
                    Foto = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.ID_Produto);
                    table.ForeignKey(
                        name: "FK_Produtos_Lotes_FK_Lote",
                        column: x => x.FK_Lote,
                        principalTable: "Lotes",
                        principalColumn: "ID_Lote",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lances_FK_Leilao",
                table: "Lances",
                column: "FK_Leilao");

            migrationBuilder.CreateIndex(
                name: "IX_Lances_FK_Usuario",
                table: "Lances",
                column: "FK_Usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Leiloes_FK_Responsavel",
                table: "Leiloes",
                column: "FK_Responsavel");

            migrationBuilder.CreateIndex(
                name: "IX_Lotes_LeilaoID_Leilao",
                table: "Lotes",
                column: "LeilaoID_Leilao");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_FK_Lote",
                table: "Produtos",
                column: "FK_Lote");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lances");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Lotes");

            migrationBuilder.DropTable(
                name: "Leiloes");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
