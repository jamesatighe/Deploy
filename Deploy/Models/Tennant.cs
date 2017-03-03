using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Deploy.Models
{
    public class Tennant
    {
        public int TennantID { get; set; }

        [Required]
        [Display(Name = "Tennant Name")]
        public string TennantName { get; set; }
        [Required]
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
