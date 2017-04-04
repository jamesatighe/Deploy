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
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace Deploy.Controllers
{
    //[Authorize(Policy = "Admins")]
    public class DeployTypesController : Controller
    {
        private readonly DeployDBContext _context;

        public DeployTypesController(DeployDBContext context)
        {
            _context = context;    
        }

   
       public async Task<IActionResult> IndexSelected(int Id)
        {
            var DeployExist = _context.DeployTypes.Where(d => d.TennantID == Id).FirstOrDefault();
            if (DeployExist != null)
            {
                var deployTypes = await _context.DeployTypes.Include(x => x.Tennants).Include(x => x.TennantParams).Where(d => d.TennantID == Id).ToListAsync();
 
                //var deployTypes = await _context.DeployTypes.Where(d => d.TennantID == Id).ToListAsync();
                var viewModel = new Deploy.ViewModel.TenantDeploys();
                viewModel.TennantID = Id;
                viewModel.TennantName = "";
                viewModel.TennantName = deployTypes.First().Tennants.TennantName;
                viewModel.DeployTypes = new List<DeployType>();
                foreach (var deployType in deployTypes)
                {
                    viewModel.DeployTypes.Add(new DeployType()
                    {
                        DeployName = deployType.DeployName,
                        DeployTypeID = deployType.DeployTypeID,
                        TennantID = deployType.TennantID,
                        Tennants = deployType.Tennants,
                        DeploySaved = deployType.DeploySaved,
                        TennantParams = deployType.TennantParams,
                        DeployState = deployType.DeployState,
                        DeployResult = deployType.DeployResult,
                        AzureDeployName = deployType.AzureDeployName
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

            var deployType = await _context.DeployTypes.Include(d => d.Tennants ).SingleOrDefaultAsync(m => m.DeployTypeID == id);
            if (deployType == null)
            {
                return NotFound();
            }
            var viewModel = new TenantDeploys();
            viewModel.TennantID = deployType.TennantID;
            viewModel.DeployTypeID = deployType.DeployTypeID;
            viewModel.DeployName = deployType.DeployName;
            viewModel.TennantName = deployType.Tennants.TennantName;
            viewModel.ResourceGroupLocation = deployType.Tennants.ResourceGroupLocation;

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
            viewModel.DeploySaved = "No";
            viewModel.ResourceGroupLocation = tennant.ResourceGroupLocation;
            
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
                return RedirectToAction("IndexCount", "Tennant");
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

        public IActionResult DeployResults(int Id)
        {
            var deployTypes = _context.DeployTypes.Where(d => d.DeployTypeID == Id).FirstOrDefault();
            var viewModel = new TenantDeploys();
            viewModel.DeployTypeID = deployTypes.DeployTypeID;
            viewModel.DeployResult = deployTypes.DeployResult;
            viewModel.TennantID = deployTypes.TennantID;
            return View(viewModel);
        }



        // POST: DeployTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DeployType deployType)
        {
            if (ModelState.IsValid)
            {
                //Logic to generate valid viewmodel with AzureDeployName parameter.
                if (deployType.DeployName == "RDS Small Solution")
                {

                    var newdeploy = new DeployType();
                    newdeploy.DeployName = "RDSSmall (VNET)";
                    newdeploy.DeploySaved = "No";
                    newdeploy.TennantID = deployType.TennantID;
                    newdeploy.AzureDeployName = "rdssmallsolution";
                    _context.Add(newdeploy);
                    await _context.SaveChangesAsync();

                    var newdeploy1 = new DeployType();
                    newdeploy1.DeployName = "RDSSmall (IDS)";
                    newdeploy1.DeploySaved = "No";
                    newdeploy1.TennantID = deployType.TennantID;
                    newdeploy1.AzureDeployName = "rdssmallsolution";
                    _context.Add(newdeploy1);
                    await _context.SaveChangesAsync();

                    var newdeploy2 = new DeployType();
                    newdeploy2.DeployName = "RDSSmall (RDSS)";
                    newdeploy2.DeploySaved = "No";
                    newdeploy2.TennantID = deployType.TennantID;
                    newdeploy2.AzureDeployName = "rdssmallsolution";
                    _context.Add(newdeploy2);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    if (deployType.DeployName == "RDS Medium Solution")
                    {
                        var newdeploy = new DeployType();
                        newdeploy.DeployName = "RDSMedium (VNET)";
                        newdeploy.DeploySaved = "No";
                        newdeploy.TennantID = deployType.TennantID;
                        newdeploy.AzureDeployName = "rdsmedsolution";
                        _context.Add(newdeploy);
                        await _context.SaveChangesAsync();

                        var newdeploy1 = new DeployType();
                        newdeploy1.DeployName = "RDSMedium (IDM)";
                        newdeploy1.DeploySaved = "No";
                        newdeploy1.TennantID = deployType.TennantID;
                        newdeploy1.AzureDeployName = "rdsmedsolution";
                        _context.Add(newdeploy1);
                        await _context.SaveChangesAsync();

                        var newdeploy2 = new DeployType();
                        newdeploy2.DeployName = "RDSMedium (RDSM)";
                        newdeploy2.DeploySaved = "No";
                        newdeploy2.TennantID = deployType.TennantID;
                        newdeploy2.AzureDeployName = "rdsmedsolution";
                        _context.Add(newdeploy2);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        if (deployType.DeployName == "RDS Small")
                        {
                            var newdeploy = new DeployType();
                            newdeploy.DeployName = "RDS Small";
                            newdeploy.DeploySaved = "No";
                            newdeploy.TennantID = deployType.TennantID;
                            newdeploy.AzureDeployName = "rdssmall";
                            _context.Add(newdeploy);
                            await _context.SaveChangesAsync();

                        }
                        else
                        {
                            if (deployType.DeployName == "Identity Small")
                            {
                                var newdeploy = new DeployType();
                                newdeploy.DeployName = "Identity Small";
                                newdeploy.DeploySaved = "No";
                                newdeploy.TennantID = deployType.TennantID;
                                newdeploy.AzureDeployName = "identitysmall";
                                _context.Add(newdeploy);
                                await _context.SaveChangesAsync();
                            }
                            else
                            {
                                if (deployType.DeployName == "Identity Medium")
                                {
                                    var newdeploy = new DeployType();
                                    newdeploy.DeployName = "Identity Medium";
                                    newdeploy.DeploySaved = "No";
                                    newdeploy.TennantID = deployType.TennantID;
                                    newdeploy.AzureDeployName = "identitymedium";
                                    _context.Add(newdeploy);
                                    await _context.SaveChangesAsync();

                                }
                                else
                                {
                                    if (deployType.DeployName == "RDS Medium")
                                    {
                                        var newdeploy = new DeployType();
                                        newdeploy.DeployName = "RDS Medium";
                                        newdeploy.DeploySaved = "No";
                                        newdeploy.TennantID = deployType.TennantID;
                                        newdeploy.AzureDeployName = "rdsmedium";
                                        _context.Add(newdeploy);
                                        await _context.SaveChangesAsync();

                                    }
                                    else
                                    {
                                        if (deployType.DeployName == "VNET")
                                        {
                                            var newdeploy = new DeployType();
                                            newdeploy.DeployName = "VNET";
                                            newdeploy.DeploySaved = "No";
                                            newdeploy.TennantID = deployType.TennantID;
                                            newdeploy.AzureDeployName = "VNET";
                                            _context.Add(newdeploy);
                                            await _context.SaveChangesAsync();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
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
