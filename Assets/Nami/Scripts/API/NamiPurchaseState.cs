using System;

namespace NamiSdk
{
    [Serializable]
    public enum NamiPurchaseState
    {
        Pending,
        Purchased,
        Consumed,
        Resubscribed,
        Unsubscribed,
        Deferred,
        Failed,
        Cancelled,
        Unknown
    }
}