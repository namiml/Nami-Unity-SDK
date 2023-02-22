using System;
using System.Collections.Generic;
using System.Linq;
using NamiSdk.Interfaces;
using NamiSdk.Utils;
using NamiSdk.Proxy;
using UnityEngine;

namespace NamiSdk.Implementation
{
    public class NamiEntitlementManagerAndroid : INamiEntitlementManager
    {
        private readonly OnRegisterActiveEntitlementsListenerProxy registerListenerProxy;

        public NamiEntitlementManagerAndroid()
        {
            registerListenerProxy = new OnRegisterActiveEntitlementsListenerProxy();
            JavaClassNames.NamiBridge.AJCCallStaticOnce("registerActiveEntitlementsHandler", registerListenerProxy);
        }

        public List<NamiEntitlement> Active()
        {
            var ajoList = JavaClassNames.NamiEntitlementManager.AJCCallStaticOnceAJO("active");
            return ajoList.FromJavaList<AndroidJavaObject>().Select(ajo => new NamiEntitlement(ajo)).ToList();
        }

        public bool IsEntitlementActive(string referenceId)
        {
            return JavaClassNames.NamiEntitlementManager.AJCCallStaticOnce<bool>("isEntitlementActive", referenceId);
        }

        public void Refresh(Action<List<NamiEntitlement>> refreshCallback)
        {
            var refreshListener = new OnRefreshEntitlementsListenerProxy(refreshCallback);
            JavaClassNames.NamiBridge.AJCCallStaticOnce("refresh", refreshListener);
        }

        public void RegisterActiveEntitlementsHandler(Action<List<NamiEntitlement>> activeEntitlementsCallback)
        {
            registerListenerProxy.activeEntitlementsCallback += activeEntitlementsCallback;
        }
    }
}