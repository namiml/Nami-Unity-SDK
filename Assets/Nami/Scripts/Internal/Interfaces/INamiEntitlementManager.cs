using System;
using System.Collections.Generic;

namespace NamiSDK.Interfaces
{
    public interface INamiEntitlementManager
    {
        public List<NamiEntitlement> Active();
        public bool IsEntitlementActive(string referenceId);
        public void Refresh(Action<List<NamiEntitlement>> refreshCallback);
        public void RegisterActiveEntitlementsHandler(Action<List<NamiEntitlement>> activeEntitlementsCallback);
    }
}