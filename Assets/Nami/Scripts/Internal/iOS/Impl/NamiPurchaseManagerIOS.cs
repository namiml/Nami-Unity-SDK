using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using NamiSdk.Interfaces;
using NamiSdk.MiniJSON;

namespace NamiSdk.Implementation
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

        [DllImport("__Internal")]
        private static extern void _nm_consumePurchasedSku(string skuId);

        [DllImport("__Internal")]
        private static extern void _nm_registerPurchasesChangedHandler(IntPtr purchasesChangedCallbackPtr);

        [DllImport("__Internal")]
        private static extern bool _nm_isSkuIdPurchased(string skuId);
    }
}