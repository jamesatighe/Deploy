using Deploy.DAL;
using Deploy.Service;
using Deploy.Models;
using Deploy.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.Controllers
{
    //[Authorize(Policy = "Admins")]
    public class TennantParamsController : Controller
    {
        private readonly DeployDBContext _context;
        private AzureStorageConfig _storageConfig;

        public TennantParamsController(DeployDBContext context, IOptions<AzureStorageConfig> config)
        {
            _context = context;
            _storageConfig = config.Value;
        }


        // GET: TennantParams
        public async Task<IActionResult> IndexSelected(int Id)
        {
           
            var TennantParams = _context.TennantParams.Where(t => t.DeployTypeID == Id).FirstOrDefault();
            var DeployTypes = _context.DeployTypes.Include(d => d.Tennants).Where(d => d.DeployTypeID == Id).FirstOrDefault();
            if (TennantParams != null)
            {
                var service = new Deploy.Service.TenantParameters(_context);
                var TenantParams = await service.GetTenantParams(Id);
                var viewModel = new TennantDeployParams();

                viewModel.DeployName = DeployTypes.DeployName;
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
                        ParameterDeployType = Param.ParameterDeployType
                    });
                }
                viewModel.TennantParams = new List<TennantParam>();
                foreach (var tennant in tennantParams)
                {
                    viewModel.TennantParams.Add(new TennantParam()
                    {
                        ParamValue = tennant.ParamValue
                    });
                }
                //var viewModel = new TennantDeployParams();
                //viewModel.DeployName = DeployTypes.DeployName;
                //viewModel.DeploySaved = DeployTypes.DeploySaved;

                //if (viewModel.DeployName == "Identity Small" || viewModel.DeployName.Contains("(IDS)"))
                //{
                //    viewModel.DeployTypeID = TennantParams.DeployTypeID;
                //    viewModel.TennantName = DeployTypes.Tennants.TennantName;
                //    viewModel.TennantID = DeployTypes.Tennants.TennantID;

                //    var Params = await _context.DeployParms.Where(d => d.ParameterDeployType == "ADS").ToListAsync();
                //    var tennantParams = await _context.TennantParams.Where(t => t.DeployTypeID == Id).ToListAsync();
                //    viewModel.DeployParams = new List<DeployParam>();
                //    foreach (var Param in Params)
                //    {
                //        viewModel.DeployParams.Add(new DeployParam()
                //        {
                //            DeployParamID = Param.DeployParamID,
                //            ParameterName = Param.ParameterName,
                //            ParameterDeployType = Param.ParameterDeployType
                //        });
                //    }
                //    viewModel.TennantParams = new List<TennantParam>();
                //    foreach (var tennant in tennantParams)
                //    {
                //        viewModel.TennantParams.Add(new TennantParam()
                //        {
                //            ParamValue = tennant.ParamValue
                //        });
                //    }

                //}

                //if (viewModel.DeployName == "Identity Medium" || viewModel.DeployName.Contains("(IDM)"))
                //{
                //    viewModel.DeployTypeID = TennantParams.DeployTypeID;
                //    viewModel.TennantName = DeployTypes.Tennants.TennantName;
                //    viewModel.TennantID = DeployTypes.Tennants.TennantID;

                //    var Params = await _context.DeployParms.Where(d => d.ParameterDeployType == "ADM").ToListAsync();
                //    var tennantParams = await _context.TennantParams.Where(t => t.DeployTypeID == Id).ToListAsync();
                //    viewModel.DeployParams = new List<DeployParam>();
                //    foreach (var Param in Params)
                //    {
                //        viewModel.DeployParams.Add(new DeployParam()
                //        {
                //            DeployParamID = Param.DeployParamID,
                //            ParameterName = Param.ParameterName,
                //            ParameterDeployType = Param.ParameterDeployType
                //        });
                //    }
                //    viewModel.TennantParams = new List<TennantParam>();
                //    foreach (var tennant in tennantParams)
                //    {
                //        viewModel.TennantParams.Add(new TennantParam()
                //        {
                //            ParamValue = tennant.ParamValue
                //        });
                //    }

                //}

                //if (viewModel.DeployName == "RDS Small" || viewModel.DeployName.Contains("(RDSS)"))
                //{
                //    viewModel.DeployTypeID = TennantParams.DeployTypeID;
                //    viewModel.TennantName = DeployTypes.Tennants.TennantName;
                //                        viewModel.TennantID = DeployTypes.Tennants.TennantID;

                //    var Params = await _context.DeployParms.Where(d => d.ParameterDeployType == "RDSS").ToListAsync();
                //    var tennantParams = await _context.TennantParams.Where(t => t.DeployTypeID == Id).ToListAsync();
                //    viewModel.DeployParams = new List<DeployParam>();
                //    foreach (var Param in Params)
                //    {
                //        viewModel.DeployParams.Add(new DeployParam()
                //        {
                //            DeployParamID = Param.DeployParamID,
                //            ParameterName = Param.ParameterName,
                //            ParameterDeployType = Param.ParameterDeployType
                //        });
                //    }
                //    viewModel.TennantParams = new List<TennantParam>();
                //    foreach (var tennant in tennantParams)
                //    {
                //        viewModel.TennantParams.Add(new TennantParam()
                //        {
                //            ParamValue = tennant.ParamValue
                //        });
                //    }

                //}

                //if (viewModel.DeployName == "RDS Medium" || viewModel.DeployName.Contains("(RDSM)"))
                //{
                //    viewModel.DeployTypeID = TennantParams.DeployTypeID;
                //    viewModel.TennantName = DeployTypes.Tennants.TennantName;
                //    viewModel.TennantID = DeployTypes.Tennants.TennantID;

                //    var Params = await _context.DeployParms.Where(d => d.ParameterDeployType == "RDSM").ToListAsync();
                //    var tennantParams = await _context.TennantParams.Where(t => t.DeployTypeID == Id).ToListAsync();
                //    viewModel.DeployParams = new List<DeployParam>();
                //    foreach (var Param in Params)
                //    {
                //        viewModel.DeployParams.Add(new DeployParam()
                //        {
                //            DeployParamID = Param.DeployParamID,
                //            ParameterName = Param.ParameterName,
                //            ParameterDeployType = Param.ParameterDeployType
                //        });
                //    }
                //    viewModel.TennantParams = new List<TennantParam>();
                //    foreach (var tennant in tennantParams)
                //    {
                //        viewModel.TennantParams.Add(new TennantParam()
                //        {
                //            ParamValue = tennant.ParamValue
                //        });
                //    }

                //}

                //if (viewModel.DeployName.Contains("VNET"))
                //{
                //    viewModel.DeployTypeID = TennantParams.DeployTypeID;
                //    viewModel.TennantName = DeployTypes.Tennants.TennantName;
                //    viewModel.TennantID = DeployTypes.Tennants.TennantID;

                //    var Params = await _context.DeployParms.Where(d => d.ParameterDeployType == "VNET").ToListAsync();
                //    var tennantParams = await _context.TennantParams.Where(t => t.DeployTypeID == Id).ToListAsync();
                //    viewModel.DeployParams = new List<DeployParam>();
                //    foreach (var Param in Params)
                //    {
                //        viewModel.DeployParams.Add(new DeployParam()
                //        {
                //            DeployParamID = Param.DeployParamID,
                //            ParameterName = Param.ParameterName,
                //            ParameterDeployType = Param.ParameterDeployType
                //        });
                //    }
                //    viewModel.TennantParams = new List<TennantParam>();
                //    foreach (var tennant in tennantParams)
                //    {
                //        viewModel.TennantParams.Add(new TennantParam()
                //        {
                //            ParamValue = tennant.ParamValue
                //        });
                //    }

                //}


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

            var service = new Deploy.Service.TenantParameters(_context);
            var TenantParams = await service.CreateTenantParams(Id);
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
                    ParamToolTip = param.ParamToolTip
                });
            }

            //if (viewModel.DeployName.Contains("(IDS)") || viewModel.DeployName == "Identity Small")
            //{
            //    var parameters = await _context.DeployParms.Where(d => d.ParameterDeployType == "ADS").ToListAsync();
            //    viewModel.DeployParams = new List<DeployParam>();
            //    viewModel.TennantName = deploy.Tennants.TennantName;
            //    viewModel.TennantID = deploy.Tennants.TennantID;

            //    //viewModel.DeployParamID = new List<int>();

            //    foreach (var param in parameters)
            //    {
            //        //viewModel.DeployParamID.Add(param.DeployParamID);

            //        viewModel.DeployParams.Add(new DeployParam()
            //        {
            //            DeployParamID = param.DeployParamID,
            //            ParameterName = param.ParameterName,
            //            ParameterType = param.ParameterType,
            //            ParameterDeployType = param.ParameterDeployType,
            //            ParamToolTip = param.ParamToolTip
            //        });
            //    }
            //}

            //if (viewModel.DeployName.Contains("(IDM)") || viewModel.DeployName == "Identity Medium")
            //{
            //    var parameters = await _context.DeployParms.Where(d => d.ParameterDeployType == "ADM").ToListAsync();
            //    viewModel.DeployParams = new List<DeployParam>();
            //    viewModel.TennantName = deploy.Tennants.TennantName;
            //    viewModel.TennantID = deploy.Tennants.TennantID;

            //    //viewModel.DeployParamID = new List<int>();

            //    foreach (var param in parameters)
            //    {
            //        //viewModel.DeployParamID.Add(param.DeployParamID);

            //        viewModel.DeployParams.Add(new DeployParam()
            //        {
            //            DeployParamID = param.DeployParamID,
            //            ParameterName = param.ParameterName,
            //            ParameterType = param.ParameterType,
            //            ParameterDeployType = param.ParameterDeployType,
            //            ParamToolTip = param.ParamToolTip
            //        });
            //    }
            //}

            //if (viewModel.DeployName.Contains("(RDSS)") || viewModel.DeployName == "RDS Small")
            //{
            //    var parameters = await _context.DeployParms.Where(d => d.ParameterDeployType == "RDSS").ToListAsync();
            //    viewModel.DeployParams = new List<DeployParam>();
            //    viewModel.TennantName = deploy.Tennants.TennantName;
            //    viewModel.TennantID = deploy.Tennants.TennantID;
            //    //viewModel.DeployParamID = new List<int>();

            //    foreach (var param in parameters)
            //    {
            //        //viewModel.DeployParamID.Add(param.DeployParamID);

            //        viewModel.DeployParams.Add(new DeployParam()
            //        {
            //            DeployParamID = param.DeployParamID,
            //            ParameterName = param.ParameterName,
            //            ParameterType = param.ParameterType,
            //            ParameterDeployType = param.ParameterDeployType,
            //            ParamToolTip = param.ParamToolTip
            //        });
            //    }
            //}

            //if (viewModel.DeployName.Contains("(RDSM)") || viewModel.DeployName == "RDS Medium")
            //{
            //    var parameters = await _context.DeployParms.Where(d => d.ParameterDeployType == "RDSM").ToListAsync();
            //    viewModel.DeployParams = new List<DeployParam>();
            //    viewModel.TennantName = deploy.Tennants.TennantName;
            //    viewModel.TennantID = deploy.Tennants.TennantID;
            //    //viewModel.DeployParamID = new List<int>();

            //    foreach (var param in parameters)
            //    {
            //        //viewModel.DeployParamID.Add(param.DeployParamID);

            //        viewModel.DeployParams.Add(new DeployParam()
            //        {
            //            DeployParamID = param.DeployParamID,
            //            ParameterName = param.ParameterName,
            //            ParameterType = param.ParameterType,
            //            ParameterDeployType = param.ParameterDeployType,
            //            ParamToolTip = param.ParamToolTip
            //        });
            //    }
            //}


            //if (viewModel.DeployName.Contains("VNET"))
            //{
            //    var parameters = await _context.DeployParms.Where(d => d.ParameterDeployType == "VNET").ToListAsync();
            //    viewModel.DeployParams = new List<DeployParam>();
            //    viewModel.TennantName = deploy.Tennants.TennantName;
            //    viewModel.TennantID = deploy.Tennants.TennantID;
            //    //viewModel.DeployParamID = new List<int>();

            //    foreach (var param in parameters)
            //    {
            //        //viewModel.DeployParamID.Add(param.DeployParamID);

            //        viewModel.DeployParams.Add(new DeployParam()
            //        {
            //            DeployParamID = param.DeployParamID,
            //            ParameterName = param.ParameterName,
            //            ParameterType = param.ParameterType,
            //            ParameterDeployType = param.ParameterDeployType,
            //            ParamToolTip = param.ParamToolTip
            //        });
            //    }
            //}

            return View(viewModel);
        }

        // POST: TennantParams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TennantDeployParams tennantDeployParams)
        {
            
            if (ModelState.IsValid)
            {
                for (var i = 0; i < tennantDeployParams.DeployParamID.Count(); i++)
                {
                    var tennantparam = new TennantParam();
                    tennantparam.DeployParamID = tennantDeployParams.DeployParamID[i];
                    tennantparam.ParamValue = tennantDeployParams.ParamValue[i];
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
                return RedirectToAction("IndexSelected", "DeployTypes", new { id = tennantDeployParams.TennantID });
            }
            return RedirectToAction("IndexSelected", "DeployTypes", new { id = tennantDeployParams.TennantID });
        }

        // GET: TennantParams/Edit/5
        public async Task<IActionResult> Edit(int Id)
        {
            var deploy = _context.DeployTypes.Include(d => d.Tennants).Where(d => d.DeployTypeID == Id).FirstOrDefault();
            var parameters = await _context.TennantParams.Include(t => t.DeployParams).Where(t => t.DeployTypeID == Id).ToListAsync();
            var viewModel = new TennantDeployParams();

            viewModel.DeployTypeID = Id;
            viewModel.DeployName = deploy.DeployName;
            viewModel.TennantID = deploy.TennantID;
            viewModel.TennantName = deploy.Tennants.TennantName;

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
                viewModel.ParamValue.Add(param.ParamValue);
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

            if (ModelState.IsValid)
            {
                for (var i = 0; i < tennantDeployParams.ParamName.Count(); i++)
                {
                    var tennantparam = new TennantParam();
                    tennantparam.TennantParamID = tennantDeployParams.TennantParamID[i];
                    tennantparam.ParamName = tennantDeployParams.ParamName[i];
                    tennantparam.ParamValue = tennantDeployParams.ParamValue[i];
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
                return RedirectToAction("IndexSelected", "DeployTypes", new { id = tennantDeployParams.TennantID });
            }
            return RedirectToAction("IndexSelected", "DeployTypes", new { id = tennantDeployParams.TennantID });
        }
        // GET: TennantParams/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        public async Task<IActionResult> DeployToAzure(int Id)
        {
            var deployTypes = _context.DeployTypes.Include(d => d.Tennants).Where(d => d.DeployTypeID == Id).FirstOrDefault();
            
            //Declare variables for use
            string tennantID = deployTypes.Tennants.AzureTennantID;
            string clientID = deployTypes.Tennants.AzureClientID;
            string secret = deployTypes.Tennants.AzureClientSecret;
            string subscriptionID = deployTypes.Tennants.AzureSubscriptionID;
            string resourcegroupname = deployTypes.Tennants.ResourceGroupName;
            string resourcegroup = deployTypes.Tennants.ResourceGroupName;
            string azuredeploy = string.Empty;
            string jsonResourceGroup = "{ \"location\": \"North Europe\" }";

            //Get Access Token from HTTP POST to Azure
            var results = RESTApi.PostAction(tennantID, clientID, secret);
            RESTApi.AccessToken AccessToken = JsonConvert.DeserializeObject<RESTApi.AccessToken>(results.Result);
            string accesstoken = AccessToken.access_token;
            var sasToken =  AzureHelper.GetSASToken(_storageConfig);

            //Create json for deployment to be amended.
            string jsonDeploy = "{\"properties\": { \"templateLink\": { \"uri\": \"https://cobwebjson.blob.core.windows.net/ansible/{template}{sasToken}\", \"contentVersion\": \"1.0.0.0\"}, \"mode\": \"Incremental\", \"parametersLink\": { \"uri\": \"https://cobwebjson.blob.core.windows.net/ansible/Parameters/{parameters}{sasToken}\", \"contentVersion\": \"1.0.0.0\" } } }";
            jsonDeploy = jsonDeploy.Replace("{sasToken}", sasToken);

            //Set Deploy Template dependent on Deployment Type
            //Add new Deployment Type logic here!
            if (deployTypes.DeployName == "Identity Small")
            {
                jsonDeploy = jsonDeploy.Replace("{template}", "Identity/identitysmall.json");
                jsonDeploy = jsonDeploy.Replace("{parameters}", "identitysmall-" + deployTypes.Tennants.TennantName + "-param.json");
                azuredeploy = "identitysmall";
                
            };
            if (deployTypes.DeployName == "RDS Small")
            {
                jsonDeploy = jsonDeploy.Replace("{template}", "RDS/RDSSmall.json");
                jsonDeploy = jsonDeploy.Replace("{parameters}", "rdssmall-" + deployTypes.Tennants.TennantName + "-param.json");
                azuredeploy = "rdssmall";
            };
            if (deployTypes.DeployName == "Identity Medium")
            {
                jsonDeploy = jsonDeploy.Replace("{template}", "Identity/identity.json");
                jsonDeploy = jsonDeploy.Replace("{parameters}", "identity-" + deployTypes.Tennants.TennantName + "-param.json");
                azuredeploy = "identity";
            };
            if (deployTypes.DeployName == "RDS Medium")
            {
                jsonDeploy = jsonDeploy.Replace("{template}", "RDS/RDSMedium.json");
                jsonDeploy = jsonDeploy.Replace("{parameters}", "rdsmedium-" + deployTypes.Tennants.TennantName + "-param.json");
                azuredeploy = "rdsmedium";
            };
            //end of deployment logic.

            //Create resource group.
            var putResourceGroup = RESTApi.PutAsync(subscriptionID, resourcegroup, azuredeploy, accesstoken, jsonResourceGroup, true);

            //PUT request for deployment.
            var putcontent = RESTApi.PutAsync(subscriptionID,resourcegroupname,azuredeploy,accesstoken,jsonDeploy, false);
            JObject json = JsonConvert.DeserializeObject<JObject>(putcontent.Result);

            //Update Deployment Type to show deployed
            deployTypes.DeployState = "Deployed";
            deployTypes.DeployResult = await putcontent;
            _context.Update(deployTypes);
            await _context.SaveChangesAsync();

            return RedirectToAction("IndexSelected", "DeployTypes", new { id = deployTypes.TennantID });
        }


        public async Task<IActionResult> DeployToAzureRDSSmall(int Id)
        {
            var deployTypes = await _context.DeployTypes.Include(d => d.Tennants).Where(d => d.TennantID == Id).Where(d => d.AzureDeployName == "rdssmallsolution").ToListAsync();

            //Declare variables for use
            string tennantID = deployTypes.FirstOrDefault().Tennants.AzureTennantID;
            string clientID = deployTypes[0].Tennants.AzureClientID;
            string secret = deployTypes.FirstOrDefault().Tennants.AzureClientSecret;
            string subscriptionID = deployTypes.FirstOrDefault().Tennants.AzureSubscriptionID;
            string resourcegroupname = deployTypes.FirstOrDefault().Tennants.ResourceGroupName;
            string resourcegroup = deployTypes.FirstOrDefault().Tennants.ResourceGroupName;
            string azuredeploy = string.Empty;

            var results = RESTApi.PostAction(tennantID, clientID, secret);
            RESTApi.AccessToken AccessToken = JsonConvert.DeserializeObject<RESTApi.AccessToken>(results.Result);
            string accesstoken = AccessToken.access_token;

            var sasToken = AzureHelper.GetSASToken(_storageConfig);

            string jsonResourceGroup = "{ \"location\": \"North Europe\" }";

            //string jsonDeploy = "{\"properties\": { \"templateLink\": { \"uri\": \"https://cobwebjson.blob.core.windows.net/ansible/{template}{sasToken}\", \"contentVersion\": \"1.0.0.0\"}, \"mode\": \"Incremental\", \"parametersLink\": { \"uri\": \"https://cobwebjson.blob.core.windows.net/ansible/Parameters/{parameters}{sasToken}\", \"contentVersion\": \"1.0.0.0\" } } }";
            string jsonDeploy = "{\"properties\": { \"templateLink\": { \"uri\": \"https://cobwebjson.blob.core.windows.net/ansible/{template}{sasToken}\", \"contentVersion\": \"1.0.0.0\"}, \"mode\": \"Incremental\" } }";
            jsonDeploy = jsonDeploy.Replace("{sasToken}", sasToken);

            //Set Deploy Template dependent on Deployment Type
            jsonDeploy = jsonDeploy.Replace("{template}", "Linked/rdssmallsolution-temp.json");
            jsonDeploy = jsonDeploy.Replace("{parameters}", "identitysmall-" + deployTypes.FirstOrDefault().Tennants.TennantName + "-param.json");
            azuredeploy = "rdssmallsolution";

            CloudStorageAccount storageAccount = new CloudStorageAccount(new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(_storageConfig.AccountName, _storageConfig.AccountKey), true);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("ansible/Linked");
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("rdssmallsolution.json");

            string linkedTemplate;
            using (var memoryStream = new MemoryStream())
            {
                await blockBlob.DownloadToStreamAsync(memoryStream);
                linkedTemplate = Encoding.UTF8.GetString(memoryStream.ToArray());
            }

            linkedTemplate = linkedTemplate.Replace("{templatelinkvnet}", "https://cobwebjson.blob.core.windows.net/ansible/VNet1SubnetsGW.json" + sasToken);
            linkedTemplate = linkedTemplate.Replace("{parameterlinkvnet}", "https://cobwebjson.blob.core.windows.net/ansible/Parameters/VNET-" + deployTypes.FirstOrDefault().Tennants.TennantName + "-param.json" + sasToken);
            linkedTemplate = linkedTemplate.Replace("{templatelinkid}", "https://cobwebjson.blob.core.windows.net/ansible/Identity/identitysmall.json" + sasToken);
            linkedTemplate = linkedTemplate.Replace("{parameterlinkid}", "https://cobwebjson.blob.core.windows.net/ansible/Parameters/identitysmall-" + deployTypes.FirstOrDefault().Tennants.TennantName + "-param.json" + sasToken);
            linkedTemplate = linkedTemplate.Replace("{templatelinkrds}", "https://cobwebjson.blob.core.windows.net/ansible/RDS/RDSSmallfull.json" + sasToken);
            linkedTemplate = linkedTemplate.Replace("{parameterlinkrds}", "https://cobwebjson.blob.core.windows.net/ansible/Parameters/rdssmall-" + deployTypes.FirstOrDefault().Tennants.TennantName + "-param.json" + sasToken);

            CloudBlockBlob blockBlob2 = container.GetBlockBlobReference("rdssmallsolution-temp.json");
            using (Stream s = GenerateStreamFromString(linkedTemplate))
            {
                await blockBlob2.UploadFromStreamAsync(s);
            }

            var putResourceGroup = RESTApi.PutAsync(subscriptionID, resourcegroup, azuredeploy, accesstoken, jsonResourceGroup, true);


            var putcontent = RESTApi.PutAsync(subscriptionID, resourcegroupname, azuredeploy, accesstoken, jsonDeploy, false);
            JObject json = JsonConvert.DeserializeObject<JObject>(putcontent.Result);

            //Update Deployment Type to show deployed
            foreach (var deploy in deployTypes)
            {
                deploy.DeployState = "Deployed";
                deploy.DeployResult = await putcontent;
                _context.Update(deploy);
                await _context.SaveChangesAsync();
            }


            return RedirectToAction("IndexSelected", "DeployTypes", new { id = deployTypes[0].TennantID });
        }


        public async Task<IActionResult> DeployToAzureRDSMed(int Id)
        {
            var deployTypes = await _context.DeployTypes.Include(d => d.Tennants).Where(d => d.TennantID == Id).Where(d => d.AzureDeployName == "rdsmedsolution").ToListAsync();

            //Declare variables for use
            string tennantID = deployTypes.FirstOrDefault().Tennants.AzureTennantID;
            string clientID = deployTypes.FirstOrDefault().Tennants.AzureClientID;
            string secret = deployTypes.FirstOrDefault().Tennants.AzureClientSecret;
            string subscriptionID = deployTypes.FirstOrDefault().Tennants.AzureSubscriptionID;
            string resourcegroupname = deployTypes.FirstOrDefault().Tennants.ResourceGroupName;
            string resourcegroup = deployTypes.FirstOrDefault().Tennants.ResourceGroupName;
            string resourcegrouplocation = deployTypes.FirstOrDefault().Tennants.ResourceGroupLocation;
            string azuredeploy = string.Empty;

            var results = RESTApi.PostAction(tennantID, clientID, secret);
            RESTApi.AccessToken AccessToken = JsonConvert.DeserializeObject<RESTApi.AccessToken>(results.Result);
            string accesstoken = AccessToken.access_token;

            var sasToken = AzureHelper.GetSASToken(_storageConfig);
            

            string jsonResourceGroup = "{ \"location\": \"{resourcegrouplocation}\" }";
            jsonResourceGroup = jsonResourceGroup.Replace("{resourcegrouplocation}", resourcegrouplocation);

            //string jsonDeploy = "{\"properties\": { \"templateLink\": { \"uri\": \"https://cobwebjson.blob.core.windows.net/ansible/{template}{sasToken}\", \"contentVersion\": \"1.0.0.0\"}, \"mode\": \"Incremental\", \"parametersLink\": { \"uri\": \"https://cobwebjson.blob.core.windows.net/ansible/Parameters/{parameters}{sasToken}\", \"contentVersion\": \"1.0.0.0\" } } }";
            string jsonDeploy = "{\"properties\": { \"templateLink\": { \"uri\": \"https://cobwebjson.blob.core.windows.net/ansible/{template}{sasToken}\", \"contentVersion\": \"1.0.0.0\"}, \"mode\": \"Incremental\" } }";
            jsonDeploy = jsonDeploy.Replace("{sasToken}", sasToken);

            //Set Deploy Template dependent on Deployment Type
            jsonDeploy = jsonDeploy.Replace("{template}", "Linked/rdsmedsolution-temp.json");
            //jsonDeploy = jsonDeploy.Replace("{parameters}", "identitysmall-" + deployTypes.FirstOrDefault().Tennants.TennantName + "-param.json");
            azuredeploy = "rdsmedsolution";

            CloudStorageAccount storageAccount = new CloudStorageAccount(new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(_storageConfig.AccountName, _storageConfig.AccountKey), true);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("ansible/Linked");
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("rdsmedsolution.json");

            string linkedTemplate;
            using (var memoryStream = new MemoryStream())
            {
                await blockBlob.DownloadToStreamAsync(memoryStream);
                linkedTemplate = Encoding.UTF8.GetString(memoryStream.ToArray());
            }

            linkedTemplate = linkedTemplate.Replace("{templatelinkvnet}", "https://cobwebjson.blob.core.windows.net/ansible/VNet1SubnetsGW.json" + sasToken);
            linkedTemplate = linkedTemplate.Replace("{parameterlinkvnet}", "https://cobwebjson.blob.core.windows.net/ansible/Parameters/VNET-" + deployTypes.FirstOrDefault().Tennants.TennantName + "-param.json" + sasToken);
            linkedTemplate = linkedTemplate.Replace("{templatelinkid}", "https://cobwebjson.blob.core.windows.net/ansible/Identity/identity.json" + sasToken);
            linkedTemplate = linkedTemplate.Replace("{parameterlinkid}", "https://cobwebjson.blob.core.windows.net/ansible/Parameters/identity-" + deployTypes.FirstOrDefault().Tennants.TennantName + "-param.json" + sasToken);
            linkedTemplate = linkedTemplate.Replace("{templatelinkrds}", "https://cobwebjson.blob.core.windows.net/ansible/RDS/RDSMediumfull.json" + sasToken);
            linkedTemplate = linkedTemplate.Replace("{parameterlinkrds}", "https://cobwebjson.blob.core.windows.net/ansible/Parameters/rdsmedium-" + deployTypes.FirstOrDefault().Tennants.TennantName + "-param.json" + sasToken);

            CloudBlockBlob blockBlob2 = container.GetBlockBlobReference("rdsmedsolution-temp.json");
            using (Stream s = GenerateStreamFromString(linkedTemplate))
            {
                await blockBlob2.UploadFromStreamAsync(s);
            }

            var putResourceGroup = RESTApi.PutAsync(subscriptionID, resourcegroup, azuredeploy, accesstoken, jsonResourceGroup, true);


            var putcontent = RESTApi.PutAsync(subscriptionID, resourcegroupname, azuredeploy, accesstoken, jsonDeploy, false);
            JObject json = JsonConvert.DeserializeObject<JObject>(putcontent.Result);

            //Update Deployment Type to show deployed
            foreach (var deploy in deployTypes)
            {
                deploy.DeployState = "Deployed";
                deploy.DeployResult = await putcontent;
                _context.Update(deploy);
                await _context.SaveChangesAsync();
            }


            return RedirectToAction("IndexSelected", "DeployTypes", new { id = deployTypes[0].TennantID });
        }





        public async Task<IActionResult> GetDeploy(int Id)
        {
            var deployTypes = _context.DeployTypes.Include(d => d.Tennants).Where(d => d.DeployTypeID == Id).FirstOrDefault();

            string tennantID = deployTypes.Tennants.AzureTennantID;
            string clientID = deployTypes.Tennants.AzureClientID;
            string secret = deployTypes.Tennants.AzureClientSecret;
            string subscriptionID = deployTypes.Tennants.AzureSubscriptionID;
            string resourcegroupname = deployTypes.Tennants.ResourceGroupName;
            string resourcegroup = deployTypes.Tennants.ResourceGroupName;
            string azuredeploy = deployTypes.AzureDeployName;
        
            var results = RESTApi.PostAction(tennantID, clientID, secret);
            RESTApi.AccessToken AccessToken = JsonConvert.DeserializeObject<RESTApi.AccessToken>(results.Result);
            string accesstoken = AccessToken.access_token;

            var getcontent = RESTApi.GetAsync(subscriptionID, resourcegroupname,accesstoken,azuredeploy);

            if (await getcontent == null)
            {
                deployTypes.DeployResult = "";
            }
            else
            {
                deployTypes.DeployResult = await getcontent;
            }
            _context.Update(deployTypes);
            await _context.SaveChangesAsync();

            return RedirectToAction("IndexSelected", "DeployTypes", new { id = deployTypes.TennantID });
        }


        public async Task<IActionResult> GetDeployAll(int Id)
        {
            var deployTypes = await _context.DeployTypes.Include(d => d.Tennants).Where(d => d.TennantID == Id).Where(d => d.DeployState != null).ToListAsync();

            string tennantID = deployTypes.FirstOrDefault().Tennants.AzureTennantID;
            string clientID = deployTypes.FirstOrDefault().Tennants.AzureClientID;
            string secret = deployTypes.FirstOrDefault().Tennants.AzureClientSecret;
            string subscriptionID = deployTypes.FirstOrDefault().Tennants.AzureSubscriptionID;
            string resourcegroupname = deployTypes.FirstOrDefault().Tennants.ResourceGroupName;
            string resourcegroup = deployTypes.FirstOrDefault().Tennants.ResourceGroupName;
            string azuredeploy = string.Empty;

            //Add logic for getting deployment for solutions.
            //Example for rdssmallsolution below.

            foreach (var deploy in deployTypes)
            {
                if (deploy.AzureDeployName == "rdssmallsolution")
                {
                    if (deploy.DeployName.Contains("(IDS)") || deploy.DeployName.Contains("Identity Small"))
                    {
                        azuredeploy = "IDSmall";
                    }
                    if (deploy.DeployName.Contains("(RDSS)") || deploy.DeployName.Contains("RDS Small"))
                    {
                        azuredeploy = "RDSSmall";
                    }
                    if (deploy.DeployName.Contains("(VNET)"))
                    {
                        azuredeploy = "VNET";
                    }
                }
                else
                {
                    if (deploy.AzureDeployName == "rdsmedsolution")
                    {
                        if (deploy.DeployName.Contains("(IDM)") || deploy.DeployName.Contains("Identity Medium"))
                        {
                            azuredeploy = "IDMed";
                        }
                        if (deploy.DeployName.Contains("(RDSM)") || deploy.DeployName.Contains("RDS Medium"))
                        {
                            azuredeploy = "RDSMed";
                        }
                        if (deploy.DeployName.Contains("(VNET)"))
                        {
                            azuredeploy = "VNET";
                        }
                    }
                }

               
                var results = RESTApi.PostAction(tennantID, clientID, secret);
                RESTApi.AccessToken AccessToken = JsonConvert.DeserializeObject<RESTApi.AccessToken>(results.Result);
                string accesstoken = AccessToken.access_token;

                var getcontent = RESTApi.GetAsync(subscriptionID, resourcegroupname, accesstoken, azuredeploy);

                if (await getcontent == null)
                {
                      deploy.DeployResult = " ";
                }
                else
                {
                    deploy.DeployResult = await getcontent;
                }
                _context.Update(deploy);
                await _context.SaveChangesAsync();
            }
            //End of solution logic loop.

            return RedirectToAction("IndexSelected", "DeployTypes", new { id = deployTypes.FirstOrDefault().TennantID });
        }


        public async Task<IActionResult> SaveToAzure(int Id)
        {
            var tennantParams = await _context.TennantParams.Where(t => t.DeployTypeID == Id).ToListAsync();
            var deployTypes = _context.DeployTypes.Include(d => d.Tennants).Where(d => d.DeployTypeID == Id).FirstOrDefault();

            string filename = "{deploytype}-" + deployTypes.Tennants.TennantName + "-param.json";
            if (deployTypes.DeployName.Contains("VNET"))
            {
                filename = filename.Replace("{deploytype}", "VNET");
            }
            if (deployTypes.DeployName.Contains("(IDS)") || deployTypes.DeployName.Contains("Identity Small"))
            {
                filename = filename.Replace("{deploytype}", "identitysmall");
            };
            if (deployTypes.DeployName.Contains("(IDM)") || deployTypes.DeployName.Contains("Identity Medium"))
            {
                filename = filename.Replace("{deploytype}", "identity");
            };
            if (deployTypes.DeployName.Contains("(RDSS)") || deployTypes.DeployName.Contains("RDS Small"))
            {
                filename = filename.Replace("{deploytype}", "rdssmall");
            };
            if (deployTypes.DeployName.Contains("(RDSM)") || deployTypes.DeployName.Contains("RDS Medium"))
            {
                filename = filename.Replace("{deploytype}", "rdsmedium");
            };

            var JsonHeader = new Dictionary<string, string>();
            JsonHeader.Add("$schema" ,"https:\\schema.management.azure.com/schemas/2015-01-01/deploymentParameters.json#");
            JsonHeader.Add("contentVersion", "1.0.0.0");

            //Create String Builder to hold generated Json parameters file.
            StringBuilder tempjson = new StringBuilder();
            tempjson.AppendLine("{");
            tempjson.AppendLine("\t\"$schema\": \"https://schema.management.azure.com/schemas/2015-01-01/deploymentParameters.json#\",");
            tempjson.AppendLine("\t\"contentVersion\": \"1.0.0.0\",");
            tempjson.AppendLine("\t\"parameters\": {");

            //Loop through each parameter for the deployment and add them under the parameters Json key.
            for (var i = 0; i < tennantParams.Count(); i++)
            {
                tempjson.AppendLine("\t\t\"" + tennantParams[i].ParamName.ToString() + "\": {");
                if (tennantParams[i].ParamType == "domainadmin")
                {

                    tempjson.AppendLine("\t\t\t\"value\": " + "\"" + tennantParams[i].ParamValue.ToString().Replace(@"\",@"\\") + "\"");
                }
                else if (tennantParams[i].ParamType == "array")
                {
                    tempjson.AppendLine("\t\t\t\"value\": [");
                    tempjson.AppendLine("\t\t\t\t" + tennantParams[i].ParamValue.ToString());
                    tempjson.AppendLine("\t\t\t]");
                }
                else
                {
                    tempjson.AppendLine("\t\t\t\"value\": " + "\"" + tennantParams[i].ParamValue.ToString() + "\"");
                }
                if (i < tennantParams.Count -1)
                {
                    tempjson.AppendLine("\t\t},");
                }

                if (i == tennantParams.Count -1)
                {
                    tempjson.AppendLine("\t\t}");
                }
 
            };

            tempjson.AppendLine("\t}");
            tempjson.AppendLine("}");

            var jsonfull = tempjson.ToString();
 
            CloudStorageAccount storageAccount = new CloudStorageAccount(new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(_storageConfig.AccountName, _storageConfig.AccountKey), true);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("ansible/Parameters");
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(filename);

            using (Stream s = GenerateStreamFromString(jsonfull))
            {
                await blockBlob.UploadFromStreamAsync(s);
            }

            deployTypes.DeploySaved = "Yes";
            _context.Update(deployTypes);
            await _context.SaveChangesAsync();
            return RedirectToAction("IndexSelected", "DeployTypes", new { id = deployTypes.TennantID });
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
    }
}
