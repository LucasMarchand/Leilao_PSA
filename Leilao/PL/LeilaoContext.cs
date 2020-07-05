using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Leilao.PL
{
    public class LeilaoContext : DbContext
    {

        public LeilaoContext(DbContextOptions<LeilaoContext> opt) : base(opt) { }

        public LeilaoContext() { }

        public DbSet<Leilao> Leiloes { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        //public DbSet<LoteProduto> LotesProdutos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Lance> Lances { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
           .UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=DBLeilao;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder
                .Entity<Leilao>()
                .Property(e => e.Natureza)
                .HasConversion(
                    v => v.ToString(),
                    v => (EnumNatureza)Enum.Parse(typeof(EnumNatureza), v));

            modelBuilder
                .Entity<Leilao>()
                .Property(e => e.Forma)
                .HasConversion(
                    v => v.ToString(),
                    v => (EnumForma)Enum.Parse(typeof(EnumForma), v));

            modelBuilder.Entity<Leilao>()
                .HasOne(p => p.Usuario)
                .WithMany(b => b.Leiloes)
                .HasForeignKey(p => p.FK_Responsavel)
                .HasPrincipalKey(b => b.ID_Usuario);

            //modelBuilder.Entity<Lote>()
            //    .HasOne(p => p.Leilao)
            //    .WithOne(b => b.Lotes)
            //    .HasForeignKey(p => p.FK_Lote)
            //    .HasPrincipalKey(b => b.ID_Lote)
            //    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Produto>()
                .HasOne(p => p.Lote)
                .WithMany(b => b.Produtos)
                .HasForeignKey(p => p.FK_Lote)
                .HasPrincipalKey(b => b.ID_Lote);

            modelBuilder.Entity<Lance>()
                .HasOne(p => p.Leilao)
                .WithMany(b => b.Lances)
                .HasForeignKey(p => p.FK_Leilao)
                .HasPrincipalKey(b => b.ID_Leilao);

            modelBuilder.Entity<Lance>()
                .HasOne(p => p.Usuario)
                .WithMany(b => b.Lances)
                .HasForeignKey(p => p.FK_Usuario)
                .HasPrincipalKey(b => b.ID_Usuario)
                .OnDelete(DeleteBehavior.Restrict);

        }

    }
}
