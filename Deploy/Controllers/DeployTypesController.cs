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
    public class DeployTypesController : Controller
    {
        private readonly DeployDBContext _context;

        public DeployTypesController(DeployDBContext context)
        {
            _context = context;    
        }

   


        // GET: DeployTypes
        //public async Task<IActionResult> Index(int? id)
        //{
        //       var deployDBContext = _context.DeployTypes.Include(d => d.Tennants);
        //        return View(await deployDBContext.ToListAsync());
        //}

        public async Task<IActionResult> IndexSelected(int Id)
        {
            var DeployExist = _context.DeployTypes.Where(d => d.TennantID == Id).FirstOrDefault();
            if (DeployExist != null)
            {
                var deployTypes = await _context.DeployTypes.Include(x => x.Tennants).Where(d => d.TennantID == Id).ToListAsync();
                //var deployTypes = await _context.DeployTypes.Where(d => d.TennantID == Id).ToListAsync();
                var viewModel = new Deploy.ViewModel.TenantDeploys();
                viewModel.TennantID = Id;
                viewModel.TennantName = "";
                viewModel.TennantName = deployTypes.First().Tennants.TennantName;

                viewModel.DeployTypes = new List<Deploy.Models.DeployType>();
                foreach (var deployType in deployTypes)
                {
                    viewModel.DeployTypes.Add(new DeployType()
                    {
                        DeployName = deployType.DeployName,
                        DeployTypeID = deployType.DeployTypeID,
                        TennantID = deployType.TennantID,
                        Tennants = deployType.Tennants
                    });
                }

                return View(viewModel);
            }
            else
            {

                return RedirectToAction("Create", new { id = Id });;
            }
        }




        // GET: DeployTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deployType = await _context.DeployTypes.Include(d => d.Tennants).SingleOrDefaultAsync(m => m.DeployTypeID == id);
            if (deployType == null)
            {
                return NotFound();
            }
            var viewModel = new TenantDeploys();
            viewModel.TennantID = deployType.TennantID;
            viewModel.DeployTypeID = deployType.DeployTypeID;
            viewModel.DeployName = deployType.DeployName;
            viewModel.TennantName = deployType.Tennants.TennantName;

            return View(viewModel);
        }

        // GET: DeployTypes/Create
        public IActionResult Create(int Id)
        {
            //DeployType Deploy = new DeployType { TennantID = Id };
            var tennant = _context.Tennants.Where(d => d.TennantID == Id).FirstOrDefault();
            var viewModel = new TenantDeploys();
            viewModel.TennantID = Id;
            viewModel.TennantName = tennant.TennantName;
            
            return View(viewModel);
        
        }

        // GET: DeployTypes/CreateRedirect
        public IActionResult CreateRedirect(int Id)
        {
            var DeployExist = _context.DeployTypes.Where(d => d.TennantID == Id).FirstOrDefault();
            if (DeployExist != null)
            {
                return RedirectToAction("IndexSelected", "DeployTypes", new { id = Id });
            }
            else
            {
                return RedirectToAction("IndexCount", "Deploy");
            }


        }


        public IActionResult Deploy(int Id)
        {
            return RedirectToAction("IndexSelected", "DeployTypes", new { id = Id });
        }

        public IActionResult DeployParams(int Id)
        {
            return RedirectToAction("IndexSelected", "TennantParams", new { id = Id });
        }



        // POST: DeployTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeployTypeID,TennantID,DeployName")] DeployType deployType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deployType);
                await _context.SaveChangesAsync();
                return RedirectToAction("IndexSelected", new { id = deployType.TennantID });
            }
            ViewData["TennantID"] = new SelectList(_context.Tennants, "TennantID", "TennantID", deployType.TennantID);
            return View(deployType);
        }

        //// GET: DeployTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deployType = await _context.DeployTypes.SingleOrDefaultAsync(m => m.DeployTypeID == id);
            if (deployType == null)
            {
                return NotFound();
            }
            var viewModel = new TenantDeploys();
            viewModel.DeployTypeID = deployType.DeployTypeID;
            viewModel.DeployName = deployType.DeployName;
            viewModel.TennantID = deployType.TennantID;
            //ViewData["TennantID"] = new SelectList(_context.Tennants, "TennantID", "TennantID", deployType.TennantID);
            return View(viewModel);
        }


        // POST: DeployTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DeployTypeID,TennantID,DeployName")] DeployType deployType)
        {
            if (id != deployType.DeployTypeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deployType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeployTypeExists(deployType.DeployTypeID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("IndexSelected", new { id = deployType.TennantID });
            }
            ViewData["TennantID"] = new SelectList(_context.Tennants, "TennantID", "TennantID", deployType.TennantID);
            return View(deployType);
        }

        // GET: DeployTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deployType = await _context.DeployTypes
                .Include(d => d.Tennants)
                .SingleOrDefaultAsync(m => m.DeployTypeID == id);
            if (deployType == null)
            {
                return NotFound();
            }

            return View(deployType);
        }

        // POST: DeployTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deployType = await _context.DeployTypes.SingleOrDefaultAsync(m => m.DeployTypeID == id);
            _context.DeployTypes.Remove(deployType);
            await _context.SaveChangesAsync();
            return RedirectToAction("IndexSelected", new { id = deployType.TennantID });
        }

        private bool DeployTypeExists(int id)
        {
            return _context.DeployTypes.Any(e => e.DeployTypeID == id);
        }
    }
}
