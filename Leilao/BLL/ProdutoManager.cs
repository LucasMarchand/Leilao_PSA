using Leilao.PL;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Leilao.BLL
{
    internal class ProdutoManager : IManager<Produto>
    {

        public Produto Create(Produto t)
        {
            using var db = new LeilaoContext();
            db.Produtos.Add(t);
            db.SaveChanges();
            return t;
        }

        public bool Delete(int id)
        {
            using var db = new LeilaoContext();

            var produto = Read(id);
            db.Entry(produto).State = EntityState.Deleted;
            db.SaveChanges();

            return Read(id) == null;
        }

        public bool Exists(int id)
        {
            using var db = new LeilaoContext();
            return db.Produtos.Any(e => e.ID_Produto == id);
        }

        public Produto Read(int id)
        {
            using var db = new LeilaoContext();
            return db.Produtos.Include(p => p.Lote).FirstOrDefault(x => x.ID_Produto == id);

        }

        public List<Produto> Read()
        {
            using var db = new LeilaoContext();
            return db.Produtos.ToList();
        }

        public object ReadCurrent(string v)
        {
            throw new System.NotImplementedException();
        }

        public Produto Update(Produto t)
        {
            using var db = new LeilaoContext();

            db.Entry(t).State = EntityState.Modified;
            db.SaveChanges();
            return t;
        }

        public bool VerificaSePodeEditar(string login, int id)
        {
            throw new System.NotImplementedException();
        }

        List<Produto> IManager<Produto>.ReadCurrent(string v)
        {
            throw new System.NotImplementedException();
        }
    }
}