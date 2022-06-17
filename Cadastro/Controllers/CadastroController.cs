using Cadastro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Controllers
{
    public class CadastroController : Controller
    {
        private readonly Context _context;

        public CadastroController(Context context)
        {
            _context = context;
        }

        // GET: Cadastros
        public async Task<IActionResult> Index()
        {
            ViewBag.Nome = (from c in _context.Cadastros
                            select c.nome).Distinct();

            ViewBag.Email = (from c in _context.Cadastros
                             select c.email).Distinct();
            return View(await _context.Cadastros.ToListAsync());
        }

        // GET: Cadastros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cadastros == null)
            {
                return NotFound();
            }

            var dbCadastro = await _context.Cadastros
                .FirstOrDefaultAsync(m => m.id == id);
            if (dbCadastro == null)
            {
                return NotFound();
            }

            return View(dbCadastro);
        }

        // GET: Cadastros/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nome,email,rua,numero,cep,cidade,estado")] DbCadastro dbCadastro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dbCadastro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dbCadastro);
        }

        // GET: Cadastros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cadastros == null)
            {
                return NotFound();
            }

            var dbCadastro = await _context.Cadastros.FindAsync(id);
            if (dbCadastro == null)
            {
                return NotFound();
            }
            return View(dbCadastro);
        }

        // POST: Cadastros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nome,email,rua,numero,cep,cidade,estado")] DbCadastro dbCadastro)
        {
            if (id != dbCadastro.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dbCadastro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DbCadastroExists(dbCadastro.id))
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
            return View(dbCadastro);
        }

        // GET: Cadastros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cadastros == null)
            {
                return NotFound();
            }

            var dbCadastro = await _context.Cadastros
                .FirstOrDefaultAsync(m => m.id == id);
            if (dbCadastro == null)
            {
                return NotFound();
            }

            return View(dbCadastro);
        }

        // POST: Cadastros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cadastros == null)
            {
                return Problem("Entity set 'Context.Cadastros'  is null.");
            }
            var dbCadastro = await _context.Cadastros.FindAsync(id);
            if (dbCadastro != null)
            {
                _context.Cadastros.Remove(dbCadastro);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DbCadastroExists(int id)
        {
            return (_context.Cadastros?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
