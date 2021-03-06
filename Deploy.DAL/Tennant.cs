﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Deploy.DAL
{
    public class Tennant
    {
        public int TennantID { get; set; }

        [Required]
        [Display(Name = "Tennant Name")]
        public string TennantName { get; set; }
        public string AzureTennantID { get; set; }
        public string AzureSubscriptionID { get; set; }
        public string ResourceGroupName { get; set; }
        public string AzureClientID { get; set; }
        public string AzureClientSecret { get; set; }
        [Required]
        public string ResourceGroupLocation { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        public virtual IList<DeployType> DeployTypes { get; set; }
    }
}
