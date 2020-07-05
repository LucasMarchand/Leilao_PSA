using Leilao.PL;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.Identity;

namespace Leilao.BLL
{
    internal class UsuarioManager : IManager<Usuario>
    {
    
        public Usuario Create(Usuario usuario)
        {
            using var db = new LeilaoContext();
            db.Usuarios.Add(usuario);
            db.SaveChanges();
            return usuario;
        }

        public Usuario Read(int id)
        {
            using var db = new LeilaoContext();
            return db.Usuarios.FirstOrDefault(x => x.ID_Usuario == id);
        }

        public List<Usuario> Read()
        {
            using var db = new LeilaoContext();
            return db.Usuarios.ToList();
        }

        public List<Usuario> ReadCurrent (string fk)
        {
            using var db = new LeilaoContext();
            return db.Usuarios.Where(f => f.FK_Login == fk).ToList();
        }

        public Usuario Update(Usuario usuario)
        {
            using var db = new LeilaoContext();

            db.Entry(usuario).State = EntityState.Modified;
            db.SaveChanges();
            return usuario;
        }
    
        public bool Delete(int id)
        {
            using var db = new LeilaoContext();

            var user = Read(id);
            db.Entry(user).State = EntityState.Deleted;
            db.SaveChanges();

            return Read(id) == null;
        }

        public bool Exists(int id)
        {
            using var db = new LeilaoContext();
            return db.Usuarios.Any(e => e.ID_Usuario == id);
        }

        public bool VerificaSePodeEditar(string login, int id)
        {
            throw new System.NotImplementedException();
        }
    }
}