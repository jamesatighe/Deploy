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
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Deploy.Controllers
{
    public class TennantParamsController : Controller
    {
        private readonly DeployDBContext _context;

        public TennantParamsController(DeployDBContext context)
        {
            _context = context;    
        }

        //private AzureStorageConfig _storageConfig;
        //public TennantParamsController(IOptions<AzureStorageConfig> config)
        //{
        //    _storageConfig = config.Value;
        //}

        // GET: TennantParams
        public async Task<IActionResult> IndexSelected(int Id)
        {
            var TennantParams = _context.TennantParams.Where(t => t.DeployTypeID == Id).FirstOrDefault();
            var DeployTypes = _context.DeployTypes.Include(d => d.Tennants).Where(d => d.DeployTypeID == Id).FirstOrDefault();
            if (TennantParams != null)
            {
                
                var viewModel = new TennantDeployParams();
                viewModel.DeployName = DeployTypes.DeployName;

                if (viewModel.DeployName == "Identity Small")
                {
                    viewModel.DeployTypeID = TennantParams.DeployTypeID;
                    viewModel.TennantName = DeployTypes.Tennants.TennantName;
                    viewModel.TennantID = DeployTypes.Tennants.TennantID;

                    var Params = await _context.DeployParms.Where(d => d.ParameterType == "AD").ToListAsync();
                    var tennantParams = await _context.TennantParams.Where(t => t.DeployTypeID == Id).ToListAsync();
                    viewModel.DeployParams = new List<DeployParam>();
                    foreach (var Param in Params)
                    {
                        viewModel.DeployParams.Add(new DeployParam()
                        {
                            DeployParamID = Param.DeployParamID,
                            ParameterName = Param.ParameterName,
                            ParameterType = Param.ParameterType
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

                }
                if (viewModel.DeployName == "RDS Small")
                {
                    viewModel.DeployTypeID = TennantParams.DeployTypeID;
                    viewModel.TennantName = DeployTypes.Tennants.TennantName;
                                        viewModel.TennantID = DeployTypes.Tennants.TennantID;

                    var Params = await _context.DeployParms.Where(d => d.ParameterType == "RDS").ToListAsync();
                    var tennantParams = await _context.TennantParams.Where(t => t.DeployTypeID == Id).ToListAsync();
                    viewModel.DeployParams = new List<DeployParam>();
                    foreach (var Param in Params)
                    {
                        viewModel.DeployParams.Add(new DeployParam()
                        {
                            DeployParamID = Param.DeployParamID,
                            ParameterName = Param.ParameterName,
                            ParameterType = Param.ParameterType
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

            if (viewModel.DeployName.Contains("Identity"))
            {
                var parameters = await _context.DeployParms.Where(d => d.ParameterType == "AD").ToListAsync();
                viewModel.DeployParams = new List<DeployParam>();
                viewModel.TennantName = deploy.Tennants.TennantName;
                viewModel.TennantID = deploy.Tennants.TennantID;
                //viewModel.DeployParamID = new List<int>();

                foreach (var param in parameters)
                {
                    //viewModel.DeployParamID.Add(param.DeployParamID);

                    viewModel.DeployParams.Add(new DeployParam()
                    {
                        DeployParamID = param.DeployParamID,
                        ParameterName = param.ParameterName,
                        ParameterType = param.ParameterType
                    });
                }
            }

            if (viewModel.DeployName.Contains("RDS"))
            {
                var parameters = await _context.DeployParms.Where(d => d.ParameterType == "RDS").ToListAsync();
                viewModel.DeployParams = new List<DeployParam>();
                viewModel.TennantName = deploy.Tennants.TennantName;
                viewModel.TennantID = deploy.Tennants.TennantID;
                //viewModel.DeployParamID = new List<int>();

                foreach (var param in parameters)
                {
                    //viewModel.DeployParamID.Add(param.DeployParamID);

                    viewModel.DeployParams.Add(new DeployParam()
                    {
                        DeployParamID = param.DeployParamID,
                        ParameterName = param.ParameterName,
                        ParameterType = param.ParameterType
                    });
                }
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
            
            if (ModelState.IsValid)
            {
                for (var i = 0; i < tennantDeployParams.DeployParamID.Count(); i++)
                {
                    var tennantparam = new TennantParam();
                    tennantparam.DeployParamID = tennantDeployParams.DeployParamID[i];
                    tennantparam.ParamValue = tennantDeployParams.ParamValue[i];
                    tennantparam.ParamName = tennantDeployParams.ParamName[i];
                    tennantparam.DeployTypeID = tennantDeployParams.DeployTypeID;
                    _context.Add(tennantparam);
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
            viewModel.TennantParamID = new List<int>();

            foreach (var param in parameters)
            {
                viewModel.TennantParamID.Add(param.TennantParamID);
                viewModel.ParamName.Add(param.ParamName);
                viewModel.ParamValue.Add(param.ParamValue);
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
                    tennantparam.DeployTypeID = tennantDeployParams.DeployTypeID;
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


        public async Task<IActionResult> SaveToAzure(int Id)
        {
            var tennantParams = await _context.TennantParams.Where(t => t.DeployTypeID == Id).ToListAsync();

            var JsonList = new Dictionary<string, string>();


            foreach (var param in tennantParams)
            {
                JsonList.Add(param.ParamName, param.ParamValue);

                // Json = new TennantParamJSON();
                //Json.ParamName = param.ParamName;
                //Json.ParamValue = param.ParamValue;

            }
            string json1 = JsonConvert.SerializeObject(JsonList);
        
            System.IO.File.WriteAllText(@"C:\temp\WriteLines.txt", json1);

            CloudStorageAccount storageAccount = new CloudStorageAccount(new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials("tighedeployment1", "3jh8Xg/Wxod2E6HQQI2cNGSf11HuBnqB4XYe8xRBN044y+1GQCXrh+rNO0bJ6KahXbd8XREekyrujyyJCSFm6A=="), true);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("testcontainer");
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("params.json");
            using (var filestream = System.IO.File.OpenRead(@"C:\temp\writelines.txt"))
            {
                await blockBlob.UploadFromStreamAsync(filestream);
            }
            return RedirectToAction("IndexSelected", new { id = tennantParams.FirstOrDefault().DeployTypeID });
        }


        ////POST Action to Save Parameters to Azure(first step convert to JSON)
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async void SaveToAzure(int Id)
        //{
        //    var tennantParams = await _context.TennantParams.Include(t => t.DeployParams).Where(t => t.DeployTypeID == Id).ToListAsync();
        //    var viewModel = new TennantParamJSON();

        //    viewModel.ParamName = new List<string>();
        //    viewModel.ParamValue = new List<string>();

        //    foreach (var param in tennantParams)
        //    {
        //        viewModel.ParamName.Add(param.ParamName);
        //        viewModel.ParamValue.Add(param.ParamValue);
        //    }

        //    string json = JsonConvert.SerializeObject(viewModel);

        // }

        private bool TennantParamExists(int id)
        {
            return _context.TennantParams.Any(e => e.TennantParamID == id);
        }
    }
}
