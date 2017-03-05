using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deploy.Models
{
    public class DeployType
    {
        public int DeployTypeID { get; set; }
        public int TennantID { get; set; }
        public string DeployName { get; set; }
        public string DeploySaved { get; set; }

        public virtual IList<TennantParam> TennantParams { get; set; }
        public virtual Tennant Tennants { get; set; }
    }
}
