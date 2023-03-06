#if UNITY_IOS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using NamiSDK.Interfaces;
using NamiSDK.MiniJSON;

namespace NamiSDK.Implementation
{
    public class NamiPurchaseManagerIOS : INamiPurchaseManager
    {
        public void ConsumePurchasedSku(string skuId)
        {
            _nm_consumePurchasedSku(skuId);
        }

        public void RegisterPurchasesChangedHandler(Action<List<NamiPurchase>, NamiPurchaseState, string> purchasesChangedCallback)
        {
            if (purchasesChangedCallback == null) return;
            _nm_registerPurchasesChangedHandler(
                Callbacks.New(data =>
                {
                    List<NamiPurchase> purchases = null;
                    NamiPurchaseState purchaseState = default;
                    string error = null;

                    var dictionary = Json.DeserializeDictionary(data);
                    if (dictionary != null)
                    {
                        dictionary.TryGetValue("purchases", out var purchasesObject);
                        dictionary.TryGetValue("purchaseState", out var purchaseStateObject);
                        dictionary.TryGetValue("error", out var errorObject);

                        purchases = Json.DeserializeList(purchasesObject)?.Select(jsonObject => new NamiPurchase(jsonObject)).ToList();
                        if (purchaseStateObject != null) purchaseState = (NamiPurchaseState)(long)purchaseStateObject;
                        error = (string)errorObject;
                    }

                    purchasesChangedCallback.Invoke(purchases, purchaseState, error);
                }));
        }

        public bool IsSkuIdPurchased(string skuId)
        {
            return _nm_isSkuIdPurchased(skuId);
        }

        public void PresentCodeRedemptionSheet()
        {
            _nm_presentCodeRedemptionSheet();
        }

        public void RegisterRestorePurchasesHandler(Action<RestorePurchasesState, List<NamiPurchase>, List<NamiPurchase>, string> restorePurchasesCallback)
        {
            if (restorePurchasesCallback == null) return;
            _nm_registerRestorePurchasesHandler(
                Callbacks.New(data =>
                {
                    RestorePurchasesState restorePurchaseState = default;
                    List<NamiPurchase> newPurchases = null;
                    List<NamiPurchase> oldPurchases = null;
                    string error = null;

                    var dictionary = Json.DeserializeDictionary(data);
                    if (dictionary != null)
                    {
                        dictionary.TryGetValue("restorePurchaseState", out var restorePurchaseStateObject);
                        dictionary.TryGetValue("newPurchases", out var newPurchasesObject);
                        dictionary.TryGetValue("oldPurchases", out var oldPurchasesObject);
                        dictionary.TryGetValue("error", out var errorObject);

                        if (restorePurchaseStateObject != null) restorePurchaseState = (RestorePurchasesState)(long)restorePurchaseStateObject;
                        newPurchases = Json.DeserializeList(newPurchasesObject)?.Select(jsonObject => new NamiPurchase(jsonObject)).ToList();
                        oldPurchases = Json.DeserializeList(oldPurchasesObject)?.Select(jsonObject => new NamiPurchase(jsonObject)).ToList();
                        error = (string)errorObject;
                    }

                    restorePurchasesCallback.Invoke(restorePurchaseState, newPurchases, oldPurchases, error);
                }));
        }

        public void RestorePurchases(Action<RestorePurchasesState, List<NamiPurchase>, List<NamiPurchase>, string> restorePurchasesCallback)
        {
            _nm_restorePurchases(
                restorePurchasesCallback == null ? IntPtr.Zero : Callbacks.New(data =>
                {
                    RestorePurchasesState restorePurchaseState = default;
                    List<NamiPurchase> newPurchases = null;
                    List<NamiPurchase> oldPurchases = null;
                    string error = null;

                    var dictionary = Json.DeserializeDictionary(data);
                    if (dictionary != null)
                    {
                        dictionary.TryGetValue("restorePurchaseState", out var restorePurchaseStateObject);
                        dictionary.TryGetValue("newPurchases", out var newPurchasesObject);
                        dictionary.TryGetValue("oldPurchases", out var oldPurchasesObject);
                        dictionary.TryGetValue("error", out var errorObject);

                        if (restorePurchaseStateObject != null) restorePurchaseState = (RestorePurchasesState)(long)restorePurchaseStateObject;
                        newPurchases = Json.DeserializeList(newPurchasesObject)?.Select(jsonObject => new NamiPurchase(jsonObject)).ToList();
                        oldPurchases = Json.DeserializeList(oldPurchasesObject)?.Select(jsonObject => new NamiPurchase(jsonObject)).ToList();
                        error = (string)errorObject;
                    }

                    restorePurchasesCallback.Invoke(restorePurchaseState, newPurchases, oldPurchases, error);
                }));
        }

        [DllImport("__Internal")]
        private static extern void _nm_consumePurchasedSku(string skuId);

        [DllImport("__Internal")]
        private static extern void _nm_registerPurchasesChangedHandler(IntPtr purchasesChangedCallbackPtr);

        [DllImport("__Internal")]
        private static extern bool _nm_isSkuIdPurchased(string skuId);

        [DllImport("__Internal")]
        private static extern void _nm_presentCodeRedemptionSheet();

        [DllImport("__Internal")]
        private static extern void _nm_registerRestorePurchasesHandler(IntPtr restorePurchasesCallbackPtr);

        [DllImport("__Internal")]
        private static extern void _nm_restorePurchases(IntPtr restorePurchasesCallbackPtr);
    }
}
#endif