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
                    object success = false, error = null;
                    if (Json.Deserialize(data) is Dictionary<string, object> jsonDictionary)
                    {
                        jsonDictionary.TryGetValue("success", out success);
                        jsonDictionary.TryGetValue("error", out error);
                    }
                    onLaunchCallback.Invoke((bool)success!, error?.ToString());
                }),
                onPaywallActionCallback == null ? IntPtr.Zero : Callbacks.New(data =>
                {
                    // TODO
                    /*
                    object action = 0, sku = null, error = null, purchases = null;
                    if (Json.Deserialize(data) is Dictionary<string, object> jsonDictionary)
                    {
                        jsonDictionary.TryGetValue("action", out action);
                        jsonDictionary.TryGetValue("sku", out sku);
                        jsonDictionary.TryGetValue("error", out error);
                        jsonDictionary.TryGetValue("purchases", out purchases);
                    }
                    onPaywallActionCallback.Invoke((NamiPaywallAction)action, sku, error?.ToString(), purchases);
                    */
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