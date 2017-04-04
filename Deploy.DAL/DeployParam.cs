using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deploy.DAL
{
    public class DeployParam
    {
        public int DeployParamID { get; set; }
        public string ParameterDeployType { get; set; }
        public string ParameterName { get; set; }
        public string ParameterType { get; set; }
        public string ParamToolTip { get; set; }
    }
}
