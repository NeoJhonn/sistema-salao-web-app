using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcSistemaSalao.Data;
using MvcSistemaSalao.Models;

namespace MvcSistemaSalao.Controllers
{
    public class ClientsController : Controller
    {
        private readonly MvcSistemaSalaoContext _context;

        public ClientsController(MvcSistemaSalaoContext context)
        {
            _context = context;
        }

        // GET: Clients
        public async Task<IActionResult> Index(string? professional)
        {
            var clients = from m in _context.Client
                            select m;

            if (string.IsNullOrEmpty(professional))
            {

                var ClientVM = new ClientViewModel
                {

                    Clients = await clients.ToListAsync()
                };

                return View(ClientVM);
            }
            else
            {
                clients = clients.Where(x => x.Professional == professional);


                var ClientVM = new ClientViewModel
                {
                    Clients = await clients.ToListAsync()
                };
               
                return View(ClientVM);
            }

        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //esconder barra de navegação
            ViewBag.ShowNav = 0;

            if (id == null || _context.Client == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            //esconder barra de navegação
            ViewBag.ShowNav = 0;

            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,BirthDate,Email,PhoneNumber,Professional")] Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //esconder barra de navegação
            ViewBag.ShowNav = 0;

            if (id == null || _context.Client == null)
            {
                return NotFound();
            }

            var client = await _context.Client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,BirthDate,Email,PhoneNumber,Professional")] Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
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
            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            //esconder barra de navegação
            ViewBag.ShowNav = 0;

            if (id == null || _context.Client == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Client == null)
            {
                return Problem("Entity set 'MvcSistemaSalaoContext.Client'  is null.");
            }
            var client = await _context.Client.FindAsync(id);
            if (client != null)
            {
                _context.Client.Remove(client);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
          return _context.Client.Any(e => e.Id == id);
        }
    }
}
