using System;
using System.Collections.Generic;
using NamiSDK.Implementation;
using NamiSDK.Interfaces;

namespace NamiSDK
{
    public static class NamiPurchaseManager
    {
        private static readonly INamiPurchaseManager Impl;

        static NamiPurchaseManager()
        {
#if UNITY_EDITOR
            Impl = new NamiPurchaseManagerUnityEditor();
#elif UNITY_ANDROID
            Impl = new NamiPurchaseManagerAndroid();
#elif UNITY_IOS
			Impl = new NamiPurchaseManagerIOS();
#endif
        }

        public static void ConsumePurchasedSKU(string skuId)
        {
            Impl.ConsumePurchasedSku(skuId);
        }

        public static void RegisterPurchasesChangedHandler(Action<List<NamiPurchase>, NamiPurchaseState, string> purchasesChangedCallback)
        {
            Impl.RegisterPurchasesChangedHandler(purchasesChangedCallback);
        }

        public static bool IsSkuIdPurchased(string skuId)
        {
            return Impl.IsSkuIdPurchased(skuId);
        }

        public static void PresentCodeRedemptionSheet()
        {
            Impl.PresentCodeRedemptionSheet();
        }

        public static void RegisterRestorePurchasesHandler(Action<RestorePurchasesState, List<NamiPurchase>, List<NamiPurchase>, string> restorePurchasesCallback)
        {
            Impl.RegisterRestorePurchasesHandler(restorePurchasesCallback);
        }

        public static void RestorePurchases(Action<RestorePurchasesState, List<NamiPurchase>, List<NamiPurchase>, string> restorePurchasesCallback)
        {
            Impl.RestorePurchases(restorePurchasesCallback);
        }
    }
}