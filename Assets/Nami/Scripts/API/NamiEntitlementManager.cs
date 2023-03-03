using System;
using System.Collections.Generic;
using NamiSDK.Implementation;
using NamiSDK.Interfaces;

namespace NamiSDK
{
    public static class NamiEntitlementManager
    {
        private static readonly INamiEntitlementManager Impl;

        static NamiEntitlementManager()
        {
#if UNITY_EDITOR
            Impl = new NamiEntitlementManagerUnityEditor();
#elif UNITY_ANDROID
            Impl = new NamiEntitlementManagerAndroid();
#elif UNITY_IOS
			Impl = new NamiEntitlementManagerIOS();
#endif
        }
        
        public static List<NamiEntitlement> Active()
        {
            return Impl.Active();
        }

        public static bool IsEntitlementActive(string referenceId)
        {
            return Impl.IsEntitlementActive(referenceId);
        }

        public static void Refresh(Action<List<NamiEntitlement>> refreshCallback)
        { 
            Impl.Refresh(refreshCallback);
        }

        public static void RegisterActiveEntitlementsHandler(Action<List<NamiEntitlement>> activeEntitlementsCallback)
        {
            Impl.RegisterActiveEntitlementsHandler(activeEntitlementsCallback);
        }
    }
}