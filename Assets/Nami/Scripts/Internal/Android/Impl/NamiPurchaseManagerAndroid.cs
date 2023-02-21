using System;
using System.Collections.Generic;
using NamiSdk.Interfaces;
using NamiSdk.JNI;
using NamiSdk.Proxy;

namespace NamiSdk.Implementation
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
    }
}