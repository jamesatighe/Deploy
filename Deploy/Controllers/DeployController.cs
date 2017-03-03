using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Deploy.DAL;
using Deploy.Models;
using Deploy.ViewModel;

namespace Deploy.Controllers
{
    public class DeployController : Controller
    {
        private readonly DeployDBContext _context;

        public DeployController(DeployDBContext context)
        {
            _context = context;    
        }

        // GET: Deploy
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Tennants.ToListAsync());
        //}

        public async Task<IActionResult> IndexCount()
        {
            var tennants = await _context.Tennants.Include(x => x.DeployTypes).ToListAsync();

            var viewModel = new List<Deploy.ViewModel.TenantDeploys>();
            foreach (var tennant in tennants)
            {
                viewModel.Add(new ViewModel.TenantDeploys()
                {
                    TennantID = tennant.TennantID,
                    TennantName = tennant.TennantName,
                    DeployCount = tennant.DeployTypes.Count
                });
            }
            return View(viewModel);
        }



    // GET: Deploy/Details/5
    public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tennants = await _context.Tennants.SingleOrDefaultAsync(m => m.TennantID == id);
            if (tennants == null)
            {
                return NotFound();
            }
            var viewModel = new TenantDeploys();
            viewModel.TennantID = tennants.TennantID;
            viewModel.TennantName = tennants.TennantName;
            viewModel.FirstName = tennants.FirstName;
            viewModel.LastName = tennants.LastName;
            viewModel.EmailAddress = tennants.EmailAddress;

            return View(viewModel);

        }

        // GET: Deploy/Create
        public IActionResult Create()
        {
            var viewModel = new TenantDeploys();

            return View(viewModel);
        }


        // POST: Deploy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TennantID,TennantName","FirstName","LastName","EmailAddress")] Tennant tennant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tennant);
                await _context.SaveChangesAsync();
                return RedirectToAction("IndexCount");
            }
            return View(tennant);
        }

        // GET: Deploy/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tennant = await _context.Tennants.SingleOrDefaultAsync(m => m.TennantID == id);
            if (tennant == null)
            {
                return NotFound();
            }
            var viewModel = new TenantDeploys();
            viewModel.TennantID = tennant.TennantID;
            viewModel.TennantName = tennant.TennantName;
            viewModel.FirstName = tennant.FirstName;
            viewModel.LastName = tennant.LastName;
            viewModel.EmailAddress = tennant.EmailAddress;

            return View(viewModel);
        }

        // POST: Deploy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TennantID,TennantName","FirstName","LastName","EmailAddress")] Tennant tennant)
        {
            if (id != tennant.TennantID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tennant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TennantExists(tennant.TennantID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("IndexCount");
            }
            return View(tennant);
        }

        // GET: Deploy/Deploy/5
        public IActionResult Deploy(int Id)
        {
            return RedirectToAction("IndexSelected", "DeployTypes", new { id = Id });
        }

        // GET: Deploy/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tennant = await _context.Tennants
                .SingleOrDefaultAsync(m => m.TennantID == id);
            if (tennant == null)
            {
                return NotFound();
            }

            return View(tennant);
        }

        // POST: Deploy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tennant = await _context.Tennants.SingleOrDefaultAsync(m => m.TennantID == id);
            _context.Tennants.Remove(tennant);
            await _context.SaveChangesAsync();
            return RedirectToAction("IndexCount");
        }

        private bool TennantExists(int id)
        {
            return _context.Tennants.Any(e => e.TennantID == id);
        }
    }
}
