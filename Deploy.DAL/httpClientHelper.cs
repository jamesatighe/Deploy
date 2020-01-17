using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Deploy.DAL
{
    public class RESTApi
    {

        private AzureStorageConfig _storageConfig;
        public RESTApi(AzureStorageConfig config)
        {
            _storageConfig = config;
        }
        public class AccessToken
        {
            public string token_type { get; set; }
            public string expires_in { get; set; }
            public string ext_expires_in { get; set; }
            public string expires_on { get; set; }
            public string not_before { get; set; }
            public string resource { get; set; }
            public string access_token { get; set; }
        }

        //Generate Access Token
        public static async Task<string> PostAction(string tennantID, string clientID, string secret)
        {
            string req = "grant_type=client_credentials&client_id=" + clientID + "&client_secret=" + secret + "&resource=https://management.azure.com/";
            string TokenEndpoint = "https://login.microsoftonline.com/" + tennantID;
            const string resource = "/oauth2/token?api-version=1.0";
            string uri = TokenEndpoint + resource;


            var httpClient = new HttpClient();
            var buffer = System.Text.Encoding.UTF8.GetBytes(req);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");

            var response = httpClient.PostAsync(uri, byteContent);
            
            var result = response.Result.Content;

            return await result.ReadAsStringAsync();
        }


        // Deploy API
        public static async Task<string> PutAsync(string subscriptionID, string resourcegroup, string azuredeploy, string accesstoken, string JObj, bool resource)
        {
            string uri = "";
            if (resource == true)
            {
                uri = "https://management.azure.com/subscriptions/" + subscriptionID + "/resourcegroups/" + resourcegroup + "?api-version=2015-01-01";
            }
            else
            {
                uri = "https://management.azure.com/subscriptions/" + subscriptionID + "/resourcegroups/" + resourcegroup + "/providers/Microsoft.Resources/deployments/" + azuredeploy + "?api-version=2015-01-01";
            }

            var content1 = new StringContent(JObj, Encoding.UTF8, "application/json");
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accesstoken);
            var response = httpClient.PutAsync(uri, content1);

            response.Result.EnsureSuccessStatusCode();
            var result = response.Result.Content;

            return await result.ReadAsStringAsync();
           
        }

        public static async Task<string> PutAsyncSB(string queuename, string accesstoken, string JObj)
        {
            string subscriptionID = "a512f15e-103a-47d8-8345-ab9a4dd34e9f";
            string resourcegroup = "Deployment";
            string namespaces = "cobwebdeployment";


            string uri = "https://management.azure.com/subscriptions/" + subscriptionID + "/resourcegroups/" + resourcegroup + "/providers/Microsoft.ServiceBus/namespaces/" + namespaces + "/queues/" + queuename + "?api-version=2015-08-01";


            var content1 = new StringContent(JObj, Encoding.UTF8, "application/json");
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accesstoken);
            var response = httpClient.PutAsync(uri, content1);

            response.Result.EnsureSuccessStatusCode();
            var result = response.Result.Content;

            return await result.ReadAsStringAsync();

        }



        public static async Task<string> PostAsync(string uri, string JObj)
        {
            var httpClient = new HttpClient();
            var buffer = System.Text.Encoding.UTF8.GetBytes(JObj);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");
            var response = await httpClient.PostAsync(uri, byteContent);

            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();

            return content;
        }

        public static async Task<string> GetAsync(string subscriptionID, string resourcegroup, string accesstoken, string azuredeploy)
        {
            string uri = "https://management.azure.com/subscriptions/" + subscriptionID + "/resourcegroups/" + resourcegroup + "/providers/Microsoft.Resources/deployments/" + azuredeploy + "/?api-version=2019-10-01";
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accesstoken);
            var response = httpClient.GetAsync(uri);
            //response.Result.EnsureSuccessStatusCode();
            var result = response.Result.Content;

            return await result.ReadAsStringAsync();
        }

        public static async Task<string> Validate(string subscriptionID, string resourcegroup, string azuredeploy, string accesstoken)
        {
            string uri = "https://management.azure.com/subscriptions/" + subscriptionID + "/resourcegroups/" + resourcegroup + "/providers/Microsoft.Resources/deployments/" + azuredeploy + "?api-version=2015-01-01";
            var httpClient = new HttpClient();
            string result = string.Empty;
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accesstoken);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Head, uri);
            
            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                result = "DeployExists";
            }
            else
            {
                result = "false";
            }
            return result;
        }

        public static async Task<string[]> ValidateTemplate(string subscriptionID, string resourcegroup, string azuredeploy, string accesstoken, string JObj)
        {
            string uri = "https://management.azure.com/subscriptions/" + subscriptionID + "/resourcegroups/" + resourcegroup + "/providers/Microsoft.Resources/deployments/" + azuredeploy + "/validate?api-version=2017-05-10";
            var httpClient = new HttpClient();
            string []results = new string[2];
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accesstoken);

            var content1 = new StringContent(JObj, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(uri, content1);

            if (response.IsSuccessStatusCode)
            {
                results[0] = "TemplateValid";
                results[1] = "";
                return results;
            }
            else
            {
                results[0] = "TemplateInvalid";
                results[1] = await response.Content.ReadAsStringAsync();
                return results;
            }
        }

        public async Task<string[]> EncryptionKeys()
        {
               
            string[] Keys = new string[3];
            CloudStorageAccount storageAccount = new CloudStorageAccount(new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(_storageConfig.AccountName, _storageConfig.AccountKey), true);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("keys");
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("keys.txt");

            string tempkeys;
            using (var memoryStream = new MemoryStream())
            {
                await blockBlob.DownloadToStreamAsync(memoryStream);
                tempkeys = Encoding.UTF8.GetString(memoryStream.ToArray());
            }
            Keys[0] = tempkeys.Split(',')[0];
            Keys[1] = tempkeys.Split(',')[1];
            Keys[2] = tempkeys.Split(',')[2];
            return Keys;
        }


        public static async Task<string> GetDeployJson(string subscriptionID, string accesstoken, string type, string location, string publisher = null, string offering = null, string resourceGroup = null, string VNET = null, string IP = null, string sku = null)
        {
            string uri = "";
            if (type == "VM")
            {
                uri = "https://management.azure.com/subscriptions/" + subscriptionID + "/providers/Microsoft.Compute/locations/" + location + "/vmSizes?api-version=2017-03-30";
            }
            else if (type == "SKU")
            {
                if (publisher == "null" && offering == "null")
                {
                    uri = "https://management.azure.com/subscriptions/" + subscriptionID + "/providers/Microsoft.Compute/locations/" + location + "/publishers/MicrosoftWindowsServer/artifacttypes/vmimage/offers/WindowsServer/skus?api-version=2017-03-30";
                }
                else
                {
                    uri = "https://management.azure.com/subscriptions/" + subscriptionID + "/providers/Microsoft.Compute/locations/" + location + "/publishers/" + publisher + "/artifacttypes/vmimage/offers/" + offering + "/skus?api-version=2017-03-30";
                }
            }
            else if (type == "publisher")
            {
                uri = "https://management.azure.com/subscriptions/" + subscriptionID + "/providers/Microsoft.Compute/locations/" + location + "/publishers?api-version=2017-03-30";
            }
            else if (type == "offering")
            {
                uri = "https://management.azure.com/subscriptions/" + subscriptionID + "/providers/Microsoft.Compute/locations/" + location + "/publishers/" + publisher + "/artifacttypes/vmimage/offers?api-version=2017-03-30";
            }
            else if (type == "VNET")
            {
                uri = "https://management.azure.com/subscriptions/" + subscriptionID + "/resourceGroups/" + resourceGroup + "/providers/Microsoft.Network/virtualnetworks?api-version=2017-06-01";
            }
            else if (type == "subnet")
            {
                uri = "https://management.azure.com/subscriptions/" + subscriptionID + "/resourceGroups/" + resourceGroup + "/providers/Microsoft.Network/virtualnetworks/" + VNET + "/subnets?api-version=2017-06-01";
            }
            else if (type == "resourceGroup")
            {
                uri = "https://management.azure.com/subscriptions/" + subscriptionID + "/resourceGroups?api-version=2017-06-01";
            }
            else if (type == "IPCheck")
            {
                uri = "https://management.azure.com/subscriptions/" + subscriptionID + "/resourceGroups/" + resourceGroup + "/providers/Microsoft.Network/virtualnetworks/" + VNET + "/CheckIPAddressAvailability?IPaddress=" + IP + "&api-version=2017-06-01";
            }
            else if (type == "image")
            {
                uri = "https://management.azure.com/subscriptions/" + subscriptionID + "/providers/Microsoft.Compute/locations/" + location + "/publishers/" + publisher + "/artifacttypes/vmimage/offers/" + offering + "/skus/" + sku + "/versions?api-version=2017-03-30";
            }
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accesstoken);
            var response = httpClient.GetAsync(uri);
            var result = response.Result.Content;
            return await result.ReadAsStringAsync();
                             
        }

    }
}
