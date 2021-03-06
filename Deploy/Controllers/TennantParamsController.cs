using Deploy.DAL;
using Deploy.Service;
using Deploy.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;

namespace Deploy.Controllers
{


    [Authorize(Policy = "Admins")]
    public class TennantParamsController : Controller
    {


        private readonly DeployDBContext _context;
        private AzureStorageConfig _storageConfig;
        private readonly TenantParameters _service;
        private IHostingEnvironment hostingEnv;
        public TennantParamsController(DeployDBContext context, IOptions<AzureStorageConfig> config, IHostingEnvironment env)
        {
            _context = context;
            _storageConfig = config.Value;
            _service = new TenantParameters(_context, config);
            this.hostingEnv = env;
        }


        // GET: TennantParams
        public async Task<IActionResult> IndexSelected(int Id, string sortOrder, string searchString, bool DeployExists, bool TemplateInvalid, string TemplateError)
        {

            var TennantParams = _context.TennantParams.Where(t => t.DeployTypeID == Id).FirstOrDefault();
            var DeployTypes = _context.DeployTypes.Include(d => d.Tennants).Where(d => d.DeployTypeID == Id).FirstOrDefault();
            ViewBag.DeployExists = DeployExists;
            ViewBag.TemplateInvalid = TemplateInvalid;
            ViewBag.TemplateError = TemplateError;

            if (TennantParams != null)
            {
                //var service = new TenantParameters(_context, _storageConfig);
                var TenantParams = await _service.GetTenantParams(Id);
               
                var viewModel = new TennantDeployParams();
                viewModel.DeploySaved = DeployTypes.DeploySaved;

                viewModel.DeployTypeID = TennantParams.DeployTypeID;
                viewModel.TennantName = DeployTypes.Tennants.TennantName;
                viewModel.TennantID = DeployTypes.Tennants.TennantID;


                var Params = TenantParams.DeployParams.ToList();
                var tennantParams = TenantParams.TennantParams.ToList();
                viewModel.DeployParams = new List<DeployParam>();
                
                foreach (var Param in Params)
                {
                    viewModel.DeployParams.Add(new DeployParam()
                    {
                        DeployParamID = Param.DeployParamID,
                        ParameterName = Param.ParameterName,
                        ParameterDeployType = Param.ParameterDeployType,
                    });
                }
                viewModel.TennantParams = new List<TennantParam>();
                foreach (var tennant in tennantParams)
                {
                    if (tennant.ParamType == "password")
                    {
                        viewModel.TennantParams.Add(new TennantParam()
                        {

                            ParamValue = "*****"
                        });
                    }
                    else
                    {
                        viewModel.TennantParams.Add(new TennantParam()
                        {

                            ParamValue = tennant.ParamValue
                        });
                    }
                }

            return View(viewModel);

            }
            else
            {
                return RedirectToAction("Create", new { id = Id });
            }
        }

        // GET: TennantParams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tennantParam = await _context.TennantParams
                .SingleOrDefaultAsync(m => m.TennantParamID == id);
            if (tennantParam == null)
            {
                return NotFound();
            }

            return View(tennantParam);
        }

        // GET: Deploy/Create
        public async Task<IActionResult> Create(int Id)
        {
            var deploy = _context.DeployTypes.Include(d => d.Tennants).Where(d => d.DeployTypeID == Id).FirstOrDefault();
            var viewModel = new TennantDeployParams();
            
            viewModel.DeployTypeID = Id;
            viewModel.DeployName = deploy.DeployName;
            viewModel.DeploySaved = deploy.DeploySaved;

            var TenantParams = await _service.CreateTenantParams(Id);
            var parameters = TenantParams.DeployParams.ToList();

            viewModel.DeployName = TenantParams.DeployName;
            viewModel.DeployParams = new List<DeployParam>();
            viewModel.TennantName = deploy.Tennants.TennantName;
            viewModel.TennantID = deploy.Tennants.TennantID;
    

            viewModel.DeployParamID = new List<int>();

            foreach (var param in parameters)
            {
                //viewModel.DeployParamID.Add(param.DeployParamID);

                viewModel.DeployParams.Add(new DeployParam()
                {
                    DeployParamID = param.DeployParamID,
                    ParameterName = param.ParameterName,
                    ParameterType = param.ParameterType,
                    ParameterDeployType = param.ParameterDeployType,
                    ParamToolTip = param.ParamToolTip,
                    DefaultValue = param.DefaultValue,
                    
                }) ;
            }

            return View(viewModel);
        }

        // POST: TennantParams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TennantDeployParams tennantDeployParams)
        {

            var encrypt = new RESTApi(_storageConfig, hostingEnv);
            string []Keys = await encrypt.EncryptionKeys();
            var encryption = new Encryption(Keys[1], Keys[0], 1, Keys[2], 256);

            if (ModelState.IsValid)
            {
                for (var i = 0; i < tennantDeployParams.DeployParamID.Count(); i++)
                {
                    var tennantparam = new TennantParam();
                    if (tennantDeployParams.ParamType[i] == "password")
                    {
                        var encrypted = encryption.EncryptString(tennantDeployParams.ParamValue[i]);
                        tennantparam.ParamValue = encrypted;
                    }
                    else
                    {
                        tennantparam.ParamValue = tennantDeployParams.ParamValue[i];
                    }
                    tennantparam.DeployParamID = tennantDeployParams.DeployParamID[i];
                    tennantparam.ParamName = tennantDeployParams.ParamName[i];
                    tennantparam.ParamType = tennantDeployParams.ParamType[i];
                    tennantparam.DeployTypeID = tennantDeployParams.DeployTypeID;
                    tennantparam.ParamToolTip = tennantDeployParams.ParamToolTip[i];

                    if (TryValidateModel(tennantparam))
                    {
                        _context.Add(tennantparam);
                        await _context.SaveChangesAsync();
                    }
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction("Index", "DeployTypes", new { id = tennantDeployParams.TennantID });
            }
            return RedirectToAction("Index", "DeployTypes", new { id = tennantDeployParams.TennantID });
        }

        // GET: TennantParams/Edit/5
        public async Task<IActionResult> Edit(int Id)
        {
            var encrypt = new RESTApi(_storageConfig, hostingEnv);
            string[] Keys = await encrypt.EncryptionKeys();
            var encryption = new Encryption(Keys[1], Keys[0], 1, Keys[2], 256);

            var deploy = _context.DeployTypes.Include(d => d.Tennants).Where(d => d.DeployTypeID == Id).FirstOrDefault();
            var viewModel = new TennantDeployParams();

            viewModel.DeployTypeID = Id;
            viewModel.DeployName = deploy.DeployName;
            viewModel.DeploySaved = deploy.DeploySaved;

            var TenantParams = await _service.GetTenantParams(Id);
            var parameters = TenantParams.TennantParams.ToList();
            viewModel.DeployName = TenantParams.DeployName;
            viewModel.DeployParams = new List<DeployParam>();
            viewModel.TennantName = deploy.Tennants.TennantName;
            viewModel.TennantID = deploy.Tennants.TennantID;

            viewModel.DeployParamID = new List<int>();
            viewModel.ParamName = new List<string>();
            viewModel.ParamValue = new List<string>();
            viewModel.ParamType = new List<string>();
            viewModel.ParamToolTip = new List<string>();
            viewModel.TennantParamID = new List<int>();
            viewModel.DeployParamID = new List<int>();

            foreach (var param in parameters)
            {
                viewModel.TennantParamID.Add(param.TennantParamID);
                viewModel.DeployParamID.Add(param.DeployParamID);
                viewModel.ParamName.Add(param.ParamName);
                if (param.ParamType == "password")
                {
                    var decrypted = encryption.DecryptString(param.ParamValue);
                    viewModel.ParamValue.Add(param.ParamValue = decrypted);
                }
                else
                {
                    viewModel.ParamValue.Add(param.ParamValue);
                }
                viewModel.ParamType.Add(param.ParamType);
                viewModel.ParamToolTip.Add(param.ParamToolTip);
            }
            return View(viewModel);

        }

    

        // POST: TennantParams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TennantDeployParams tennantDeployParams)
        {
            var encrypt = new RESTApi(_storageConfig, hostingEnv);
            string[] Keys = await encrypt.EncryptionKeys();
            var encryption = new Encryption(Keys[1], Keys[0], 1, Keys[2], 256);

            if (ModelState.IsValid)
            {
                for (var i = 0; i < tennantDeployParams.ParamName.Count(); i++)
                {

                    var tennantparam = new TennantParam();
                    tennantparam.TennantParamID = tennantDeployParams.TennantParamID[i];
                    tennantparam.ParamName = tennantDeployParams.ParamName[i];
                    if (tennantDeployParams.ParamType[i] == "password")
                    {
                        var encrypted = encryption.EncryptString(tennantDeployParams.ParamValue[i]);
                        tennantparam.ParamValue = encrypted;
                    }
                    else
                    {
                        tennantparam.ParamValue = tennantDeployParams.ParamValue[i];
                    }
                    tennantparam.ParamToolTip = tennantDeployParams.ParamToolTip[i];
                    tennantparam.DeployTypeID = tennantDeployParams.DeployTypeID;
                    tennantparam.ParamType = tennantDeployParams.ParamType[i];
                    tennantparam.DeployParamID = tennantDeployParams.DeployParamID[i];
                    

                    var deployTypes = _context.DeployTypes.Where(d => d.DeployTypeID == tennantDeployParams.DeployTypeID).FirstOrDefault();
                    deployTypes.DeploySaved = "No";
                    deployTypes.DeployState = "";
                    deployTypes.DeployResult = "";

                    _context.Update(deployTypes);
                    await _context.SaveChangesAsync();
                    try
                    {
                        _context.Update(tennantparam);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TennantParamExists(tennantparam.TennantParamID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    
                }
                return RedirectToAction("Index", "DeployTypes", new { id = tennantDeployParams.TennantID });
            }
            return RedirectToAction("Index", "DeployTypes", new { id = tennantDeployParams.TennantID });
        }

        // POST: Deploy/Delete/5
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

        // POST: TennantParams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tennantParam = await _context.TennantParams.SingleOrDefaultAsync(m => m.TennantParamID == id);
            _context.TennantParams.Remove(tennantParam);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeployToAzure(int Id, bool Force)
        {
            var deployTypes = _context.DeployTypes.Include(d => d.Tennants).Where(d => d.DeployTypeID == Id).FirstOrDefault();
            //var service = new TenantParameters(_context, _storageConfig);
            string[]results = await _service.DeployToAzure(Id, Force);
            if (results[0] == "DeployExists")
            {
                ViewBag.DeployExists = true;
                return RedirectToAction("IndexSelected", "TennantParams", new { id = deployTypes.DeployTypeID, DeployExists = true });
            }
            else if (results[1] == "TemplateInvalid")
            {
                ViewBag.TemplateInvalid = results[1];
                ViewBag.TemplateError = results[2];
                return RedirectToAction("IndexSelected", "TennantParams", new { id = deployTypes.DeployTypeID, TemplateInvalid = true, TemplateError = results[2] });
            }
            else
            {
                return RedirectToAction("Index", "DeployTypes", new { id = deployTypes.TennantID });
            }
        }

        public async Task<IActionResult> QueueDeployment(int Id, bool Force)
        {
            var deployTypes = _context.DeployTypes.Include(d => d.Tennants).Where(d => d.DeployTypeID == Id).FirstOrDefault();
            //var service = new TenantParameters(_context, _storageConfig);
            string[] results = await _service.QueueDeployment(Id, Force);
            if (results[0] == "DeployExists")
            {
                ViewBag.DeployExists = true;
                return RedirectToAction("IndexSelected", "TennantParams", new { id = deployTypes.DeployTypeID, DeployExists = true });
            }
            else if (results[1] == "TemplateInvalid")
            {
                ViewBag.TemplateInvalid = results[1];
                ViewBag.TemplateError = results[2];
                return RedirectToAction("IndexSelected", "TennantParams", new { id = deployTypes.DeployTypeID, TemplateInvalid = true, TemplateError = results[2] });
            }
            else
            {
                return RedirectToAction("Index", "DeployTypes", new { id = deployTypes.TennantID });
            }
        }

        public async Task<IActionResult> GetDeploy(int Id)
        {
            var deployTypes = _context.DeployTypes.Include(d => d.Tennants).Where(d => d.DeployTypeID == Id).FirstOrDefault();
            await _service.GetDeploy(Id);

            return RedirectToAction("Index", "DeployTypes", new { id = deployTypes.TennantID });
        }


        public async Task<IActionResult> GetDeployAll(int Id)
        {
            var deployTypes = await _context.DeployTypes.Include(d => d.Tennants).Where(d => d.TennantID == Id).Where(d => d.DeployState != null).ToListAsync();
            await _service.GetDeployAll(Id);

            return RedirectToAction("Index", "DeployTypes", new { id = deployTypes.FirstOrDefault().TennantID });
        }

        public async Task<IActionResult> SaveToAzure(int Id)
        {
            var deployTypes = _context.DeployTypes.Include(d => d.Tennants).Where(d => d.DeployTypeID == Id).FirstOrDefault();
            //var tennantParams = await _context.TennantParams.Where(t => t.DeployTypeID == Id).ToListAsync();
            await _service.SaveToAzure(Id);

            return RedirectToAction("Index", "DeployTypes", new { id = deployTypes.TennantID });
        }

        public static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        private bool TennantParamExists(int id)
        {
            return _context.TennantParams.Any(e => e.TennantParamID == id);
        }

        public async Task<IActionResult> GetConAccessPolicy(int Id)
        {
            var deployTypes = _context.DeployTypes.Include(d => d.Tennants).Where(d => d.DeployTypeID == Id).FirstOrDefault();

            var tokenResults = RESTApi.GraphToken(deployTypes.Tennants.AzureTennantID, deployTypes.Tennants.AzureClientID, deployTypes.Tennants.AzureClientSecret);
            RESTApi.AccessToken AccessToken = JsonConvert.DeserializeObject<RESTApi.AccessToken>(tokenResults.Result);
            string accesstoken = AccessToken.access_token;
            string uri = "https://graph.microsoft.com/beta/conditionalAccess/Policies";
            var result = await RESTApi.GraphGetAction(accesstoken, uri);

            JObject o = JObject.Parse(result);

            deployTypes.DeploySaved = "N/A";
            deployTypes.DeployResult = result;
            deployTypes.DeployState = "N/A";
            deployTypes.DeployState = "Deployed";
            _context.Update(deployTypes);
            await _context.SaveChangesAsync();
            
            return RedirectToAction("Index", "DeployTypes", new { Id = deployTypes.TennantID });
        }

        public async Task<IActionResult> CreateConAccessPolicy(int Id)
        {
            var deployTypes = _context.DeployTypes.Include(d => d.Tennants).Where(d => d.DeployTypeID == Id).FirstOrDefault();
            var policyPath = _context.TennantParams.Where(t => t.ParamName == "PolicyPath").FirstOrDefault();
            string path = policyPath.ParamValue;

            var tokenResults = RESTApi.GraphToken(deployTypes.Tennants.AzureTennantID, deployTypes.Tennants.AzureClientID, deployTypes.Tennants.AzureClientSecret);
            RESTApi.AccessToken AccessToken = JsonConvert.DeserializeObject<RESTApi.AccessToken>(tokenResults.Result);
            string accesstoken = AccessToken.access_token;

            string uri = "https://graph.microsoft.com/beta/conditionalAccess/Policies";
            var restAPI = new RESTApi(_storageConfig, hostingEnv);

            var result = await restAPI.GraphPostAction(accesstoken, uri, path);
            string filename = path;
            filename = hostingEnv.WebRootPath + $@"\csv\{filename}";
            

            if (!System.IO.File.Exists(filename))
            {
                System.IO.File.WriteAllText(filename, result);
            }

            deployTypes.DeploySaved = "N/A";
            deployTypes.DeployResult = result;
            deployTypes.DeployState = "N/A";
            deployTypes.DeployState = "Deployed";
            _context.Update(deployTypes);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "DeployTypes", new { Id = deployTypes.TennantID });


        }

        public FileResult Download(int deployTypeID)
        {
            var deployTypes = _context.DeployTypes.Include(d => d.TennantParams).Where(d => d.DeployTypeID == deployTypeID).FirstOrDefault();
            string file = "tempresults.txt";
            string filename = hostingEnv.WebRootPath + $@"\csv\{file}";

            var result = deployTypes.DeployResult;
            
            if (!System.IO.File.Exists(filename))
            {
                System.IO.File.WriteAllText(filename, result);
            }

            byte[] filebytes = System.IO.File.ReadAllBytes(filename);
            return File(filebytes, "application/x-msdownload", file);
        }
    }
}
