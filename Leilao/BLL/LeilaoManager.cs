using Leilao.PL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Leilao.BLL
{
    internal class LeilaoManager : IManager<PL.Leilao>
    {
        public PL.Leilao Create(PL.Leilao t)
        {
            using var db = new LeilaoContext();
            db.Leiloes.Add(t);
            db.SaveChanges();
            return t;
        }

        public bool Delete(int id)
        {
            using var db = new LeilaoContext();

            var leilao = Read(id);
            db.Entry(leilao).State = EntityState.Deleted;
            db.SaveChanges();

            return Read(id) == null;
        }

        public bool Exists(int id)
        {
            using var db = new LeilaoContext();
            return db.Leiloes.Any(e => e.ID_Leilao == id);
        }

        public PL.Leilao Read(int id)
        {
            using var db = new LeilaoContext();
            return db.Leiloes.Include(l => l.Usuario).FirstOrDefault(x => x.ID_Leilao == id);
        }

        public List<PL.Leilao> Read()
        {
            using var db = new LeilaoContext();
            return db.Leiloes
                .Include(l => l.Usuario)
                //.Where(l => l.Inicio <= DateTime.Today)
                .ToList();
        }

        public PL.Leilao Update(PL.Leilao t)
        {
            using var db = new LeilaoContext();

            db.Entry(t).State = EntityState.Modified;
            db.SaveChanges();
            return t;
        }

        List<PL.Leilao> IManager<PL.Leilao>.ReadCurrent(string fk)
        {
            using var db = new LeilaoContext();
            var query = db.Usuarios.Where(f => f.FK_Login == fk).Select(f => f.ID_Usuario);
            int id_user = 0;

            foreach (var i in query)
            {
                id_user = i;
            }

            return db.Leiloes
                .Where(f => f.FK_Responsavel == id_user && f.Termino >= DateTime.Today)
                .ToList();

        }

        public bool VerificaSePodeEditar (string login, int id)
        {
            using var db = new LeilaoContext();

            var query = from p in db.Leiloes.Include(l => l.Usuario)
                         where p.Usuario.FK_Login == login && p.ID_Leilao == id
                         select p.ID_Leilao;
            int x = 0;
            foreach (var i in query)
            {
                x = i;
            }

            if (x > 0) { return true; } else { return false; }

        }

    }
}
