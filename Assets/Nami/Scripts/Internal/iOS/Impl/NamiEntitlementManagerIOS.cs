using System;
using System.Collections.Generic;
using NamiSdk.Interfaces;

namespace NamiSdk.Implementation
{
    public class NamiEntitlementManagerIOS : INamiEntitlementManager
    {
        // TODO iOS 

        public List<NamiEntitlement> Active()
        {
            return default;
        }

        public bool IsEntitlementActive(string referenceId)
        {
            return default;
        }

        public void Refresh(Action<List<NamiEntitlement>> refreshCallback)
        {
        }

        public void RegisterActiveEntitlementsHandler(Action<List<NamiEntitlement>> activeEntitlementsCallback)
        {
        }
    }
}