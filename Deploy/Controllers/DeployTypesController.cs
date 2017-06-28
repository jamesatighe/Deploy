using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Deploy.DAL;
using Deploy.Service;
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
        private readonly DeployTypesService _service;

        public DeployTypesController(DeployDBContext context)
        {
            _context = context;
            _service = new DeployTypesService(_context);
        }

   
       public async Task<IActionResult> IndexSelected(int Id,string sortOrder, string searchString, bool DeployExists, bool TemplateInvalid, string TemplateError)
        {
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "DeployName_desc" : "";

            ViewBag.DeployExists = DeployExists;
            ViewBag.TemplateInvalid = TemplateInvalid;
            ViewBag.TemplateError = TemplateError;


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
                        AzureDeployName = deployType.AzureDeployName,
                        ResourceGroupName = deployType.ResourceGroupName
                    });
                }

                if (!string.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToString();
                    searchString = searchString.ToUpper();
                    viewModel.DeployTypes = viewModel.DeployTypes.Where(t => t.DeployName.ToUpper().Contains(searchString)).ToList();
                }

                switch (sortOrder)
                {
                    case "DeployName_desc":
                        viewModel.DeployTypes = viewModel.DeployTypes.OrderByDescending(t => t.DeployName).ToList();
                        break;
                    case "TenantName":
                        viewModel.DeployTypes = viewModel.DeployTypes.OrderBy(t => t.DeployName).ToList();
                        break;
                    default:
                        viewModel.DeployTypes = viewModel.DeployTypes.OrderBy(t => t.DeployName).ToList();
                        break;
                }
                return View(viewModel);
            }
            else
            {

                return RedirectToAction("CreateNew", new { id = Id });;
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


        // GET: DeployTypes/CreateNew
        public IActionResult CreateNew(int Id)
        {
            //DeployType Deploy = new DeployType { TennantID = Id };
            var tennant = _context.Tennants.Where(d => d.TennantID == Id).FirstOrDefault();
            var viewModel = new DeployChoiceViewModel();
            viewModel.TennantID = Id;
            viewModel.TennantName = tennant.TennantName;
            viewModel.DeploySaved = "No";
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


        //// POST: DeployTypes/CreateNew
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNew(DeployChoiceViewModel choices)
        {
            if (ModelState.IsValid)
            {
                //Logic to generate valid viewmodel with AzureDeployName parameter.


                var service = new DeployTypesService(_context);
                var DeployChoices = _service.CreateDeployTypes(choices);

                if (DeployChoices.DeployName == "RDS Small Solution")
                {
                    Guid guid = Guid.NewGuid();
                    var newdeploy = new DeployType();
                    newdeploy.DeployName = "RDSSmall (VNET)";
                    newdeploy.DeploySaved = "No";
                    newdeploy.TennantID = DeployChoices.TennantID;
                    newdeploy.DeployFile = DeployChoices.DeployFile;
                    newdeploy.ParamsFile = DeployChoices.ParamsFile;
                    newdeploy.DeployCode = "VNET";
                    newdeploy.ResourceGroupName = DeployChoices.ResourceGroupName;
                    newdeploy.AzureDeployName = "rdssmallVNET" + guid;
                    _context.Add(newdeploy);
                    await _context.SaveChangesAsync();

                    Guid guid1 = Guid.NewGuid();
                    var newdeploy1 = new DeployType();
                    newdeploy1.DeployName = "RDSSmall (IDS)";
                    newdeploy1.DeploySaved = "No";
                    newdeploy1.TennantID = DeployChoices.TennantID;
                    newdeploy1.DeployFile = DeployChoices.DeployFile;
                    newdeploy1.ParamsFile = DeployChoices.ParamsFile;
                    newdeploy1.DeployCode = "ADS";
                    newdeploy1.ResourceGroupName = DeployChoices.ResourceGroupName;
                    newdeploy1.AzureDeployName = "rdssmallID" + guid1;
                    _context.Add(newdeploy1);
                    await _context.SaveChangesAsync();

                    Guid guid2 = Guid.NewGuid();
                    var newdeploy2 = new DeployType();
                    newdeploy2.DeployName = "RDSSmall (RDSS)";
                    newdeploy2.DeploySaved = "No";
                    newdeploy2.TennantID = DeployChoices.TennantID;
                    newdeploy2.DeployFile = DeployChoices.DeployFile;
                    newdeploy2.ParamsFile = DeployChoices.ParamsFile;
                    newdeploy2.DeployCode = "RDSS";
                    newdeploy2.ResourceGroupName = DeployChoices.ResourceGroupName;
                    newdeploy2.AzureDeployName = "rdssmallRDS" + guid2;
                    _context.Add(newdeploy2);
                    await _context.SaveChangesAsync();
                }
                if (DeployChoices.DeployName == "RDS Medium Solution")
                {
                    Guid guid = Guid.NewGuid();
                    var newdeploy = new DeployType();
                    newdeploy.DeployName = "RDSMedium (VNET)";
                    newdeploy.DeploySaved = "No";
                    newdeploy.TennantID = DeployChoices.TennantID;
                    newdeploy.DeployFile = DeployChoices.DeployFile;
                    newdeploy.ParamsFile = DeployChoices.ParamsFile;
                    newdeploy.DeployCode = "VNET";
                    newdeploy.ResourceGroupName = DeployChoices.ResourceGroupName;
                    newdeploy.AzureDeployName = "rdsmedVNET" + guid;
                    _context.Add(newdeploy);
                    await _context.SaveChangesAsync();

                    Guid guid1 = Guid.NewGuid();
                    var newdeploy1 = new DeployType();
                    newdeploy1.DeployName = "RDSMedium (IDM)";
                    newdeploy1.DeploySaved = "No";
                    newdeploy1.TennantID = DeployChoices.TennantID;
                    newdeploy1.DeployFile = DeployChoices.DeployFile;
                    newdeploy1.ParamsFile = DeployChoices.ParamsFile;
                    newdeploy1.DeployCode = "ADM";
                    newdeploy1.ResourceGroupName = DeployChoices.ResourceGroupName;
                    newdeploy1.AzureDeployName = "rdsmedID" + guid1;
                    _context.Add(newdeploy1);
                    await _context.SaveChangesAsync();

                    Guid guid2 = Guid.NewGuid();
                    var newdeploy2 = new DeployType();
                    newdeploy2.DeployName = "RDSMedium (RDSM)";
                    newdeploy2.DeploySaved = "No";
                    newdeploy2.TennantID = DeployChoices.TennantID;
                    newdeploy2.DeployFile = DeployChoices.DeployFile;
                    newdeploy2.ParamsFile = DeployChoices.ParamsFile;
                    newdeploy2.DeployCode = "RDSM";
                    newdeploy2.ResourceGroupName = DeployChoices.ResourceGroupName;
                    newdeploy2.AzureDeployName = "rdsmedRDS" + guid2;
                    _context.Add(newdeploy2);
                    await _context.SaveChangesAsync();
                }

                if (DeployChoices.DeployName == "RDS Small (Typetec)")
                {
                    Guid guid = Guid.NewGuid();
                    var newdeploy = new DeployType();
                    newdeploy.DeployName = "RDSMedium (VNET)";
                    newdeploy.DeploySaved = "No";
                    newdeploy.TennantID = DeployChoices.TennantID;
                    newdeploy.DeployFile = DeployChoices.DeployFile;
                    newdeploy.ParamsFile = DeployChoices.ParamsFile;
                    newdeploy.DeployCode = "VNET";
                    newdeploy.ResourceGroupName = DeployChoices.ResourceGroupName;
                    newdeploy.AzureDeployName = "VNET" + guid;
                    _context.Add(newdeploy);
                    await _context.SaveChangesAsync();

                    Guid guid1 = Guid.NewGuid();
                    var newdeploy1 = new DeployType();
                    newdeploy1.DeployName = "RDSMedium (IDSTYPE)";
                    newdeploy1.DeploySaved = "No";
                    newdeploy1.TennantID = DeployChoices.TennantID;
                    newdeploy1.DeployFile = DeployChoices.DeployFile;
                    newdeploy1.ParamsFile = DeployChoices.ParamsFile;
                    newdeploy1.DeployCode = "ADSTYPE";
                    newdeploy1.ResourceGroupName = DeployChoices.ResourceGroupName;
                    newdeploy1.AzureDeployName = "identitysmall" + guid1;
                    _context.Add(newdeploy1);
                    await _context.SaveChangesAsync();

                    Guid guid2 = Guid.NewGuid();
                    var newdeploy2 = new DeployType();
                    newdeploy2.DeployName = "RDSSmall (RDSSTYPE)";
                    newdeploy2.DeploySaved = "No";
                    newdeploy2.TennantID = DeployChoices.TennantID;
                    newdeploy2.DeployFile = DeployChoices.DeployFile;
                    newdeploy2.ParamsFile = DeployChoices.ParamsFile;
                    newdeploy2.DeployCode = "RDSSTYPE";
                    newdeploy2.ResourceGroupName = DeployChoices.ResourceGroupName;
                    newdeploy2.AzureDeployName = "rdssmall" + guid2;
                    _context.Add(newdeploy2);
                    await _context.SaveChangesAsync();
                }

                if (DeployChoices.DeployName == "RDS Medium (Typetec)")
                {
                    Guid guid = Guid.NewGuid();
                    var newdeploy = new DeployType();
                    newdeploy.DeployName = "RDSMedium (VNET)";
                    newdeploy.DeploySaved = "No";
                    newdeploy.TennantID = DeployChoices.TennantID;
                    newdeploy.DeployFile = DeployChoices.DeployFile;
                    newdeploy.ParamsFile = DeployChoices.ParamsFile;
                    newdeploy.DeployCode = "VNET";
                    newdeploy.ResourceGroupName = DeployChoices.ResourceGroupName;
                    newdeploy.AzureDeployName = "VNET" + guid;
                    _context.Add(newdeploy);
                    await _context.SaveChangesAsync();

                    Guid guid1 = Guid.NewGuid();
                    var newdeploy1 = new DeployType();
                    newdeploy1.DeployName = "RDSMedium (IDMTYPE)";
                    newdeploy1.DeploySaved = "No";
                    newdeploy1.TennantID = DeployChoices.TennantID;
                    newdeploy1.DeployFile = DeployChoices.DeployFile;
                    newdeploy1.ParamsFile = DeployChoices.ParamsFile;
                    newdeploy1.DeployCode = "ADMTYPE";
                    newdeploy1.ResourceGroupName = DeployChoices.ResourceGroupName;
                    newdeploy1.AzureDeployName = "identity" + guid1;
                    _context.Add(newdeploy1);
                    await _context.SaveChangesAsync();

                    Guid guid2 = Guid.NewGuid();
                    var newdeploy2 = new DeployType();
                    newdeploy2.DeployName = "RDSMedium (RDSMTYPE)";
                    newdeploy2.DeploySaved = "No";
                    newdeploy2.TennantID = DeployChoices.TennantID;
                    newdeploy2.DeployFile = DeployChoices.DeployFile;
                    newdeploy2.ParamsFile = DeployChoices.ParamsFile;
                    newdeploy2.DeployCode = "RDSMTYPE";
                    newdeploy2.ResourceGroupName = DeployChoices.ResourceGroupName;
                    newdeploy2.AzureDeployName = "rdsmed" + guid2;
                    _context.Add(newdeploy2);
                    await _context.SaveChangesAsync();
                }


                else
                {
                    Guid guid = Guid.NewGuid();
                    var newdeploy = new DeployType();
                    newdeploy.DeployName = DeployChoices.DeployName;
                    newdeploy.DeploySaved = "No";
                    newdeploy.TennantID = DeployChoices.TennantID;
                    newdeploy.DeployFile = DeployChoices.DeployFile;
                    newdeploy.ParamsFile = DeployChoices.ParamsFile;
                    newdeploy.DeployCode = DeployChoices.DeployCode;
                    newdeploy.ResourceGroupName = DeployChoices.ResourceGroupName;
                    newdeploy.AzureDeployName = DeployChoices.DeployCode + guid;
                    _context.Add(newdeploy);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction("IndexSelected", new { id = DeployChoices.TennantID });
            }
            return View(choices);
        }



        // POST: DeployTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(DeployType deployType)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //Logic to generate valid viewmodel with AzureDeployName parameter.
        //        if (deployType.DeployName == "RDS Small Solution")
        //        {
        //            Guid guid = Guid.NewGuid();
        //            var newdeploy = new DeployType();
        //            newdeploy.DeployName = "RDSSmall (VNET)";
        //            newdeploy.DeploySaved = "No";
        //            newdeploy.TennantID = deployType.TennantID;
        //            newdeploy.AzureDeployName = "rdssmallVNET" + guid;
        //            _context.Add(newdeploy);
        //            await _context.SaveChangesAsync();

        //            Guid guid1 = Guid.NewGuid();
        //            var newdeploy1 = new DeployType();
        //            newdeploy1.DeployName = "RDSSmall (IDS)";
        //            newdeploy1.DeploySaved = "No";
        //            newdeploy1.TennantID = deployType.TennantID;
        //            newdeploy1.AzureDeployName = "rdssmallID" + guid1;
        //            _context.Add(newdeploy1);
        //            await _context.SaveChangesAsync();

        //            Guid guid2 = Guid.NewGuid();
        //            var newdeploy2 = new DeployType();
        //            newdeploy2.DeployName = "RDSSmall (RDSS)";
        //            newdeploy2.DeploySaved = "No";
        //            newdeploy2.TennantID = deployType.TennantID;
        //            newdeploy2.AzureDeployName = "rdssmallRDS" + guid2;
        //            _context.Add(newdeploy2);
        //            await _context.SaveChangesAsync();
        //        }
        //        else
        //        {
        //            if (deployType.DeployName == "RDS Medium Solution")
        //            {
        //                Guid guid = Guid.NewGuid();
        //                var newdeploy = new DeployType();
        //                newdeploy.DeployName = "RDSMedium (VNET)";
        //                newdeploy.DeploySaved = "No";
        //                newdeploy.TennantID = deployType.TennantID;
        //                newdeploy.AzureDeployName = "rdsmedVNET" + guid;
        //                _context.Add(newdeploy);
        //                await _context.SaveChangesAsync();

        //                Guid guid1 = Guid.NewGuid();
        //                var newdeploy1 = new DeployType();
        //                newdeploy1.DeployName = "RDSMedium (IDM)";
        //                newdeploy1.DeploySaved = "No";
        //                newdeploy1.TennantID = deployType.TennantID;
        //                newdeploy1.AzureDeployName = "rdsmedID" + guid1;
        //                _context.Add(newdeploy1);
        //                await _context.SaveChangesAsync();

        //                Guid guid2 = Guid.NewGuid();
        //                var newdeploy2 = new DeployType();
        //                newdeploy2.DeployName = "RDSMedium (RDSM)";
        //                newdeploy2.DeploySaved = "No";
        //                newdeploy2.TennantID = deployType.TennantID;
        //                newdeploy2.AzureDeployName = "rdsmedRDS" + guid2;
        //                _context.Add(newdeploy2);
        //                await _context.SaveChangesAsync();
        //            }
        //            else
        //            {
        //                if (deployType.DeployName == "RDS Small")
        //                {
        //                    Guid guid = Guid.NewGuid();
        //                    var newdeploy = new DeployType();
        //                    newdeploy.DeployName = "RDS Small";
        //                    newdeploy.DeploySaved = "No";
        //                    newdeploy.TennantID = deployType.TennantID;
        //                    newdeploy.AzureDeployName = "rdssmall" + guid;
        //                    _context.Add(newdeploy);
        //                    await _context.SaveChangesAsync();

        //                }
        //                else
        //                {
        //                    if (deployType.DeployName == "Identity Small")
        //                    {
        //                        Guid guid = Guid.NewGuid();
        //                        var newdeploy = new DeployType();
        //                        newdeploy.DeployName = "Identity Small";
        //                        newdeploy.DeploySaved = "No";
        //                        newdeploy.TennantID = deployType.TennantID;
        //                        newdeploy.AzureDeployName = "identitysmall" + guid;
        //                        _context.Add(newdeploy);
        //                        await _context.SaveChangesAsync();
        //                    }
        //                    else
        //                    {
        //                        if (deployType.DeployName == "Identity Medium")
        //                        {
        //                            Guid guid = Guid.NewGuid();
        //                            var newdeploy = new DeployType();
        //                            newdeploy.DeployName = "Identity Medium";
        //                            newdeploy.DeploySaved = "No";
        //                            newdeploy.TennantID = deployType.TennantID;
        //                            newdeploy.AzureDeployName = "identitymedium" + guid;
        //                            _context.Add(newdeploy);
        //                            await _context.SaveChangesAsync();

        //                        }
        //                        else
        //                        {
        //                            if (deployType.DeployName == "RDS Medium")
        //                            {
        //                                Guid guid = Guid.NewGuid();
        //                                var newdeploy = new DeployType();
        //                                newdeploy.DeployName = "RDS Medium";
        //                                newdeploy.DeploySaved = "No";
        //                                newdeploy.TennantID = deployType.TennantID;
        //                                newdeploy.AzureDeployName = "rdsmedium" + guid;
        //                                _context.Add(newdeploy);
        //                                await _context.SaveChangesAsync();

        //                            }
        //                            else
        //                            {
        //                                if (deployType.DeployName == "VNET")
        //                                {
        //                                    Guid guid = Guid.NewGuid();
        //                                    var newdeploy = new DeployType();
        //                                    newdeploy.DeployName = "VNET";
        //                                    newdeploy.DeploySaved = "No";
        //                                    newdeploy.TennantID = deployType.TennantID;
        //                                    newdeploy.AzureDeployName = "VNET" + guid;
        //                                    _context.Add(newdeploy);
        //                                    await _context.SaveChangesAsync();
        //                                }
        //                                else
        //                                {
        //                                    if (deployType.DeployName == "File Server (2 node)")
        //                                    {
        //                                        Guid guid = Guid.NewGuid();
        //                                        var newdeploy = new DeployType();
        //                                        newdeploy.DeployName = "File Server (2 node)";
        //                                        newdeploy.DeploySaved = "No";
        //                                        newdeploy.TennantID = deployType.TennantID;
        //                                        newdeploy.AzureDeployName = "FileServerMed" + guid;
        //                                        _context.Add(newdeploy);
        //                                        await _context.SaveChangesAsync();

        //                                    }
        //                                    else
        //                                    {
        //                                        if (deployType.DeployName == "Virtual Machine (Domain, Data Disk)")
        //                                        {
        //                                            Guid guid = Guid.NewGuid();
        //                                            var newdeploy = new DeployType();
        //                                            newdeploy.DeployName = "VM (Domain, Data Disk)";
        //                                            newdeploy.DeploySaved = "No";
        //                                            newdeploy.TennantID = deployType.TennantID;
        //                                            newdeploy.AzureDeployName = "VMDomainDisk" + guid;
        //                                            _context.Add(newdeploy);
        //                                            await _context.SaveChangesAsync();
        //                                        }
        //                                        else
        //                                        {
        //                                            if (deployType.DeployName == "Virtual Machine (No Domain, Data Disk)")
        //                                            {
        //                                                Guid guid = Guid.NewGuid();
        //                                                var newdeploy = new DeployType();
        //                                                newdeploy.DeployName = "VM (No Domain, Data Disk)";
        //                                                newdeploy.DeploySaved = "No";
        //                                                newdeploy.TennantID = deployType.TennantID;
        //                                                newdeploy.AzureDeployName = "VMNoDomainDisk" + guid;
        //                                                _context.Add(newdeploy);
        //                                                await _context.SaveChangesAsync();
        //                                            }
        //                                            else
        //                                            {
        //                                                if (deployType.DeployName == "Virtual Machine (No Domain, No Data Disk)")
        //                                                {
        //                                                    Guid guid = Guid.NewGuid();
        //                                                    var newdeploy = new DeployType();
        //                                                    newdeploy.DeployName = "VM (No Domain, No Data Disk)";
        //                                                    newdeploy.DeploySaved = "No";
        //                                                    newdeploy.TennantID = deployType.TennantID;
        //                                                    newdeploy.AzureDeployName = "VMNoDomainNoDisk" + guid;
        //                                                    _context.Add(newdeploy);
        //                                                    await _context.SaveChangesAsync();
        //                                                }
        //                                                else
        //                                                {
        //                                                    if (deployType.DeployName == "VPN")
        //                                                    {
        //                                                        Guid guid = Guid.NewGuid();
        //                                                        var newdeploy = new DeployType();
        //                                                        newdeploy.DeployName = "VPN";
        //                                                        newdeploy.DeploySaved = "No";
        //                                                        newdeploy.TennantID = deployType.TennantID;
        //                                                        newdeploy.AzureDeployName = "VPN" + guid;
        //                                                        _context.Add(newdeploy);
        //                                                        await _context.SaveChangesAsync();
        //                                                    }
        //                                                    else
        //                                                    {
        //                                                        if (deployType.DeployName == "Identity Small Extended")
        //                                                        {
        //                                                            Guid guid = Guid.NewGuid();
        //                                                            var newdeploy = new DeployType();
        //                                                            newdeploy.DeployName = "Identity Small Extended";
        //                                                            newdeploy.DeploySaved = "No";
        //                                                            newdeploy.TennantID = deployType.TennantID;
        //                                                            newdeploy.AzureDeployName = "IdentitySmallExtended" + guid;
        //                                                            _context.Add(newdeploy);
        //                                                            await _context.SaveChangesAsync();
        //                                                        }
        //                                                        else
        //                                                        {
        //                                                            if (deployType.DeployName == "Identity Medium Extended")
        //                                                            {
        //                                                                Guid guid = Guid.NewGuid();
        //                                                                var newdeploy = new DeployType();
        //                                                                newdeploy.DeployName = "Identity Medium Extended";
        //                                                                newdeploy.DeploySaved = "No";
        //                                                                newdeploy.TennantID = deployType.TennantID;
        //                                                                newdeploy.AzureDeployName = "IdentityMediumExtended" + guid;
        //                                                                _context.Add(newdeploy);
        //                                                                await _context.SaveChangesAsync();
        //                                                            }
        //                                                        }
        //                                                    }
        //                                                }
        //                                            }
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //            return RedirectToAction("IndexSelected", new { id = deployType.TennantID });
        //    }
        //    ViewData["TennantID"] = new SelectList(_context.Tennants, "TennantID", "TennantID", deployType.TennantID);
        //    return View(deployType);
        //}

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
