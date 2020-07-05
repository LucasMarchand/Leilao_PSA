using Leilao.PL;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Leilao.BLL
{
    internal class LoteManager : IManager<Lote>
    {
        public Lote Create(Lote t)
        {
            using var db = new LeilaoContext();
            db.Lotes.Add(t);
            db.SaveChanges();
            return t;
        }

        public bool Delete(int id)
        {
            using var db = new LeilaoContext();

            var lote = Read(id);
            db.Entry(lote).State = EntityState.Deleted;
            db.SaveChanges();

            return Read(id) == null;
        }

        public bool Exists(int id)
        {
            using var db = new LeilaoContext();
            return db.Lotes.Any(e => e.ID_Lote == id);
        }

        public Lote Read(int id)
        {
            using var db = new LeilaoContext();
            return db.Lotes.FirstOrDefault(x => x.ID_Lote == id);

        }

        public List<Lote> Read()
        {
            using var db = new LeilaoContext();
            return db.Lotes.ToList();
        }

        public object ReadCurrent(string v)
        {
            throw new System.NotImplementedException();
        }

        public Lote Update(Lote t)
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

        List<Lote> IManager<Lote>.ReadCurrent(string v)
        {
            throw new System.NotImplementedException();
        }
    }
}
