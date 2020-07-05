using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Leilao.PL;
using Leilao;
using Microsoft.AspNetCore.Authorization;

namespace WebLeilao.Controllers
{
    [Authorize]
    public class ProdutosController : Controller
    {
        private readonly LeilaoContext _context;
        private readonly IManager<Produto> _produto = new Facade().GetProdutoManager();
        private readonly IManager<Lote> _lote = new Facade().GetLoteManager();

        public ProdutosController(LeilaoContext context)
        {
            _context = context;
        }

        // GET: Produtoes
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(_produto.Read());
        }

        // GET: Produtoes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Produto produto = _produto.Read(id.Value);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // GET: Produtoes/Create
        public IActionResult Create()
        {
            ViewData["FK_Lote"] = new SelectList(_lote.Read(), "ID_Lote", "ID_Lote");
            return View();
        }

        // POST: Produtoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ID_Produto,FK_Lote,DescricaoCurta,DescricaoLonga,Categoria")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                _produto.Create(produto);
                return RedirectToAction(nameof(Index));
            }
            ViewData["FK_Lote"] = new SelectList(_lote.Read(), "ID_Lote", "ID_Lote", produto.FK_Lote);
            return View(produto);
        }

        // GET: Produtoes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Produto produto = _produto.Read(id.Value);
            if (produto == null)
            {
                return NotFound();
            }
            ViewData["FK_Lote"] = new SelectList(_lote.Read(), "ID_Lote", "ID_Lote", produto.FK_Lote);
            return View(produto);
        }

        // POST: Produtoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID_Produto,FK_Lote,DescricaoCurta,DescricaoLonga,Categoria")] Produto produto)
        {
            if (id != produto.ID_Produto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _produto.Update(produto);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.ID_Produto))
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
            ViewData["FK_Lote"] = new SelectList(_lote.Read(), "ID_Lote", "ID_Lote", produto.FK_Lote);
            return View(produto);
        }

        // GET: Produtoes/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Produto produto = _produto.Read(id.Value);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: Produtoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _produto.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(int id)
        {
            return _produto.Exists(id);
        }
    }
}
