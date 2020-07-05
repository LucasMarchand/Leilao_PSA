using Leilao.PL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Leilao.BLL
{
    internal class LanceManager : IManager<Lance>
    {

        public Lance Create(Lance t)
        {
            using var db = new LeilaoContext();
            t.Data = DateTime.Today;
            db.Lances.Add(t);
            db.SaveChanges();
            return t;
        }

        public bool Delete(int id)
        {
            using var db = new LeilaoContext();

            var lance = Read(id);
            db.Entry(lance).State = EntityState.Deleted;
            db.SaveChanges();

            return Read(id) == null;
        }

        public bool Exists(int id)
        {
            using var db = new LeilaoContext();
            return db.Lances.Any(e => e.ID_Lance == id);
        }

        public Lance Read(int id)
        {
            using var db = new LeilaoContext();
            return db.Lances.Include(l => l.Leilao)
                            .Include(l => l.Usuario)
                            .FirstOrDefault(x => x.ID_Lance == id);

        }

        public List<Lance> Read()
        {
            using var db = new LeilaoContext();
            //return db.Lances.Include(l => l.Leilao)
            //                .Include(l => l.Usuario)
            //                .ToList();

            var query = from p in db.Lances.Include(l => l.Usuario).Include(l => l.Leilao)
                         where p.Leilao.Forma == EnumForma.Aberto || p.Leilao.Termino <= DateTime.Today
                         select p;

            //var user = db.Lances.Where(f => f.FK_Login == login).Select(f => f.ID_Usuario);
            return query.ToList();
        }

        public Lance Update(Lance t)
        {
            using var db = new LeilaoContext();

            db.Entry(t).State = EntityState.Modified;
            db.SaveChanges();
            return t;
        }

        public bool VerificaSePodeEditar(string login, int id)
        {
            throw new NotImplementedException();
        }

        List<Lance> IManager<Lance>.ReadCurrent(string login)
        {
            using var db = new LeilaoContext();
            //return db.Lances.Include(l => l.Leilao)
            //                .Include(l => l.Usuario)
            //                .ToList();

            var query = from p in db.Lances.Include(l => l.Usuario).Include(l => l.Leilao)
                        where p.Leilao.Forma == EnumForma.Aberto || p.Leilao.Termino <= DateTime.Today || p.Usuario.FK_Login == login
                        select p;

            //var user = db.Lances.Where(f => f.FK_Login == login).Select(f => f.ID_Usuario);
            return query.ToList();
        }
    }
}