using System;
using System.Collections.Generic;
using NamiSDK.Interfaces;

namespace NamiSDK.Implementation
{
    public class NamiEntitlementManagerUnityEditor : INamiEntitlementManager
    {
        // TODO Editor implementation

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