﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Deploy.DAL
{
    public class TennantParam
    {
        public int TennantParamID { get; set; }
        public int DeployTypeID { get; set; }
        public int DeployParamID { get; set; }
        [Required(ErrorMessage = "All fields must be have values!")]
        public string ParamValue { get; set; }
        public string ParamName { get; set; }
        public string ParamType { get; set; }
        public string ParamToolTip { get; set; }

        public List<DeployParam> DeployParams { get; set; }
        public virtual DeployType DeployTypes { get; set; }

    }
}
