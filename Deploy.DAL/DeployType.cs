using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deploy.DAL
{
    public class DeployType
    {
        public int DeployTypeID { get; set; }
        public int TennantID { get; set; }
        public string Description { get; set; }
        public string DeployName { get; set; }
        public string DeploySaved { get; set; }
        public string DeployState { get; set; }
        public string DeployResult { get; set; }
        public string AzureDeployName { get; set; }
        public string ResourceGroupName { get; set; }
        public string DeployFile { get; set; }
        public string ParamsFile { get; set; }
        public string DeployCode { get; set; }
        public virtual IList<TennantParam> TennantParams { get; set; }
        public virtual Tennant Tennants { get; set; }
    }
}
