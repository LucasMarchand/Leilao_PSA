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

namespace WebLeilao.Controllers
{
    [Authorize]
    public class LotesController : Controller
    {
        private readonly LeilaoContext _context;
        private readonly IManager<Lote> _lote = new Facade().GetLoteManager();
        private readonly IManager<Leilao.PL.Leilao> _leilao = new Facade().GetLeilaoManager();

        public LotesController(LeilaoContext context)
        {
            _context = context;
        }

        // GET: Lotes
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(_lote.Read());
        }

        // GET: Lotes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Lote lote = _lote.Read(id.Value);

            if (lote == null)
            {
                return NotFound();
            }

            return View(lote);
        }

        // GET: Lotes/Create
        public IActionResult Create()
        {
            ViewData["FK_Leilao"] = new SelectList(_leilao.ReadCurrent(User.Identity.GetUserId<string>()), "ID_Leilao", "Titulo");
            return View();
        }

        // POST: Lotes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ID_Lote,FK_Leilao,ValorMinimo,ValorMaximo")] Lote lote)
        {
            if (ModelState.IsValid)
            {
                _lote.Create(lote);
                return RedirectToAction(nameof(Index));
            }
            ViewData["FK_Leilao"] = new SelectList(_leilao.ReadCurrent(User.Identity.GetUserId<string>()), "ID_Leilao", "Titulo");
            return View(lote);
        }

        // GET: Lotes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Lote lote = _lote.Read(id.Value);
            if (lote == null)
            {
                return NotFound();
            }
            ViewData["FK_Leilao"] = new SelectList(_leilao.ReadCurrent(User.Identity.GetUserId<string>()), "ID_Leilao", "Titulo");
            return View(lote);
        }

        // POST: Lotes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID_Lote,FK_Leilao,ValorMinimo,ValorMaximo")] Lote lote)
        {
            if (id != lote.ID_Lote)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _lote.Update(lote);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoteExists(lote.ID_Lote))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                ViewData["FK_Leilao"] = new SelectList(_leilao.ReadCurrent(User.Identity.GetUserId<string>()), "ID_Leilao", "Titulo");
                return RedirectToAction(nameof(Index));
            }
            return View(lote);
        }

        // GET: Lotes/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Lote lote = _lote.Read(id.Value);
            if (lote == null)
            {
                return NotFound();
            }

            return View(lote);
        }

        // POST: Lotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _lote.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool LoteExists(int id)
        {
            return _lote.Exists(id);
        }
    }
}
