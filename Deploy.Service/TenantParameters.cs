using System;
using Deploy.DAL;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Deploy.ViewModel;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace Deploy.Service
{
    [Authorize(Policy = "Admins")]
    public class TenantParameters
    {
        private readonly DeployDBContext _context;
        private readonly AzureStorageConfig _storageConfig;

        public TenantParameters(DeployDBContext context, IOptions<AzureStorageConfig> config)
        {
            _context = context;
            _storageConfig = config.Value;
        }

//#########################################
//##        GetTennantParams             ##
//#########################################

        public async Task<TennantDeployParamsViewModel> GetTenantParams(int Id)
        {

            var TennantParams = _context.TennantParams.Where(t => t.DeployTypeID == Id).FirstOrDefault();
            var DeployTypes = _context.DeployTypes.Include(d => d.Tennants).Where(d => d.DeployTypeID == Id).FirstOrDefault();
            if (TennantParams != null)
            {

                var viewModel = new TennantDeployParamsViewModel();
                viewModel.DeployName = DeployTypes.DeployName;
                viewModel.DeploySaved = DeployTypes.DeploySaved;


                viewModel.DeployTypeID = TennantParams.DeployTypeID;
                viewModel.TennantName = DeployTypes.Tennants.TennantName;
                viewModel.TennantID = DeployTypes.Tennants.TennantID;
                viewModel.DeployCode = DeployTypes.DeployCode;

                var Params = await _context.DeployParms.Where(d => d.ParameterDeployType == viewModel.DeployCode).ToListAsync();
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
                        ParamValue = tennant.ParamValue,
                        ParamType = tennant.ParamType,
                        ParamName = tennant.ParamName,
                        ParamToolTip = tennant.ParamToolTip,
                        TennantParamID = tennant.TennantParamID

                    });
                }

                return viewModel;

            }
            throw new NotImplementedException();
        }

//#########################################
//##        CreateTennantParams          ##
//#########################################

        public async Task<TennantDeployParamsViewModel> CreateTenantParams(int Id)
        {
            var deploy = _context.DeployTypes.Include(d => d.Tennants).Where(d => d.DeployTypeID == Id).FirstOrDefault();
            var viewModel = new TennantDeployParamsViewModel();

            viewModel.DeployTypeID = Id;
            viewModel.DeployName = deploy.DeployName;
            viewModel.DeploySaved = deploy.DeploySaved;

            var parameters = await _context.DeployParms.Where(d => d.ParameterDeployType == deploy.DeployCode).ToListAsync();

            viewModel.DeployParams = new List<DeployParam>();
            viewModel.TennantName = deploy.Tennants.TennantName;
            viewModel.TennantID = deploy.Tennants.TennantID;

            foreach (var param in parameters)
            {
                viewModel.DeployParams.Add(new DeployParam()
                {
                    DeployParamID = param.DeployParamID,
                    ParameterName = param.ParameterName,
                    ParameterType = param.ParameterType,
                    ParameterDeployType = param.ParameterDeployType,
                    ParamToolTip = param.ParamToolTip
                });
            }

            return viewModel;
        }

        //##########################################
        //##            QueueDeployment           ##
        //##########################################

        public async Task<string[]> QueueDeployment(int Id, bool Force)
        {
            var deployTypes = _context.DeployTypes.Include(d => d.Tennants).Where(d => d.DeployTypeID == Id).FirstOrDefault();

            string[] resultsarr = new string[3];
            //Declare variables for use
            string tennantID = deployTypes.Tennants.AzureTennantID;
            string clientID = deployTypes.Tennants.AzureClientID;
            string secret = deployTypes.Tennants.AzureClientSecret;
            string subscriptionID = deployTypes.Tennants.AzureSubscriptionID;
            string resourcegroupname = deployTypes.ResourceGroupName;
            if (string.IsNullOrEmpty(resourcegroupname) == true)
            {
                resourcegroupname = deployTypes.Tennants.ResourceGroupName;
            }
            string resourcegrouplocation = deployTypes.Tennants.ResourceGroupLocation;
            string azuredeploy = string.Empty;


            string jsonResourceGroup = "{ \"location\": \"{resourcegrouplocation}\" }";
            jsonResourceGroup = jsonResourceGroup.Replace("{resourcegrouplocation}", resourcegrouplocation);

            //Get Access Token from HTTP POST to Azure
            var results = RESTApi.PostAction(tennantID, clientID, secret);
            RESTApi.AccessToken AccessToken = JsonConvert.DeserializeObject<RESTApi.AccessToken>(results.Result);
            string accesstoken = AccessToken.access_token;
            var sasToken = AzureHelper.GetSASToken(_storageConfig);

            //Create json for deployment to be amended.
            string jsonDeploy = "{\"properties\": { \"templateLink\": { \"uri\": \"https://cobwebjson.blob.core.windows.net/ansible/{template}{sasToken}\", \"contentVersion\": \"1.0.0.0\"}, \"mode\": \"Incremental\", \"parametersLink\": { \"uri\": \"https://cobwebjson.blob.core.windows.net/ansible/{parameters}{sasToken}\", \"contentVersion\": \"1.0.0.0\" } } }";
            jsonDeploy = jsonDeploy.Replace("{sasToken}", sasToken);

            //Set Deploy Template dependent on Deployment Type
            //Add new Deployment Type logic here!

            jsonDeploy = jsonDeploy.Replace("{template}", deployTypes.DeployFile);
            string DepParams = deployTypes.ParamsFile;
            DepParams = DepParams.Replace("{tennant}", deployTypes.Tennants.TennantName);
            DepParams = DepParams.Replace("{id}", deployTypes.DeployTypeID.ToString());
            jsonDeploy = jsonDeploy.Replace("{parameters}", DepParams);
            azuredeploy = deployTypes.AzureDeployName;


            var putResourceGroup = RESTApi.PutAsync(subscriptionID, resourcegroupname, azuredeploy, accesstoken, jsonResourceGroup, true);

            //PUT request for deployment.
            var ValidateContent = RESTApi.Validate(subscriptionID, resourcegroupname, azuredeploy, accesstoken).Result;
            if (ValidateContent == "DeployExists" && Force == false)
            {
                resultsarr[0] = "DeployExists";
                resultsarr[1] = "";
                resultsarr[2] = "";
                return resultsarr;
            }
            else
            {
                var ValidateTemplate = RESTApi.ValidateTemplate(subscriptionID, resourcegroupname, azuredeploy, accesstoken, jsonDeploy).Result;
                if (ValidateTemplate[0] == "TemplateValid")
                {

                    //var putcontent = RESTApi.PutAsync(subscriptionID, resourcegroupname, azuredeploy, accesstoken, jsonDeploy, false);
                    //JObject json = JsonConvert.DeserializeObject<JObject>(putcontent.Result);

                    //Update Deployment Type to show deployed
                    deployTypes.DeployResult = "Queued";
                    _context.Update(deployTypes);
                    await _context.SaveChangesAsync();
                    ValidateContent = "False";
                    resultsarr[0] = "DeployNotExists";
                    resultsarr[1] = "TemplateValid";
                    resultsarr[2] = "";

                    var queue = new Queue();
                    queue.DeployTypeID = Id;
                    queue.Order = queue.QueueID;
                    queue.DeployName = deployTypes.DeployName;
                    queue.TennantName = deployTypes.Tennants.TennantName;
                    queue.TennantID = deployTypes.TennantID;
                    queue.azuredeploy = azuredeploy;
                    queue.subscriptionID = subscriptionID;
                    queue.resourcegroup = resourcegroupname;
                    queue.resource = false;

                    _context.Add(queue);
                    await _context.SaveChangesAsync();

                   
                    return resultsarr;
                }
                else
                {
                    resultsarr[0] = "DeployNotExists";
                    resultsarr[1] = ValidateTemplate[0];
                    resultsarr[2] = ValidateTemplate[1];
                    return resultsarr;
                }
            }
        }


        //##########################################
        //##            DeployfromQueue           ##
        //##########################################

        [DisableConcurrentExecution(7200)]
        public async Task<string> DeployfromQueue(int Id, bool Force)
        {

            var results = string.Empty;

            //Get Queue Item to process
            var QueueItem = _context.Queue.Include(q => q.DeployTypes).Include(q => q.DeployTypes.Tennants).Where(q => q.DeployTypeID == Id).FirstOrDefault();

            //Declare variables for use in Deploy method
            string resourcegroup = QueueItem.resourcegroup;
            string subscriptionID = QueueItem.subscriptionID;
            string azuredeploy = QueueItem.azuredeploy;
            string tennantID = QueueItem.DeployTypes.Tennants.AzureTennantID;
            string TennantName = QueueItem.DeployTypes.Tennants.TennantName;
            string clientID = QueueItem.DeployTypes.Tennants.AzureClientID;
            string secret = QueueItem.DeployTypes.Tennants.AzureClientSecret;
            string DepParams = QueueItem.DeployTypes.ParamsFile;
            string DeployFile = QueueItem.DeployTypes.DeployFile;

            //Get Access Token from HTTP POST to Azure
            var tokenResults = RESTApi.PostAction(tennantID, clientID, secret);
            RESTApi.AccessToken AccessToken = JsonConvert.DeserializeObject<RESTApi.AccessToken>(tokenResults.Result);
            string accesstoken = AccessToken.access_token;
            var sasToken = AzureHelper.GetSASToken(_storageConfig);

            //Create relevant JSON Object for deployment.
            string jsonDeploy = "{\"properties\": { \"templateLink\": { \"uri\": \"https://cobwebjson.blob.core.windows.net/ansible/{template}{sasToken}\", \"contentVersion\": \"1.0.0.0\"}, \"mode\": \"Incremental\", \"parametersLink\": { \"uri\": \"https://cobwebjson.blob.core.windows.net/ansible/{parameters}{sasToken}\", \"contentVersion\": \"1.0.0.0\" } } }";
            jsonDeploy = jsonDeploy.Replace("{sasToken}", sasToken);

            //Set Deploy Template dependent on Deployment Type
            //Add new Deployment Type logic here!

            jsonDeploy = jsonDeploy.Replace("{template}", DeployFile);
            DepParams = DepParams.Replace("{tennant}", TennantName);
            DepParams = DepParams.Replace("{id}", QueueItem.DeployTypes.DeployTypeID.ToString());
            jsonDeploy = jsonDeploy.Replace("{parameters}", DepParams);

            //Run main deploy REST API call to Azure
            var putcontent = RESTApi.PutAsync(subscriptionID, resourcegroup, azuredeploy, accesstoken, jsonDeploy, false);
            JObject json = JsonConvert.DeserializeObject<JObject>(putcontent.Result);

            //Run CheckQueue method to ensure deployment complete before moving onto next item.
            var check = CheckQueue(Id);
            while (!check.Result.Contains("Succeeded") && !check.Result.Contains("Failed"))
            {
                QueueItem.status = "Running";
                _context.Update(QueueItem);
                await _context.SaveChangesAsync();
                //Currently set to sleep for 30 secs
                //Thread.Sleep(30000);
                check = CheckQueue(Id);
            }

            //Check to see if deployment failed.
            if (check.Result.Contains("Failed"))
            {
                results = "Failed";
                QueueItem.status = "Failed - See Results";
                _context.Update(QueueItem);
            }
            //On successful completion of deployment mark Queue Item to Complete.
            results = "Succeeded";
            QueueItem.status = "Complete";
            _context.Update(QueueItem);

            await _context.SaveChangesAsync();
            return results;

        }

        //##########################################
        //##            CheckQueue                ##
        //##########################################

        public async Task<string> CheckQueue(int Id)
        {
            var QueueList = _context.Queue.Include(q => q.DeployTypes).Where(q => q.DeployTypeID == Id).FirstOrDefault();

            string tennantID = QueueList.DeployTypes.Tennants.AzureTennantID;
            string clientID = QueueList.DeployTypes.Tennants.AzureClientID;
            string secret = QueueList.DeployTypes.Tennants.AzureClientSecret;
            string subscriptionID = QueueList.DeployTypes.Tennants.AzureSubscriptionID;
            string resourcegroup = QueueList.resourcegroup;
            if (string.IsNullOrEmpty(resourcegroup) == true)
            {
                resourcegroup = QueueList.DeployTypes.Tennants.ResourceGroupName;
            }
            string azuredeploy = QueueList.azuredeploy;

            var results = RESTApi.PostAction(tennantID, clientID, secret);
            RESTApi.AccessToken AccessToken = JsonConvert.DeserializeObject<RESTApi.AccessToken>(results.Result);
            string accesstoken = AccessToken.access_token;

            var getcontent = RESTApi.GetAsync(subscriptionID, resourcegroup, accesstoken, azuredeploy);

            if (await getcontent == null)
            {
                QueueList.DeployTypes.DeployResult = "";
            }
            else
            {
                QueueList.DeployTypes.DeployResult = await getcontent;
            }
            _context.Update(QueueList.DeployTypes);
            await _context.SaveChangesAsync();


            var content = await getcontent;
            return content;


        }

        //#########################################
        //##        DeployToAzure                ##
        //#########################################

        public async Task<string[]> DeployToAzure(int Id, bool Force)
        {

            var deployTypes = _context.DeployTypes.Include(d => d.Tennants).Where(d => d.DeployTypeID == Id).FirstOrDefault();
            string[] resultsarr = new string[3];
            //Declare variables for use
            string tennantID = deployTypes.Tennants.AzureTennantID;
            string clientID = deployTypes.Tennants.AzureClientID;
            string secret = deployTypes.Tennants.AzureClientSecret;
            string subscriptionID = deployTypes.Tennants.AzureSubscriptionID;
            string resourcegroupname = deployTypes.ResourceGroupName;
            if (string.IsNullOrEmpty(resourcegroupname) == true)
            {
                resourcegroupname = deployTypes.Tennants.ResourceGroupName;
            }
            string resourcegrouplocation = deployTypes.Tennants.ResourceGroupLocation;
            string azuredeploy = string.Empty;


            string jsonResourceGroup = "{ \"location\": \"{resourcegrouplocation}\" }";
            jsonResourceGroup = jsonResourceGroup.Replace("{resourcegrouplocation}", resourcegrouplocation);

            //Get Access Token from HTTP POST to Azure
            var results = RESTApi.PostAction(tennantID, clientID, secret);
            RESTApi.AccessToken AccessToken = JsonConvert.DeserializeObject<RESTApi.AccessToken>(results.Result);
            string accesstoken = AccessToken.access_token;
            var sasToken = AzureHelper.GetSASToken(_storageConfig);

            //Create json for deployment to be amended.
            string jsonDeploy = "{\"properties\": { \"templateLink\": { \"uri\": \"https://cobwebjson.blob.core.windows.net/ansible/{template}{sasToken}\", \"contentVersion\": \"1.0.0.0\"}, \"mode\": \"Incremental\", \"parametersLink\": { \"uri\": \"https://cobwebjson.blob.core.windows.net/ansible/{parameters}{sasToken}\", \"contentVersion\": \"1.0.0.0\" } } }";
            jsonDeploy = jsonDeploy.Replace("{sasToken}", sasToken);

            //Set Deploy Template dependent on Deployment Type
            //Add new Deployment Type logic here!

            jsonDeploy = jsonDeploy.Replace("{template}", deployTypes.DeployFile);
            var DepParams = deployTypes.ParamsFile;
            DepParams = DepParams.Replace("{tennant}", deployTypes.Tennants.TennantName);
            DepParams = DepParams.Replace("{id}", deployTypes.DeployTypeID.ToString());
            jsonDeploy = jsonDeploy.Replace("{parameters}", DepParams);
            azuredeploy = deployTypes.AzureDeployName;

           
            var putResourceGroup = RESTApi.PutAsync(subscriptionID, resourcegroupname, azuredeploy, accesstoken, jsonResourceGroup, true);

            //PUT request for deployment.
            var ValidateContent = RESTApi.Validate(subscriptionID, resourcegroupname, azuredeploy, accesstoken).Result;
            if (ValidateContent == "DeployExists" && Force == false)
            {
                resultsarr[0] = "DeployExists";
                resultsarr[1] = "";
                resultsarr[2] = "";
                return resultsarr;
            }
            else
            {
                var ValidateTemplate = RESTApi.ValidateTemplate(subscriptionID, resourcegroupname, azuredeploy, accesstoken, jsonDeploy).Result;
                if (ValidateTemplate[0] == "TemplateValid")
                {

                    var putcontent = RESTApi.PutAsync(subscriptionID, resourcegroupname, azuredeploy, accesstoken, jsonDeploy, false);
                    JObject json = JsonConvert.DeserializeObject<JObject>(putcontent.Result);

                    //Update Deployment Type to show deployed
                    deployTypes.DeployState = "Deployed";
                    deployTypes.DeployResult = await putcontent;
                    _context.Update(deployTypes);
                    await _context.SaveChangesAsync();
                    ValidateContent = "False";
                    resultsarr[0] = "DeployNotExists";
                    resultsarr[1] = "TemplateValid";
                    resultsarr[2] = "";
                    return resultsarr;
                }
                else
                {
                    resultsarr[0] = "DeployNotExists";
                    resultsarr[1] = ValidateTemplate[0];
                    resultsarr[2] = ValidateTemplate[1];
                    return resultsarr;
                }
            }
        }



/*
 _____           _           _____    ____________  _____ _____                 _ _ 
|  _  \         | |          |_ _|    | ___ \  _  \/  ___/  ___|               | | |
| | | |___ _ __ | | ___  _   _| | ___ | |_/ / | | |\ `--.\ `--. _ __ ___   __ _| | |
| | | / _ \ '_ \| |/ _ \| | | | |/ _ \|    /| | | | `--. \`--. \ '_ ` _ \ / _` | | |
| |/ /  __/ |_) | | (_) | |_| | | (_) | |\ \| |/ / /\__/ /\__/ / | | | | | (_| | | |
|___/ \___| .__/|_|\___/ \__, \_/\___/\_| \_|___/  \____/\____/|_| |_| |_|\__,_|_|_|
          | |             __/ |                                                     
          |_|            |___/                                                      
*/
        public async Task<String[]> DeployRDSSmall(int Id)
        { 
            var deployTypes = await _context.DeployTypes.Include(d => d.Tennants).Where(d => d.TennantID == Id).Where(d => d.AzureDeployName.Contains("rdssmall") && d.DeployState != "Deployed").ToListAsync();

            //Declare variables for use
            string[] resultsarr = new string[3];
            string tennantID = deployTypes.FirstOrDefault().Tennants.AzureTennantID;
            string clientID = deployTypes[0].Tennants.AzureClientID;
            string secret = deployTypes.FirstOrDefault().Tennants.AzureClientSecret;
            string subscriptionID = deployTypes.FirstOrDefault().Tennants.AzureSubscriptionID;
            string resourcegroup = deployTypes.FirstOrDefault().ResourceGroupName;
            string azuredeploy = string.Empty;


            var VNETSmallID = deployTypes.Where(d => d.DeployName.Contains("(VNET)")).FirstOrDefault().DeployTypeID;
            var VNETSmallDep = deployTypes.Where(d => d.DeployName.Contains("(VNET)")).FirstOrDefault().AzureDeployName;

            var IdentitySmallID = deployTypes.Where(d => d.DeployName.Contains("(IDS)")).FirstOrDefault().DeployTypeID;
            var IdentitySmallDep = deployTypes.Where(d => d.DeployName.Contains("(IDS)")).FirstOrDefault().AzureDeployName;

            var RDSSmallID = deployTypes.Where(d => d.DeployName.Contains("(RDSS)")).FirstOrDefault().DeployTypeID;
            var RDSSmallDep = deployTypes.Where(d => d.DeployName.Contains("(RDSS)")).FirstOrDefault().AzureDeployName;

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

            Guid guid = Guid.NewGuid();
            azuredeploy = "rdssmallsolution" + guid;

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

            linkedTemplate = linkedTemplate.Replace("{VNETDep}", VNETSmallDep);
            linkedTemplate = linkedTemplate.Replace("{templatelinkvnet}", "https://cobwebjson.blob.core.windows.net/ansible/Network/VNet1SubnetsGW.json" + sasToken);
            linkedTemplate = linkedTemplate.Replace("{parameterlinkvnet}", "https://cobwebjson.blob.core.windows.net/ansible/Parameters/rdssmall-" + deployTypes.FirstOrDefault().Tennants.TennantName + "-" + VNETSmallID + "-param.json" + sasToken);

            linkedTemplate = linkedTemplate.Replace("{IDDep}", IdentitySmallDep);
            linkedTemplate = linkedTemplate.Replace("{templatelinkid}", "https://cobwebjson.blob.core.windows.net/ansible/Identity/identitysmallMD.json" + sasToken);
            linkedTemplate = linkedTemplate.Replace("{parameterlinkid}", "https://cobwebjson.blob.core.windows.net/ansible/Parameters/rdssmall-" + deployTypes.FirstOrDefault().Tennants.TennantName + "-" + IdentitySmallID + "-param.json" + sasToken);

            linkedTemplate = linkedTemplate.Replace("{RDSDep}", RDSSmallDep);
            linkedTemplate = linkedTemplate.Replace("{templatelinkrds}", "https://cobwebjson.blob.core.windows.net/ansible/RDS/RDSSmallfullMD.json" + sasToken);
            linkedTemplate = linkedTemplate.Replace("{parameterlinkrds}", "https://cobwebjson.blob.core.windows.net/ansible/Parameters/rdssmall-" + deployTypes.FirstOrDefault().Tennants.TennantName + "-" + RDSSmallID + "-param.json" + sasToken);

            CloudBlockBlob blockBlob2 = container.GetBlockBlobReference("rdssmallsolution-temp.json");
            using (Stream s = GenerateStreamFromString(linkedTemplate))
            {
                await blockBlob2.UploadFromStreamAsync(s);
            }

            var putResourceGroup = RESTApi.PutAsync(subscriptionID, resourcegroup, azuredeploy, accesstoken, jsonResourceGroup, true);

            var ValidateTemplate = RESTApi.ValidateTemplate(subscriptionID, resourcegroup, azuredeploy, accesstoken, jsonDeploy).Result;
            if (ValidateTemplate[0] == "TemplateValid")
            {
                var putcontent = RESTApi.PutAsync(subscriptionID, resourcegroup, azuredeploy, accesstoken, jsonDeploy, false);
                JObject json = JsonConvert.DeserializeObject<JObject>(putcontent.Result);

                //Update Deployment Type to show deployed
                foreach (var deploy in deployTypes)
                {
                    deploy.DeployState = "Deployed";
                    deploy.DeployResult = await putcontent;
                    _context.Update(deploy);
                    await _context.SaveChangesAsync();
                }
                resultsarr[0] = "DeployNotExists";
                resultsarr[1] = "TemplateValid";
                resultsarr[2] = "";
                return resultsarr;
            }
            else
            {
                resultsarr[0] = "DeployNotExists";
                resultsarr[1] = ValidateTemplate[0];
                resultsarr[2] = ValidateTemplate[1];
                return resultsarr;
            }
        }

/*
______           _           _____   ____________  ________   ___         _ 
|  _  \         | |          |_ _|   | ___ \  _  \/  ___ |  \/  |        | |
| | | |___ _ __ | | ___ _    _| | ___ | |_/ / | | |\ `--.| .  . | ___ __ | |
| | | / _ \ '_ \| |/ _ \| | | | |/ _ \|    /| | | | `--. \ |\/| |/ _ \/ _` |
| |/ /  __/ |_) | | (_) | |_| | | (_) | |\ \| |/ / /\__/ / |  | |  __/ (_| |
|___/ \___| .__/|_|\___/ \__, \_/\___/\_| \_|___/  \____/\_|  |_/\___|\__,_|
          | |             __/ |                                             
          |_|            |___/                                              
*/

        public async Task<String[]> DeployRDSMed(int Id)
        {
            var deployTypes = await _context.DeployTypes.Include(d => d.Tennants).Where(d => d.TennantID == Id).Where(d => d.AzureDeployName.Contains("rdsmed") && d.DeployState != "Deployed").ToListAsync();

            //Declare variables for use
            string[] resultsarr = new string[3];
            string tennantID = deployTypes.FirstOrDefault().Tennants.AzureTennantID;
            string clientID = deployTypes.FirstOrDefault().Tennants.AzureClientID;
            string secret = deployTypes.FirstOrDefault().Tennants.AzureClientSecret;
            string subscriptionID = deployTypes.FirstOrDefault().Tennants.AzureSubscriptionID;
            string resourcegroupname = deployTypes.FirstOrDefault().Tennants.ResourceGroupName;
            string resourcegroup = deployTypes.FirstOrDefault().Tennants.ResourceGroupName;
            string resourcegrouplocation = deployTypes.FirstOrDefault().Tennants.ResourceGroupLocation;
            string azuredeploy = string.Empty;

            var VNETMedID = deployTypes.Where(d => d.DeployName.Contains("(VNET)")).FirstOrDefault().DeployTypeID;
            var VNETMedDep = deployTypes.Where(d => d.DeployName.Contains("(VNET)")).FirstOrDefault().AzureDeployName;

            var IdentityMedID = deployTypes.Where(d => d.DeployName.Contains("(IDM)")).FirstOrDefault().DeployTypeID;
            var IdentityMedDep = deployTypes.Where(d => d.DeployName.Contains("(IDM)")).FirstOrDefault().AzureDeployName;

            var RDSMedID = deployTypes.Where(d => d.DeployName.Contains("(RDSM)")).FirstOrDefault().DeployTypeID;
            var RDSMedDep = deployTypes.Where(d => d.DeployName.Contains("(RDSM)")).FirstOrDefault().AzureDeployName;


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

            Guid guid = Guid.NewGuid();
            azuredeploy = "rdsmedsolution" + guid;

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



            linkedTemplate = linkedTemplate.Replace("{templatelinkvnet}", "https://cobwebjson.blob.core.windows.net/ansible/Network/VNet1SubnetsGW.json" + sasToken);
            linkedTemplate = linkedTemplate.Replace("{parameterlinkvnet}", "https://cobwebjson.blob.core.windows.net/ansible/Parameters/rdsmed-" + deployTypes.FirstOrDefault().Tennants.TennantName + "-" + VNETMedID + "-param.json" + sasToken);
            if (deployTypes.FirstOrDefault().Tennants.TennantName.Contains("TypeTec"))
            {
                linkedTemplate = linkedTemplate.Replace("{templatelinkid}", "https://cobwebjson.blob.core.windows.net/ansible/Identity/identityMDType.json" + sasToken);
                linkedTemplate = linkedTemplate.Replace("{parameterlinkid}", "https://cobwebjson.blob.core.windows.net/ansible/Parameters/rdsmed-" + deployTypes.FirstOrDefault().Tennants.TennantName + "-" + IdentityMedID + "-param.json" + sasToken);
                linkedTemplate = linkedTemplate.Replace("{templatelinkrds}", "https://cobwebjson.blob.core.windows.net/ansible/RDS/RDSMediumfullType.json" + sasToken);
                linkedTemplate = linkedTemplate.Replace("{parameterlinkrds}", "https://cobwebjson.blob.core.windows.net/ansible/Parameters/rdsmed-" + deployTypes.FirstOrDefault().Tennants.TennantName + "-" + RDSMedID + "-param.json" + sasToken);
            }
            else
            {
                linkedTemplate = linkedTemplate.Replace("{templatelinkid}", "https://cobwebjson.blob.core.windows.net/ansible/Identity/identityMD.json" + sasToken);
                linkedTemplate = linkedTemplate.Replace("{parameterlinkid}", "https://cobwebjson.blob.core.windows.net/ansible/Parameters/rdsmed-" + deployTypes.FirstOrDefault().Tennants.TennantName + "-" + IdentityMedID + "-param.json" + sasToken);
                linkedTemplate = linkedTemplate.Replace("{templatelinkrds}", "https://cobwebjson.blob.core.windows.net/ansible/RDS/RDSMediumfull.json" + sasToken);
                linkedTemplate = linkedTemplate.Replace("{parameterlinkrds}", "https://cobwebjson.blob.core.windows.net/ansible/Parameters/rdsmed-" + deployTypes.FirstOrDefault().Tennants.TennantName + "-" + RDSMedID + "-param.json" + sasToken);
            }
            
            CloudBlockBlob blockBlob2 = container.GetBlockBlobReference("rdsmedsolution-temp.json");
            using (Stream s = GenerateStreamFromString(linkedTemplate))
            {
                await blockBlob2.UploadFromStreamAsync(s);
            }

            var putResourceGroup = RESTApi.PutAsync(subscriptionID, resourcegroup, azuredeploy, accesstoken, jsonResourceGroup, true);

            var ValidateTemplate = RESTApi.ValidateTemplate(subscriptionID, resourcegroup, azuredeploy, accesstoken, jsonDeploy).Result;
            if (ValidateTemplate[0] == "TemplateValid")
            {
                var putcontent = RESTApi.PutAsync(subscriptionID, resourcegroup, azuredeploy, accesstoken, jsonDeploy, false);
                JObject json = JsonConvert.DeserializeObject<JObject>(putcontent.Result);

                //Update Deployment Type to show deployed
                foreach (var deploy in deployTypes)
                {
                    deploy.DeployState = "Deployed";
                    deploy.DeployResult = await putcontent;
                    _context.Update(deploy);
                    await _context.SaveChangesAsync();
                }
                resultsarr[0] = "DeployNotExists";
                resultsarr[1] = "TemplateValid";
                resultsarr[2] = "";
                return resultsarr;
            }
            else
            {
                resultsarr[0] = "DeployNotExists";
                resultsarr[1] = ValidateTemplate[0];
                resultsarr[2] = ValidateTemplate[1];
                return resultsarr;
            }
        }



 //#########################################
//##        GetDeploy                    ##
//#########################################

        public async Task GetDeploy(int Id)
        {
            var deployTypes = _context.DeployTypes.Include(d => d.Tennants).Where(d => d.DeployTypeID == Id).FirstOrDefault();

            string tennantID = deployTypes.Tennants.AzureTennantID;
            string clientID = deployTypes.Tennants.AzureClientID;
            string secret = deployTypes.Tennants.AzureClientSecret;
            string subscriptionID = deployTypes.Tennants.AzureSubscriptionID;
            string resourcegroupname = deployTypes.ResourceGroupName;
            if (string.IsNullOrEmpty(resourcegroupname) == true)
            {
                resourcegroupname = deployTypes.Tennants.ResourceGroupName;
            }
            string resourcegroup = deployTypes.Tennants.ResourceGroupName;
            string azuredeploy = deployTypes.AzureDeployName;

            var results = RESTApi.PostAction(tennantID, clientID, secret);
            RESTApi.AccessToken AccessToken = JsonConvert.DeserializeObject<RESTApi.AccessToken>(results.Result);
            string accesstoken = AccessToken.access_token;

            var getcontent = RESTApi.GetAsync(subscriptionID, resourcegroupname, accesstoken, azuredeploy);

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
        }

//#########################################
//##        GetDeployAll                 ##
//#########################################

        public async Task GetDeployAll(int Id)
        {
            var deployTypes = await _context.DeployTypes.Include(d => d.Tennants).Where(d => d.TennantID == Id).Where(d => d.DeployState != null).ToListAsync();

            string tennantID = deployTypes.FirstOrDefault().Tennants.AzureTennantID;
            string clientID = deployTypes.FirstOrDefault().Tennants.AzureClientID;
            string secret = deployTypes.FirstOrDefault().Tennants.AzureClientSecret;
            string subscriptionID = deployTypes.FirstOrDefault().Tennants.AzureSubscriptionID;
            //string resourcegroupname = deployTypes.FirstOrDefault().Tennants.ResourceGroupName;
            //if (string.IsNullOrEmpty(resourcegroupname) == true)
            //{
            //    resourcegroupname = deployTypes.FirstOrDefault().Tennants.ResourceGroupName;
            //}
            string resourcegroup = deployTypes.FirstOrDefault().Tennants.ResourceGroupName;
            string azuredeploy = string.Empty;

            //Add logic for getting deployment for solutions.
            //Example for rdssmallsolution below.

            for (var i = 0; i < deployTypes.Count(); i++)
            {
                string resourcegroupname = deployTypes[i].ResourceGroupName;
                if (string.IsNullOrEmpty(resourcegroupname) == true)
                {
                    resourcegroupname = deployTypes.FirstOrDefault().Tennants.ResourceGroupName;
                }
                if (deployTypes[i].AzureDeployName.Contains("rdssmallsolution"))
                {
                    if (deployTypes[i].DeployName.Contains("(IDS)") || deployTypes[i].DeployName.Contains("Identity Small"))
                    {
                        azuredeploy = "IDSmall";
                    }
                    if (deployTypes[i].DeployName.Contains("(RDSS)") || deployTypes[i].DeployName.Contains("RDS Small"))
                    {
                        azuredeploy = "RDSSmall";
                    }
                    if (deployTypes[i].DeployName.Contains("(VNET)"))
                    {
                        azuredeploy = "VNET";
                    }
                }
                else
                {
                    if (deployTypes[i].AzureDeployName.Contains("rdsmedsolution"))
                    {
                        if (deployTypes[i].DeployName.Contains("(IDM)") || deployTypes[i].DeployName.Contains("Identity Medium") || deployTypes[i].DeployName.Contains("(IDMTYPE)"))
                        {
                            azuredeploy = "IDMed";
                        }
                        if (deployTypes[i].DeployName.Contains("(RDSM)") || deployTypes[i].DeployName.Contains("RDS Medium") || deployTypes[i].DeployName.Contains("(RDSMTYPE)"))
                        {
                            azuredeploy = "RDSMed";
                        }
                        if (deployTypes[i].DeployName.Contains("(VNET)"))
                        {
                            azuredeploy = "VNET";
                        }
                    }

                    else
                    {
                      azuredeploy = deployTypes[i].AzureDeployName;
                    }
                }
                var results = RESTApi.PostAction(tennantID, clientID, secret);
                RESTApi.AccessToken AccessToken = JsonConvert.DeserializeObject<RESTApi.AccessToken>(results.Result);
                string accesstoken = AccessToken.access_token;

                var getcontent = RESTApi.GetAsync(subscriptionID, resourcegroupname, accesstoken, azuredeploy);

                if (await getcontent == null)
                {
                    deployTypes[i].DeployResult = " ";
                }
                else
                {
                    deployTypes[i].DeployResult = await getcontent;
                }
                _context.Update(deployTypes[i]);
                await _context.SaveChangesAsync();
            }
            //End of solution logic loop.
        }

//#########################################
//##        SaveToAzure                  ##
//#########################################

        public async Task SaveToAzure(int Id)
        {
            var tennantParams = await _context.TennantParams.Where(t => t.DeployTypeID == Id).ToListAsync();
            var deployTypes = _context.DeployTypes.Include(d => d.Tennants).Where(d => d.DeployTypeID == Id).FirstOrDefault();
            var sasToken = AzureHelper.GetSASToken(_storageConfig);

            var encrypt = new RESTApi(_storageConfig);
            string[] Keys = await encrypt.EncryptionKeys();
            var encryption = new Encryption(Keys[1], Keys[0], 1, Keys[2], 256);

            string filename = deployTypes.ParamsFile;
            filename = filename.Replace("{tennant}", deployTypes.Tennants.TennantName);
            filename = filename.Replace("{id}", deployTypes.DeployTypeID.ToString());

            var JsonHeader = new Dictionary<string, string>();
            JsonHeader.Add("$schema", "https:\\schema.management.azure.com/schemas/2015-01-01/deploymentParameters.json#");
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
                if (tennantParams[i].ParamType == "domainadmin" || tennantParams[i].ParamType == "directorypath")
                {

                    tempjson.AppendLine("\t\t\t\"value\": " + "\"" + tennantParams[i].ParamValue.ToString().Replace(@"\", @"\\") + "\"");
                }
                else if (tennantParams[i].ParamType == "array")
                {
                    tempjson.AppendLine("\t\t\t\"value\": [");
                    tempjson.AppendLine("\t\t\t\t" + tennantParams[i].ParamValue.ToString());
                    tempjson.AppendLine("\t\t\t]");
                }
                else if (tennantParams[i].ParamType == "password")
                {
                    var decrypted = encryption.DecryptString(tennantParams[i].ParamValue);
                    tempjson.AppendLine("\t\t\t\"value\": " + "\"" + decrypted + "\"");
                }
                else
                {
                    tempjson.AppendLine("\t\t\t\"value\": " + "\"" + tennantParams[i].ParamValue.ToString() + "\"");
                }
                if (i < tennantParams.Count - 1)
                {
                    tempjson.AppendLine("\t\t},");
                }

                if (i == tennantParams.Count - 1)
                {
                    tempjson.AppendLine("\t\t}");
                }

            };

            tempjson.AppendLine("\t}");
            tempjson.AppendLine("}");

            var jsonfull = tempjson.ToString();


            
            CloudStorageAccount storageAccount = new CloudStorageAccount(new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(_storageConfig.AccountName, _storageConfig.AccountKey), true);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("ansible/");
            CloudBlockBlob blockBlob1 = container.GetBlockBlobReference(filename);

            using (Stream s = GenerateStreamFromString(jsonfull))
            {
                await blockBlob1.UploadFromStreamAsync(s);
            }

            deployTypes.DeploySaved = "Yes";
            deployTypes.DeployResult = null;
            deployTypes.DeployState = null;
            deployTypes.ParamsFile = filename;
            _context.Update(deployTypes);
            await _context.SaveChangesAsync();
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

        public string GetDeployJson(int Id, string type, string publisher = null, string offering = null, string resourceGroup = null, string VNET = null, string IP = null)
        {
            var deployTypes = _context.DeployTypes.Include(d => d.Tennants).Where(d => d.DeployTypeID == Id).FirstOrDefault();
            string subscriptionID = deployTypes.Tennants.AzureSubscriptionID;
            string clientID = deployTypes.Tennants.AzureClientID;
            string secret = deployTypes.Tennants.AzureClientSecret;
            string tennantID = deployTypes.Tennants.AzureTennantID;
            string location = deployTypes.Tennants.ResourceGroupLocation;
            
            var results = RESTApi.PostAction(tennantID, clientID, secret);
            RESTApi.AccessToken AccessToken = JsonConvert.DeserializeObject<RESTApi.AccessToken>(results.Result);
            string accesstoken = AccessToken.access_token;

            var getcontent = RESTApi.GetDeployJson(subscriptionID, accesstoken, type, location, publisher, offering, resourceGroup, VNET, IP);
            return getcontent.Result;
        }

    }
}
