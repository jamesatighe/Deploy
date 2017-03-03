using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Deploy.Models;

namespace Deploy.ViewModel
{
    public class TenantDeploys
    {
        public int TennantID { get; set; }
        public string TennantName { get; set; }
        public string DeployName { get; set; }
        public int DeployTypeID { get; set; }

        public int DeployCount { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }

        public virtual List<DeployType> DeployTypes { get; set; }
        public virtual List<Tennant> Tennants { get; set; }
    }
}
