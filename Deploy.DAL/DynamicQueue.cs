using Hangfire.Common;
using Hangfire.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class DynamicQueueAttribute : JobFilterAttribute, IElectStateFilter
{
    public DynamicQueueAttribute(string paramQueue)
    {
        ParamQueue = paramQueue;
    }

    public string ParamQueue { get; }

    public void OnStateElection(ElectStateContext context)
    {
        var enqueuedState = context.CandidateState as EnqueuedState;
        if (enqueuedState != null)
        {
            enqueuedState.Queue = ParamQueue;
        }
    }
}
