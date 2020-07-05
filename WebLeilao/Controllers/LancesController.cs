using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Leilao;
using Leilao.PL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;

namespace WebLeilao.Controllers
{
    [Authorize]
    public class LancesController : Controller
    {

        private readonly IManager<Lance> lm = new Facade().GetLanceManager();
        private readonly IManager<Leilao.PL.Leilao> leilao_m = new Facade().GetLeilaoManager();
        private readonly IManager<Usuario> usuario_m = new Facade().GetUsuarioManager();
        private readonly IManagerLeilao<Leilao.PL.Leilao> _leilao_f = new Facade().GetLeilaoFunctions();

        private readonly LeilaoContext _context;

        public LancesController(LeilaoContext context)
        {
            _context = context;
        }

        // GET: Lances
        public IActionResult Index()
        {
            // var leilaoContext = _context.Lances.Include(l => l.Leilao).Include(l => l.Usuario);
            return View(lm.ReadCurrent(User.Identity.GetUserId<string>()));
        }

        // GET: Lances/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*
            var lance = await _context.Lances
                .Include(l => l.Leilao)
                .Include(l => l.Usuario)
                .FirstOrDefaultAsync(m => m.ID_Lance == id);
            */

            Lance lance = lm.Read(id.Value);

            if (lance == null)
            {
                return NotFound();
            }

            return View(lance);
        }

        // GET: Lances/Create
        public IActionResult Create()
        {
            ViewData["FK_Leilao"] = new SelectList(_leilao_f.ReadLeiloesParaDarLance(User.Identity.GetUserId<string>()), "ID_Leilao", "Titulo");
            ViewData["FK_Usuario"] = new SelectList(usuario_m.ReadCurrent(User.Identity.GetUserId<string>()), "ID_Usuario", "Nome");   
            return View();
        }

        // POST: Lances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ID_Lance,FK_Leilao,FK_Usuario,Valor")] Lance lance)
        {
            if (ModelState.IsValid)
            {
                lm.Create(lance);
                return RedirectToAction(nameof(Index));
            }
            ViewData["FK_Leilao"] = new SelectList(_leilao_f.ReadLeiloesParaDarLance(User.Identity.GetUserId<string>()), "ID_Leilao", "Titulo");
            ViewData["FK_Usuario"] = new SelectList(usuario_m.ReadCurrent(User.Identity.GetUserId<string>()), "ID_Usuario", "Nome");
            return View(lance);
        }

        // GET: Lances/Edit/5
        [Authorize]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Lance lance = lm.Read(id.Value);

            if (lance == null)
            {
                return NotFound();
            }
            ViewData["FK_Leilao"] = new SelectList(_leilao_f.ReadLeiloesParaDarLance(User.Identity.GetUserId<string>()), "ID_Leilao", "Titulo");
            ViewData["FK_Usuario"] = new SelectList(usuario_m.ReadCurrent(User.Identity.GetUserId<string>()), "ID_Usuario", "Nome");
            return View(lance);
        }

        // POST: Lances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Edit(int id, [Bind("ID_Lance,FK_Leilao,FK_Usuario,Valor")] Lance lance)
        {
            if (id != lance.ID_Lance)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    lm.Update(lance);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LanceExists(lance.ID_Lance))
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
            
            ViewData["FK_Leilao"] = new SelectList(_leilao_f.ReadLeiloesParaDarLance(User.Identity.GetUserId<string>()), "ID_Leilao", "Titulo", lance.FK_Leilao);
            ViewData["FK_Usuario"] = new SelectList(usuario_m.ReadCurrent(User.Identity.GetUserId<string>()), "ID_Usuario", "Nome", lance.FK_Usuario);
            return View(lance);
        }

        // GET: Lances/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Lance lance = lm.Read(id.Value);
            if (lance == null)
            {
                return NotFound();
            }

            return View(lance);
        }

        // POST: Lances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            lm.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool LanceExists(int id)
        {
            return lm.Exists(id);
        }
    }
}
