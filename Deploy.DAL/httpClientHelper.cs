using System.Net.Http;
using System.Threading.Tasks;
using System.Text;

namespace Deploy.DAL
{
    public class RESTApi
    {
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
            string uri = "https://management.azure.com/subscriptions/" + subscriptionID + "/resourcegroups/" + resourcegroup + "/providers/Microsoft.Resources/deployments/" + azuredeploy + "?api-version=2015-01-01";
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
                result = "true";
            }
            else
            {
                result = "false";
            }
            return result;
        }




    }
}
