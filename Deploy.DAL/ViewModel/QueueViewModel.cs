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

        public virtual List<Queue> Queues { get; set; }
    }
}
