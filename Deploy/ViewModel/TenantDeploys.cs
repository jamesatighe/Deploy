using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Deploy.Models;
using System.ComponentModel.DataAnnotations;

namespace Deploy.ViewModel
{
    public class TenantDeploys
    {
        public int TennantID { get; set; }
        [Required(ErrorMessage = "You must enter a Tennant name")]
        [Display(Name = "Tennant Name")]
        public string TennantName { get; set; }
        public string DeployName { get; set; }
        public int DeployTypeID { get; set; }
        public string DeploySaved { get; set; }

        public int DeployCount { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set; }

        public virtual List<DeployType> DeployTypes { get; set; }
        public virtual List<Tennant> Tennants { get; set; }
    }
}
