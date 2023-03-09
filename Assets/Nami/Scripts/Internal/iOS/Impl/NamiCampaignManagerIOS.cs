#if UNITY_IOS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using NamiSDK.Interfaces;
using NamiSDK.MiniJSON;

namespace NamiSDK.Implementation
{
    public class NamiCampaignManagerIOS : INamiCampaignManager
    {
        public void Launch()
        {
            _nm_launch();
        }

        public void Launch(string label, LaunchHandler launchHandler = null, PaywallActionHandler paywallActionHandler = null)
        {
            var onLaunchSuccessCallback = launchHandler?.OnSuccessCallback;
            var onLaunchFailureCallback = launchHandler?.OnFailureCallback;
            var onPaywallActionCallback = paywallActionHandler?.OnPaywallActionCallback;
            
            _nm_launchWithLabel(label,
                onLaunchSuccessCallback == null && onLaunchFailureCallback == null ? IntPtr.Zero : Callbacks.New(data =>
                {
                    bool success = false;
                    string error = null;

                    var dictionary = Json.DeserializeDictionary(data);
                    if (dictionary != null)
                    {
                        dictionary.TryGetValue("success", out var successObject);
                        dictionary.TryGetValue("error", out var errorObject);

                        if (successObject != null) success = (bool)successObject;
                        error = (string)errorObject;
                    }

                    if (success)
                    {
                        onLaunchSuccessCallback?.Invoke();
                    }
                    else
                    {
                        onLaunchFailureCallback?.Invoke(error);
                    }
                }),
                onPaywallActionCallback == null ? IntPtr.Zero : Callbacks.New(data =>
                {
                    NamiPaywallAction action = default;
                    NamiSKU sku = null;
                    string error = null;
                    List<NamiPurchase> purchases = null;

                    var dictionary = Json.DeserializeDictionary(data);
                    if (dictionary != null)
                    {
                        dictionary.TryGetValue("action", out var actionObject);
                        dictionary.TryGetValue("sku", out var skuObject);
                        dictionary.TryGetValue("error", out var errorObject);
                        dictionary.TryGetValue("purchases", out var purchasesObject);

                        if (actionObject != null) action = (NamiPaywallAction)(long)actionObject;
                        if (skuObject != null) sku = new NamiSKU(skuObject);
                        error = (string)errorObject;
                        if (purchasesObject != null)
                        {
                            var list = Json.DeserializeList(purchasesObject);
                            if (list != null)
                            {
                                purchases = list.Select(jsonObject => new NamiPurchase(jsonObject)).ToList();
                            }
                        }
                    }

                    onPaywallActionCallback.Invoke(action, sku, error, purchases);
                }));
        }

        public List<NamiCampaign> AllCampaigns()
        {
            List<NamiCampaign> campaigns = null;

            var data = _nm_allCampaigns();

            var dictionary = Json.DeserializeDictionary(data);
            if (dictionary != null)
            {
                dictionary.TryGetValue("campaigns", out var campaignsObject);
                campaigns = Json.DeserializeList(campaignsObject)?.Select(jsonObject => new NamiCampaign(jsonObject)).ToList();
            }

            return campaigns;
        }

        public void RegisterAvailableCampaignsHandler(Action<List<NamiCampaign>> availableCampaignsCallback)
        {
            if (availableCampaignsCallback == null) return;
            _nm_registerAvailableCampaignsHandler(
                Callbacks.New(data =>
                {
                    List<NamiCampaign> campaigns = null;

                    var dictionary = Json.DeserializeDictionary(data);
                    if (dictionary != null)
                    {
                        dictionary.TryGetValue("campaigns", out var campaignsObject);
                        campaigns = Json.DeserializeList(campaignsObject)?.Select(jsonObject => new NamiCampaign(jsonObject)).ToList();
                    }

                    availableCampaignsCallback.Invoke(campaigns);
                }));
        }

        [DllImport("__Internal")]
        private static extern void _nm_launch();

        [DllImport("__Internal")]
        private static extern void _nm_launchWithLabel(string label, IntPtr launchCallbackPtr, IntPtr paywallActionCallbackPtr);

        [DllImport("__Internal")]
        private static extern string _nm_allCampaigns();

        [DllImport("__Internal")]
        private static extern void _nm_registerAvailableCampaignsHandler(IntPtr availableCampaignsCallbackPtr);
    }
}
#endif