using Leilao.PL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Leilao.BLL
{
    internal class LeilaoFunctions : IManagerLeilao<PL.Leilao>
    {
        public List<PL.Leilao> ReadLeiloesParaDarLance(string login)
        {
            using var db = new LeilaoContext();

            var query = from p in db.Leiloes.Include(l => l.Usuario)
                        where p.Usuario.FK_Login != login && p.Termino > DateTime.Today && p.Inicio < DateTime.Today
                        select p;

            return query.ToList();
        }
    }
}
