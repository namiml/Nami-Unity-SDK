using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NamiSDK.Utils;
using UnityEngine;

namespace NamiSDK.Proxy
{
    public class OnRefreshEntitlementsListenerProxy : AndroidJavaProxy
    {
        private readonly Action<List<NamiEntitlement>> _refreshCallback;

        public OnRefreshEntitlementsListenerProxy(Action<List<NamiEntitlement>> refreshCallback) : base("com.namiml.unity.OnRefreshEntitlementsListener")
        {
            _refreshCallback = refreshCallback;
        }

        [UsedImplicitly]
        void onRefresh(AndroidJavaObject entitlementsList)
        {
            if (_refreshCallback == null) return;
            NamiHelper.Queue(() =>
            {
                _refreshCallback(entitlementsList?.FromJavaList<AndroidJavaObject>().Select(ajo => new NamiEntitlement(ajo)).ToList());
            });
        }
    }
}