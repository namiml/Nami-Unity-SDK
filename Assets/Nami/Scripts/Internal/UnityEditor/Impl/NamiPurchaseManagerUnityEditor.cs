using System;
using System.Collections.Generic;
using NamiSdk.Interfaces;

namespace NamiSdk.Implementation
{
    public class NamiPurchaseManagerUnityEditor : INamiPurchaseManager
    {
        // TODO Editor implementation

        public void ConsumePurchasedSku(string skuId)
        {
        }

        public void RegisterPurchasesChangedHandler(Action<List<NamiPurchase>, NamiPurchaseState, string> purchasesChangedCallback)
        {
        }

        public bool IsSkuIdPurchased(string skuId)
        {
            return default;
        }
    }
}