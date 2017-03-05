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
using System.Text;
using System.IO;

namespace Deploy.Controllers
{
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
                
                var viewModel = new TennantDeployParams();
                viewModel.DeployName = DeployTypes.DeployName;
                viewModel.DeploySaved = DeployTypes.DeploySaved;

                if (viewModel.DeployName == "Identity Small")
                {
                    viewModel.DeployTypeID = TennantParams.DeployTypeID;
                    viewModel.TennantName = DeployTypes.Tennants.TennantName;
                    viewModel.TennantID = DeployTypes.Tennants.TennantID;

                    var Params = await _context.DeployParms.Where(d => d.ParameterDeployType == "ADS").ToListAsync();
                    var tennantParams = await _context.TennantParams.Where(t => t.DeployTypeID == Id).ToListAsync();
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

                }
                if (viewModel.DeployName == "RDS Small")
                {
                    viewModel.DeployTypeID = TennantParams.DeployTypeID;
                    viewModel.TennantName = DeployTypes.Tennants.TennantName;
                                        viewModel.TennantID = DeployTypes.Tennants.TennantID;

                    var Params = await _context.DeployParms.Where(d => d.ParameterDeployType == "RDSS").ToListAsync();
                    var tennantParams = await _context.TennantParams.Where(t => t.DeployTypeID == Id).ToListAsync();
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

            if (viewModel.DeployName.Contains("Identity"))
            {
                var parameters = await _context.DeployParms.Where(d => d.ParameterDeployType == "ADS").ToListAsync();
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
                        ParameterType = param.ParameterType,
                        ParameterDeployType = param.ParameterDeployType
                    });
                }
            }

            if (viewModel.DeployName.Contains("RDS"))
            {
                var parameters = await _context.DeployParms.Where(d => d.ParameterDeployType == "RDSS").ToListAsync();
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
                        ParameterType = param.ParameterType,
                        ParameterDeployType = param.ParameterDeployType
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
                    tennantparam.ParamType = tennantDeployParams.ParamType[i];
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
            viewModel.ParamType = new List<string>();
            viewModel.TennantParamID = new List<int>();

            foreach (var param in parameters)
            {
                viewModel.TennantParamID.Add(param.TennantParamID);
                viewModel.ParamName.Add(param.ParamName);
                viewModel.ParamValue.Add(param.ParamValue);
                viewModel.ParamType.Add(param.ParamType);
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
            var deployTypes = _context.DeployTypes.Include(d => d.Tennants).Where(d => d.DeployTypeID == Id).FirstOrDefault();

            
            string filename = deployTypes.DeployName + "-" + deployTypes.Tennants.TennantName + ".json";

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
 

            System.IO.File.WriteAllText(@"C:\temp\WriteLines.txt", jsonfull);

            CloudStorageAccount storageAccount = new CloudStorageAccount(new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(_storageConfig.AccountName, _storageConfig.AccountKey), true);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("testcontainer");
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
