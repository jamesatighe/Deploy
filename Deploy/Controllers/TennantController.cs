using Deploy.DAL;
using Deploy.Models;
using Deploy.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deploy.Controllers
{
    //[Authorize(Policy = "Admins")]
    public class TennantController : Controller
    {
        private readonly DeployDBContext _context;

        public TennantController(DeployDBContext context)
        {
            _context = context;    
        }

        // GET: Deploy
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Tennants.ToListAsync());
        //}

        public async Task<IActionResult> IndexCount(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "TenantName_desc" : "";
            ViewBag.DeployCountSortParm = sortOrder == "DeployCount_desc" ? "DeployCount" : "DeployCount_desc";

            var tennants = await _context.Tennants.Include(x => x.DeployTypes).ToListAsync();

            var viewModel = new List<Deploy.ViewModel.TenantDeploys>();
            foreach (var tennant in tennants)
            {
                viewModel.Add(new ViewModel.TenantDeploys()
                {
                    TennantID = tennant.TennantID,
                    TennantName = tennant.TennantName,
                    DeployCount = tennant.DeployTypes.Count,
                    AzureTennantID = tennant.AzureTennantID,
                    ResourceGroupName = tennant.ResourceGroupName,
                    AzureClientID = tennant.AzureClientID,
                    AzureClientSecret = tennant.AzureClientSecret,
                    AzureSubscriptionID = tennant.AzureSubscriptionID
                });
            }

            //Search box
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToString();
                searchString = searchString.ToUpper();
                viewModel = viewModel.Where(t => t.TennantName.ToUpper().Contains(searchString)).ToList();
            }

            switch (sortOrder)
            {
                case "TenantName_desc":
                    viewModel = viewModel.OrderByDescending(t => t.TennantName).ToList();
                    break;
                case "TenantName":
                    viewModel = viewModel.OrderBy(t => t.TennantName).ToList();
                    break;
                case "DeployCount_desc":
                    viewModel = viewModel.OrderByDescending(t => t.DeployCount).ToList();
                    break;
                case "DeployCount":
                    viewModel = viewModel.OrderBy(t => t.DeployCount).ToList();
                    break;
                default:
                    viewModel = viewModel.OrderBy(t => t.TennantName).ToList();
                    break;
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
            viewModel.AzureTennantID = tennants.AzureTennantID;
            viewModel.ResourceGroupName = tennants.ResourceGroupName;
            viewModel.AzureClientID = tennants.AzureClientID;
            viewModel.AzureClientSecret = tennants.AzureClientSecret;
            viewModel.AzureSubscriptionID = tennants.AzureSubscriptionID;

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
        public async Task<IActionResult> Create(Tennant tennant)
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
            viewModel.AzureTennantID = tennant.AzureTennantID;
            viewModel.ResourceGroupName = tennant.ResourceGroupName;
            viewModel.ResourceGroupLocation = tennant.ResourceGroupLocation;
            viewModel.AzureClientID = tennant.AzureClientID;
            viewModel.AzureClientSecret = tennant.AzureClientSecret;
            viewModel.AzureSubscriptionID = tennant.AzureSubscriptionID;

            return View(viewModel);
        }

        // POST: Deploy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Tennant tennant)
        {
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
