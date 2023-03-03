using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NamiSDK.Utils;
using UnityEngine;

namespace NamiSDK.Proxy
{
    public class OnRegisterPurchasesListenerProxy : AndroidJavaProxy
    {
        public Action<List<NamiPurchase>, NamiPurchaseState, string> purchasesChangedCallback;

        public OnRegisterPurchasesListenerProxy() : base("com.namiml.unity.OnRegisterPurchasesListener")
        {
        }

        [UsedImplicitly]
        void onRegisterPurchasesChangedHandler(AndroidJavaObject purchases, AndroidJavaObject purchaseState, string error)
        {
            if (purchasesChangedCallback == null) return;
            NamiHelper.Queue(() =>
            {
                var purchasesList = purchases.FromJavaList<AndroidJavaObject>().Select(ajo => new NamiPurchase(ajo)).ToList();
                purchasesChangedCallback(purchasesList, purchaseState.JavaToEnum<NamiPurchaseState>(), error);
            });
        }
    }
}