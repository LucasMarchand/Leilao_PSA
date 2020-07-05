using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Leilao.PL;
using Leilao;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Leilao.BLL;

namespace WebLeilao.Controllers
{
    [Authorize]
    public class LeiloesController : Controller
    {
        private readonly LeilaoContext _context;
        private readonly IManager<Leilao.PL.Leilao> _leilao = new Facade().GetLeilaoManager();
        private readonly IManager<Usuario> _usuario = new Facade().GetUsuarioManager();

        public LeiloesController(LeilaoContext context)
        {
            _context = context;
        }

        // GET: Leiloes
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(_leilao.Read());
        }

        // GET: Leiloes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Leilao.PL.Leilao leilao = _leilao.Read(id.Value);

            if (leilao == null)
            {
                return NotFound();
            }

            return View(leilao);
        }

        // GET: Leiloes/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["FK_Responsavel"] = new SelectList(_usuario.ReadCurrent(User.Identity.GetUserId<string>()), "ID_Usuario", "Nome");
            return View();
        }

        // POST: Leiloes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ID_Leilao,Titulo,Descricao,Natureza,Forma,Inicio,Termino,FK_Responsavel")] Leilao.PL.Leilao leilao)
        {
            if (ModelState.IsValid)
            {
                _leilao.Create(leilao);
                return RedirectToAction(nameof(Index));
            }
            ViewData["FK_Responsavel"] = new SelectList(_usuario.ReadCurrent(User.Identity.GetUserId<string>()), "ID_Usuario", "Nome", leilao.FK_Responsavel);
            return View(leilao);
        }

        // GET: Leiloes/Edit/5
        [Authorize]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!_leilao.VerificaSePodeEditar(User.Identity.GetUserId<string>(), id.Value))
            {
                return NotFound();
            }


            Leilao.PL.Leilao leilao = _leilao.Read(id.Value);

            if (leilao == null)
            {
                return NotFound();
            }
            ViewData["FK_Responsavel"] = new SelectList(_usuario.ReadCurrent(User.Identity.GetUserId<string>()), "ID_Usuario", "Nome", leilao.FK_Responsavel);
            return View(leilao);
        }

        // POST: Leiloes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Edit(int id, [Bind("ID_Leilao,Titulo,Descricao,Natureza,Forma,Inicio,Termino,FK_Responsavel")] Leilao.PL.Leilao leilao)
        {
            if (id != leilao.ID_Leilao)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _leilao.Update(leilao);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeilaoExists(leilao.ID_Leilao))
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
            ViewData["FK_Responsavel"] = new SelectList(_usuario.ReadCurrent(User.Identity.GetUserId<string>()), "ID_Usuario", "Nome", leilao.FK_Responsavel);
            return View(leilao);
        }

        // GET: Leiloes/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!_leilao.VerificaSePodeEditar(User.Identity.GetUserId<string>(), id.Value))
            {
                return NotFound();
            }

            Leilao.PL.Leilao leilao = _leilao.Read(id.Value);
            if (leilao == null)
            {
                return NotFound();
            }

            return View(leilao);
        }

        // POST: Leiloes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _leilao.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool LeilaoExists(int id)
        {
            return _leilao.Exists(id);
        }
    }
}
