﻿// <auto-generated />
using System;
using Leilao.PL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Leilao.Migrations
{
    [DbContext(typeof(LeilaoContext))]
    [Migration("20200705105719_Modificacao_Data_Annotations")]
    partial class Modificacao_Data_Annotations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Leilao.PL.Lance", b =>
                {
                    b.Property<int>("ID_Lance")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime");

                    b.Property<int>("FK_Leilao")
                        .HasColumnType("int");

                    b.Property<int>("FK_Usuario")
                        .HasColumnType("int");

                    b.Property<string>("FlagVencedor")
                        .IsRequired()
                        .HasColumnType("char(1)");

                    b.Property<double>("Valor")
                        .HasColumnType("float");

                    b.HasKey("ID_Lance");

                    b.HasIndex("FK_Leilao");

                    b.HasIndex("FK_Usuario");

                    b.ToTable("Lances");
                });

            modelBuilder.Entity("Leilao.PL.Leilao", b =>
                {
                    b.Property<int>("ID_Leilao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(500)");

                    b.Property<int>("FK_Responsavel")
                        .HasColumnType("int");

                    b.Property<string>("Forma")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime>("Inicio")
                        .HasColumnType("datetime");

                    b.Property<string>("Natureza")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime>("Termino")
                        .HasColumnType("datetime");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(60);

                    b.HasKey("ID_Leilao");

                    b.HasIndex("FK_Responsavel");

                    b.ToTable("Leiloes");
                });

            modelBuilder.Entity("Leilao.PL.Lote", b =>
                {
                    b.Property<int>("ID_Lote")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FK_Leilao")
                        .HasColumnType("int");

                    b.Property<int?>("LeilaoID_Leilao")
                        .HasColumnType("int");

                    b.Property<decimal?>("ValorMaximo")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal?>("ValorMinimo")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("ID_Lote");

                    b.HasIndex("LeilaoID_Leilao");

                    b.ToTable("Lotes");
                });

            modelBuilder.Entity("Leilao.PL.Produto", b =>
                {
                    b.Property<int>("ID_Produto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Categoria")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.Property<string>("DescricaoCurta")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("DescricaoLonga")
                        .IsRequired()
                        .HasColumnType("varchar(1000)");

                    b.Property<int>("FK_Lote")
                        .HasColumnType("int");

                    b.HasKey("ID_Produto");

                    b.HasIndex("FK_Lote");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("Leilao.PL.Usuario", b =>
                {
                    b.Property<int>("ID_Usuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CpfOrCnpj")
                        .HasColumnType("varchar(14)");

                    b.Property<string>("FK_Login")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(100)");

                    b.HasKey("ID_Usuario");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Leilao.PL.Lance", b =>
                {
                    b.HasOne("Leilao.PL.Leilao", "Leilao")
                        .WithMany("Lances")
                        .HasForeignKey("FK_Leilao")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Leilao.PL.Usuario", "Usuario")
                        .WithMany("Lances")
                        .HasForeignKey("FK_Usuario")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Leilao.PL.Leilao", b =>
                {
                    b.HasOne("Leilao.PL.Usuario", "Usuario")
                        .WithMany("Leiloes")
                        .HasForeignKey("FK_Responsavel")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Leilao.PL.Lote", b =>
                {
                    b.HasOne("Leilao.PL.Leilao", "Leilao")
                        .WithMany()
                        .HasForeignKey("LeilaoID_Leilao");
                });

            modelBuilder.Entity("Leilao.PL.Produto", b =>
                {
                    b.HasOne("Leilao.PL.Lote", "Lote")
                        .WithMany("Produtos")
                        .HasForeignKey("FK_Lote")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
