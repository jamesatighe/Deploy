using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Deploy.Models
{
    public class TennantParam
    {
        public int TennantParamID { get; set; }
        public int DeployTypeID { get; set; }
        public int DeployParamID { get; set; }
        public string ParamValue { get; set; }
        public string ParamName { get; set; }
        public string ParamType { get; set; }
        public string ParamToolTip { get; set; }

        public List<DeployParam> DeployParams { get; set; }

    }
}
