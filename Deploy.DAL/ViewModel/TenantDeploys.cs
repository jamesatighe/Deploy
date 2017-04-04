using System.Collections.Generic;
using Deploy.DAL;

namespace Deploy.ViewModel
{
    public class TenantDeploysViewModel
    {
        public int TennantID { get; set; }
        public string TennantName { get; set; }
        public string DeployName { get; set; }
        public int DeployTypeID { get; set; }
        public string DeploySaved { get; set; }
        public string  DeployState { get; set; }
        public string DeployResult { get; set; }
        public string AzureDeployName { get; set; }
        public string AzureTennantID { get; set; }
        public string AzureSubscriptionID { get; set; }
        public string ResourceGroupName { get; set; }
        public string AzureClientID { get; set; }
        public string AzureClientSecret { get; set; }
        public string ResourceGroupLocation { get; set; }
        public int DeployCount { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public virtual List<DeployType> DeployTypes { get; set; }
        public virtual List<Tennant> Tennants { get; set; }
        public virtual List <TennantParam> TennantParams { get; set; }
    }
}
