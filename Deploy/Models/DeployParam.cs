using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deploy.Models
{
    public class DeployParam
    {
        public int DeployParamID { get; set; }
        public string ParameterDeployType { get; set; }
        public string ParameterName { get; set; }
        public string ParameterType { get; set; }
    }
}
