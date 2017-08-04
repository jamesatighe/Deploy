using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Deploy.DAL
{
    public class DeployList
    {
        public int DeployListID { get; set; }
        public string DeployName { get; set; }
        public string DeployValue { get; set; }
        public string  DeployType { get; set; }
    }
}
