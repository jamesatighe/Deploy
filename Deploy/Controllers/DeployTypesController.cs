using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Deploy.DAL;
using Deploy.Service;
using Deploy.ViewModel;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Extensions.Options;
using System.Text;

namespace Deploy.Controllers
{
    [Authorize(Policy = "Admins")]
    public class DeployTypesController : Controller
    {
        private readonly DeployDBContext _context;
        private AzureStorageConfig _storageConfig;
        private readonly DeployTypesService _service;
        private readonly TenantParameters _tenantService;

        public DeployTypesController(DeployDBContext context, IOptions<AzureStorageConfig> config)
        {
            _storageConfig = config.Value;
            _context = context;
            _service = new DeployTypesService(_context);
            _tenantService = new TenantParameters(_context, config);
        }


        public IActionResult TempManagement()
        {
            return View();
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
                        Description = deployType.Description,
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
            var deploylist = _context.DeployList.ToList();
            var viewModel = new DeployChoiceViewModel();
            viewModel.DeployList = new List<DeployList>();
            viewModel.DeployListItem = new List<SelectListItem>();
            viewModel.TennantID = Id;
            viewModel.TennantName = tennant.TennantName;
            viewModel.DeploySaved = "No";
            foreach (var group in deploylist.GroupBy(item => item.DeployType))
            {
                SelectListGroup listGroup = new SelectListGroup() { Name = group.Key };

                foreach (var item in group)
                {
                    viewModel.DeployListItem.Add(new SelectListItem
                    {

                        Text = item.DeployValue,
                        Value = item.DeployName,
                        Group = listGroup
                    });

                };
            }
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

                Guid guid = Guid.NewGuid();
                var newdeploy = new DeployType();
                newdeploy.DeployName = DeployChoices.DeployName;
                newdeploy.Description = DeployChoices.Description;
                newdeploy.DeploySaved = "No";
                newdeploy.TennantID = DeployChoices.TennantID;
                newdeploy.DeployFile = DeployChoices.DeployFile;
                newdeploy.ParamsFile = DeployChoices.ParamsFile;
                newdeploy.DeployCode = DeployChoices.DeployCode;
                newdeploy.ResourceGroupName = DeployChoices.ResourceGroupName;
                newdeploy.AzureDeployName = DeployChoices.DeployCode + guid;
                _context.Add(newdeploy);
                await _context.SaveChangesAsync();
  

                return RedirectToAction("IndexSelected", new { id = DeployChoices.TennantID });
            }
            return View(choices);
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

        [HttpPost]
        public HttpStatusCode Delete(int id)
        {
            var deployType = _context.DeployTypes.FirstOrDefault(m => m.DeployTypeID == id);
            if (deployType == null)
            {
                return HttpStatusCode.NotFound;
            }

            _context.DeployTypes.Remove(deployType);
            _context.SaveChanges();
            return HttpStatusCode.OK;
        }

        private bool DeployTypeExists(int id)
        {
            return _context.DeployTypes.Any(e => e.DeployTypeID == id);
        }

        //#########################################
        //##        Json to SelectList           ##
        //#########################################

        [HttpPost]
        public JsonResult GetDeployJson(int Id, string type, string publisher = null, string offering = null, string resourceGroup = null, string VNET = null, string IP = null)
        {
            if (type == "VM" || type == "resourceGroup")
            {
                var getContent = _tenantService.GetDeployJson(Id, type);
                JObject json = JsonConvert.DeserializeObject<JObject>(getContent);

                return Json(json);
            }
            if (type == "VNET")
            {
                var getContent = _tenantService.GetDeployJson(Id, type, null, null, resourceGroup);
                JObject json = JsonConvert.DeserializeObject<JObject>(getContent);

                return Json(json);
            }
            if (type == "subnet")
            {
                var getContent = _tenantService.GetDeployJson(Id, type, null, null, resourceGroup, VNET);
                JObject json = JsonConvert.DeserializeObject<JObject>(getContent);

                return Json(json);
            }
            if (type == "IPCheck")
            {
                var getContent = _tenantService.GetDeployJson(Id, type, null, null, resourceGroup, VNET, IP);
                JObject json = JsonConvert.DeserializeObject<JObject>(getContent);

                return Json(json);
            }
            else
            {
                var getContent = _tenantService.GetDeployJson(Id, type, publisher, offering, null, VNET);
                JArray array = JArray.Parse(getContent);
                JObject json = new JObject();
                json.Add("value", array);
                
                //new JProperty("value", array));
               
                return Json(json);
            }
        }


    }
}   
