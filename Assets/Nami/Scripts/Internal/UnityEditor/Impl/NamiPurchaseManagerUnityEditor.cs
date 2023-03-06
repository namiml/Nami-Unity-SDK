using System;
using System.Collections.Generic;
using NamiSDK.Interfaces;

namespace NamiSDK.Implementation
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

        public void PresentCodeRedemptionSheet()
        {
        }

        public void RegisterRestorePurchasesHandler(Action<RestorePurchasesState, List<NamiPurchase>, List<NamiPurchase>, string> restorePurchasesCallback)
        {
        }

        public void RestorePurchases(Action<RestorePurchasesState, List<NamiPurchase>, List<NamiPurchase>, string> restorePurchasesCallback)
        {
        }
    }
}