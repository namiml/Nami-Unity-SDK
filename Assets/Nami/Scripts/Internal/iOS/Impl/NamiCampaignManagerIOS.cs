using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NamiSdk.Interfaces;
using NamiSdk.MiniJSON;

namespace NamiSdk.Implementation
{
    public class NamiCampaignManagerIOS : INamiCampaignManager
    {
        public void Launch(string label, LaunchHandler launchHandler = null, PaywallActionHandler paywallActionHandler = null)
        {
            var onLaunchCallback = launchHandler?.OnLaunchCallback;
            var onPaywallActionCallback = paywallActionHandler?.OnPaywallActionCallback;
            
            _nm_launch(label,
                onLaunchCallback == null ? IntPtr.Zero : Callbacks.New(data =>
                {
                    bool success = false;
                    string error = null;

                    if (Json.Deserialize(data) is Dictionary<string, object> jsonDictionary)
                    {
                        jsonDictionary.TryGetValue("success", out var successObject);
                        jsonDictionary.TryGetValue("error", out var errorObject);

                        if (successObject != null) success = (bool)successObject;
                        error = (string)errorObject;
                    }

                    onLaunchCallback.Invoke(success, error);
                }),
                onPaywallActionCallback == null ? IntPtr.Zero : Callbacks.New(data =>
                {
                    NamiPaywallAction action = default;
                    NamiSKU sku = null;
                    string error = null;
                    List<NamiPurchase> purchases = null;

                    if (Json.Deserialize(data) is Dictionary<string, object> jsonDictionary)
                    {
                        jsonDictionary.TryGetValue("action", out var actionObject);
                        jsonDictionary.TryGetValue("sku", out var skuObject);
                        jsonDictionary.TryGetValue("error", out var errorObject);
                        jsonDictionary.TryGetValue("purchases", out var purchasesObject);

                        if (actionObject != null) action = (NamiPaywallAction)actionObject;
                        if (skuObject != null) sku = new NamiSKU((string)skuObject);
                        error = (string)errorObject;
                        if (purchasesObject != null)
                        {
                            if (Json.Deserialize((string)purchasesObject) is List<string> jsonList)
                            {
                                purchases = new List<NamiPurchase>(jsonList.Count);
                                foreach (var purchaseJson in jsonList)
                                {
                                    var purchase = new NamiPurchase(purchaseJson);
                                    purchases.Add(purchase);
                                }
                            }
                        }
                    }

                    onPaywallActionCallback.Invoke(action, sku, error, purchases);
                }));
        }

        public List<NamiCampaign> AllCampaigns()
        {
            return default;
        }

        public void RegisterAvailableCampaignsHandler(Action<List<NamiCampaign>> availableCampaignsCallback)
        {
        }

        [DllImport("__Internal")]
        private static extern void _nm_launch(string label, IntPtr launchCallbackPtr, IntPtr paywallActionCallbackPtr);
    }
}