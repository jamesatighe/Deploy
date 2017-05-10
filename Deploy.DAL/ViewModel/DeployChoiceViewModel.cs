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
        public string DeployCode { get; set; }
        public int TennantID { get; set; }
        public string TennantName { get; set; }

        public string ResourceGroupName { get; set; }

        public string DeploySaved { get; set; }
        public string DeployFile { get; set; }
        public string ParamsFile { get; set; }
        public List<SelectListItem> DeployNames { get; private set; }
        public DeployChoiceViewModel()
        {
            var Networking = new SelectListGroup { Name = "Networking" };
            var VirtualMachines = new SelectListGroup { Name = "Virtual Machines" };
            
            var Solutions = new SelectListGroup { Name = "Solutions" };

            DeployNames = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = "VPN",
                    Text = "Virtual Private Network",
                    Group = Networking
                },
                new SelectListItem
                {
                    Value = "VNET",
                    Text = "Virtual Network",
                    Group = Networking
                },
                new SelectListItem
                {
                    Value = "VirtualMachine",
                    Text = "Virtual Machine",
                    Group = VirtualMachines
                },
                new SelectListItem
                {
                    Value = "RDS",
                    Text = "RDS",
                    Group = VirtualMachines
                },
                new SelectListItem
                {
                    Value = "RDSTypeTec",
                    Text = "RDS TypeTec",
                    Group = VirtualMachines
                },
                new SelectListItem
                {
                    Value = "Identity",
                    Text = "Identity",
                    Group = VirtualMachines
                },
                new SelectListItem
                {
                    Value = "FileSrvTypeTec",
                    Text = "File Server TypeTec",
                    Group = VirtualMachines
                }

            };
        }

        public virtual List<Tennant> Tennants { get; set; }


    }
}
