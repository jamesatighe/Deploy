using System;
using System.Collections.Generic;
using System.Text;

namespace Deploy.DAL
{
    public class Queue
    {
        public int QueueID { get; set; }
        public int TennantID { get; set; }
        public string status { get; set; }
        public string TennantName { get; set; }
        public int DeployTypeID { get; set; }
        public string DeployName { get; set; }
        public string subscriptionID { get; set; }
        public string resourcegroup { get; set; }
        public string azuredeploy { get; set; }
        public int Order { get; set; }
        public bool resource { get; set; }
        public virtual DeployType DeployTypes { get; set; }
    }
}
