using System;
using System.Collections.Generic;
using System.Text;

namespace Deploy.DAL
{
    public class DeployChoices
    {
        public int DeployChoicesID { get; set; }
        public string BaseOption { get; set; }
        public bool Domain { get; set; }
        public bool Datadisk { get; set; }
        public string Size { get; set; }
        public bool Solution { get; set; }

        public string DeployFile { get; set; }
        public string ParamsFile { get; set; }
        public string DeployName { get; set; }
        public string DeployCode { get; set; }
    }
}
