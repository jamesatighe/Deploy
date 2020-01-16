using System.Collections.Generic;
using Deploy.DAL;


namespace Deploy.ViewModel
{
    public class QueueViewModel
    {
        public int QueueID { get; set; }

        public virtual List<Queue> Queues { get; set; }
    }
}
