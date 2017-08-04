using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Deploy.DAL;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Deploy.ViewModel
{
    public class DeployChoiceViewModel
    {
        public int DeployChoicesID { get; set; }
        public string BaseOption { get; set; }
        public bool Domain { get; set; }
        public bool Datadisk { get; set; }
        public string Size { get; set; }
        public bool Solution { get; set; }
        public string DeployName { get; set; }
        public string Description { get; set; }
        public string DeployCode { get; set; }
        public int TennantID { get; set; }
        public string TennantName { get; set; }

        public string ResourceGroupName { get; set; }

        public string DeploySaved { get; set; }
        public string DeployFile { get; set; }
        public string ParamsFile { get; set; }
        public virtual List<Tennant> Tennants { get; set; }
        public virtual List<DeployList> DeployList { get; set; }
        public List<SelectListItem> DeployListItem { get; set; }

    }
}
