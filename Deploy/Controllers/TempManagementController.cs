using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Deploy.DAL;
using Microsoft.Extensions.Options;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Deploy.Controllers
{

    [Authorize(Policy = "Admins")]
    public class TempManagementController : Controller
    {
        private IHostingEnvironment hostingEnv;
        private readonly DeployDBContext _context;
        private readonly AzureStorageConfig _storageConfig;

        public TempManagementController(DeployDBContext context, IOptions<AzureStorageConfig> config, IHostingEnvironment env)
        {
            _context = context;
            _storageConfig = config.Value;
            this.hostingEnv = env;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(IList<IFormFile> files)
        {
            long size = 0;
            foreach (var file in files)
            {
                string filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.ToString().Trim('"');
                filename = filename.Split(Path.DirectorySeparatorChar).Last();
                filename = hostingEnv.WebRootPath + $@"\csv\{filename}";
                string filenameNoExt = System.IO.Path.GetFileNameWithoutExtension(filename);
                
                size += file.Length;
                using (FileStream fs = System.IO.File.Create(filename))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }

                string[] csv = System.IO.File.ReadAllLines(filename);
                //validate if valid csv file
                if (csv[0] == "{")
                {

                    string joined = string.Join(Environment.NewLine, csv);
                    dynamic o = JObject.Parse(joined);


                    foreach (var param in o.parameters)
                    {
                        var tempparam = param.Name;
                        var tempValue = param.Value;
                        string type = string.Empty;
                        string defaultValue = string.Empty;
                        string description = "None";
                        foreach (JProperty x in (JToken)tempValue)
                        {
                            string name = x.Name;
                            JToken value = x.Value;
                            if (name == "type")
                            {
                                type = value.ToString();
                            }
                            if (name == "defaultValue")
                            {
                                defaultValue = value.ToString();
                            }
                            if (name == "metadata")
                            {
                                JToken props = x.First.First;

                                foreach (dynamic prop in props)
                                {
                                    JToken metaValue = prop.Value;
                                    description = metaValue.ToString();
                                }
                            }
                        }

                        System.IO.File.Delete(filename);
                        var DeployParams = new DeployParam();
                        DeployParams.ParameterDeployType = filenameNoExt;
                        DeployParams.ParameterName = tempparam;
                        DeployParams.ParameterType = type;
                        DeployParams.ParamToolTip = description;
                        DeployParams.DefaultValue = defaultValue;
                        _context.Add(DeployParams);
                        _context.SaveChanges();
                        ViewBag.Message = "Parameter file uploaded successfully!";
                    }
                }
                else if (csv[0] == "BaseOption,DataDisk,Domain,Size,DeployFile,ParamsFile,DeployName,DeployType")
                {
                    for (var i = 1; i < csv.Length; i++)
                    {
                        var DeployChoices = new DeployChoices();
                        var DeployList = new DeployList();


                        DeployChoices.BaseOption = csv[i].Split(',')[0];
                        if (bool.Parse(csv[i].Split(',')[1]) == false)
                        {
                            DeployChoices.Datadisk = false;
                        }
                        else
                        {
                            DeployChoices.Datadisk = true;
                        }
                        if (bool.Parse(csv[i].Split(',')[2]) == false)
                        {
                            DeployChoices.Domain = false;
                        }
                        else
                        {
                            DeployChoices.Domain = true;
                        }
                        DeployChoices.Size = csv[i].Split(',')[3];
                        DeployChoices.DeployFile = csv[i].Split(',')[4];
                        DeployChoices.ParamsFile = csv[i].Split(',')[5];
                        DeployChoices.DeployName = csv[i].Split(',')[6];
                        DeployChoices.DeployCode = csv[i].Split(',')[0];
                        _context.Add(DeployChoices);

                        DeployList.DeployName = csv[i].Split(',')[0];
                        DeployList.DeployValue = csv[i].Split(',')[6];
                        DeployList.DeployType = csv[i].Split(',')[7];
                        _context.Add(DeployList);

                        _context.SaveChanges();
                    }
                    ViewBag.Message = "Choices file uploaded successfully!";
                }
                else if (csv[0] == "CAPOLICIES")
                {
                    ViewBag.Message = "CA Policies uploaded successfully!";
                }
                else
                {
                    ViewBag.Message = $"{file.FileName} is not a valid CSV file.";
                    System.IO.File.Delete(filename);
                }


            }

            return View();
        }

        public FileResult Download(string type)
        {
            if (type == "DeployChoices")
            {
                var filename = hostingEnv.WebRootPath + $@"\csv\deploychoices.csv";
                byte[] filebytes = System.IO.File.ReadAllBytes(filename);
                return File(filebytes, "application/x-msdownload", "deploychoices.csv");
            }
            else 
            {
                var filename = hostingEnv.WebRootPath + $@"\csv\example.txt";
                byte[] filebytes = System.IO.File.ReadAllBytes(filename);
                return File(filebytes, "application/x-msdownload", "example.txt");
            }

        }

        //#########################################
        //##        GenerateSteamFromString      ##
        //#########################################

        public static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}