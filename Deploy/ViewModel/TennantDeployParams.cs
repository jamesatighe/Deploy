﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Deploy.DAL;

namespace Deploy.ViewModel
{
    public class TennantDeployParams
    {
        public List<int> TennantParamID { get; set; }
        public List<int> DeployParamID { get; set; }
        [Display(Name = "Deployment Name")]
        public string DeployName { get; set; }
        public int DeployTypeID { get; set; }
        public string DeploySaved { get; set; }
        [Required(ErrorMessage = "All fields must be have values!")]
        public List<string> ParamValue { get; set; }
        public List<string> ParamName { get; set; }
        public List<string> ParamType { get; set; }
        public List<string> ParamToolTip { get; set; }
        public List<string> DefaultValue { get; set; }
        [Display(Name = "Tennant Name")]
        public string TennantName { get; set; }
        public int TennantID { get; set; }

        public virtual List<DeployParam> DeployParams { get; set; }
        public virtual List<TennantParam> TennantParams { get; set; }
        public virtual DeployType DeployTypes { get; set; }

   }
}



