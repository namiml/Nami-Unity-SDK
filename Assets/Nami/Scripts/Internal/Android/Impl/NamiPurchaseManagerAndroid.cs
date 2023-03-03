#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using NamiSDK.Interfaces;
using NamiSDK.Utils;
using NamiSDK.Proxy;
using UnityEngine;

namespace NamiSDK.Implementation
{
    public class NamiPurchaseManagerAndroid : INamiPurchaseManager
    {
        private readonly OnRegisterPurchasesListenerProxy registerListenerProxy;

        public NamiPurchaseManagerAndroid()
        {
            registerListenerProxy = new OnRegisterPurchasesListenerProxy();
            JavaClassNames.NamiBridge.AJCCallStaticOnce("registerPurchasesHandler", registerListenerProxy);
        }

        public void ConsumePurchasedSku(string skuId)
        {
            JavaClassNames.NamiPurchaseManager.AJCCallStaticOnce("consumePurchasedSKU", skuId);
        }

        public void RegisterPurchasesChangedHandler(Action<List<NamiPurchase>, NamiPurchaseState, string> purchasesChangedCallback)
        {
            registerListenerProxy.purchasesChangedCallback += purchasesChangedCallback;
        }

        public bool IsSkuIdPurchased(string skuId)
        {
            return JavaClassNames.NamiPurchaseManager.AJCCallStaticOnce<bool>("isSKUIDPurchased", skuId);
        }

        public void PresentCodeRedemptionSheet()
        {
            Debug.LogError("You can use NamiPurchaseManager.PresentCodeRedemptionSheet only for iOS platforms.");
        }

        public void RegisterRestorePurchasesHandler(Action<RestorePurchasesState, List<NamiPurchase>, List<NamiPurchase>, string> restorePurchasesCallback)
        {
            Debug.LogError("You can use NamiPurchaseManager.RegisterRestorePurchasesHandler only for iOS platforms.");
        }

        public void RestorePurchases(Action<RestorePurchasesState, List<NamiPurchase>, List<NamiPurchase>, string> restorePurchasesCallback)
        {
            Debug.LogError("You can use NamiPurchaseManager.RestorePurchases only for iOS platforms.");
        }
    }
}
#endif