using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Leilao.PL;
using Leilao;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;

namespace WebLeilao.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        private readonly LeilaoContext _context;
        //private readonly UserManager<IdentityUser> _userManager;
        private readonly IManager<Usuario> _usuario = new Facade().GetUsuarioManager();

        public UsuariosController(LeilaoContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public IActionResult Index()
        {
            return View(_usuario.ReadCurrent(User.Identity.GetUserId<string>()));
        }

        //// GET: Usuarios/Details/5
        //public IActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    Usuario usuario = _usuario.Read(id.Value);

        //    if (usuario == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(usuario);
        //}

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ID_Usuario,Nome,CpfOrCnpj")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                
                usuario.FK_Login = User.Identity.GetUserId<string>();
                _usuario.Create(usuario);
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Usuario usuario = _usuario.Read(id.Value);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID_Usuario,Nome,CpfOrCnpj")] Usuario usuario)
        {
            if (id != usuario.ID_Usuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    usuario.FK_Login = User.Identity.GetUserId<string>();
                    _usuario.Update(usuario);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.ID_Usuario))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Usuario usuario = _usuario.Read(id.Value);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _usuario.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _usuario.Exists(id);
        }
    }
}
