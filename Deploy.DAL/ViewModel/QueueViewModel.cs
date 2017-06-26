using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Deploy.DAL;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Deploy.ViewModel
{
    public class QueueViewModel
    {
        public int QueueID { get; set; }
        public string status { get; set; }
        public string TennantName { get; set; }
        public int TennantID { get; set; }
        public int DeployTypeID { get; set; }
        public string DeployName { get; set; }
        public string subscriptionID { get; set; }
        public string resourcegroup { get; set; }
        public string azuredeploy { get; set; }
        public string accesstoken { get; set; }
        public int Order { get; set; }
        public string jsondeploy { get; set; }
        public bool resource { get; set; }

        public virtual List<Queue> Queues { get; set; }
    }
}
