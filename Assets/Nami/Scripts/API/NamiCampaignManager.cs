using System;
using System.Collections.Generic;
using NamiSdk.Implementation;
using NamiSdk.Interfaces;

namespace NamiSdk
{
    public static class NamiCampaignManager
    {
        private static readonly INamiCampaignManager Impl;

        static NamiCampaignManager()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            Impl = new NamiCampaignManagerUnityEditor();
#elif UNITY_ANDROID
            Impl = new NamiCampaignManagerAndroid();
#elif UNITY_IOS
			Impl = new NamiCampaignManagerIOS();
#endif
        }

        public static void Launch(string label, Action<NamiPaywallAction, NamiSKU> paywallActionCallback = null, Action onLaunchSuccessCallback = null, Action<LaunchCampaignError> onLaunchFailureCallback = null, Action<NamiPurchaseState, List<NamiPurchase>, string> onLaunchPurchaseChangedCallback = null)
        {
            Impl.Launch(label, paywallActionCallback, onLaunchSuccessCallback, onLaunchFailureCallback, onLaunchPurchaseChangedCallback);
        }
    }
}