using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NamiSDK.Utils;
using UnityEngine;

namespace NamiSDK.Proxy
{
    public class OnRegisterActiveEntitlementsListenerProxy : AndroidJavaProxy
    {
        public Action<List<NamiEntitlement>> activeEntitlementsCallback;

        public OnRegisterActiveEntitlementsListenerProxy() : base("com.namiml.unity.OnRegisterActiveEntitlementsListener")
        {
        }

        [UsedImplicitly]
        void onActiveEntitlementsCallback(AndroidJavaObject activeEntitlements)
        {
            if (activeEntitlementsCallback == null) return;
            NamiHelper.Queue(() =>
            {
                activeEntitlementsCallback(activeEntitlements?.FromJavaList<AndroidJavaObject>().Select(ajo => new NamiEntitlement(ajo)).ToList());
            });
        }
    }
}