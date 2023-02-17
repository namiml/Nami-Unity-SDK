using System;
using System.Collections.Generic;
using NamiSdk.Interfaces;

namespace NamiSdk
{
    public class NamiCampaignManagerIOS : INamiCampaignManager
    {
        public void Launch(string label, Action<NamiPaywallAction, string> paywallActionCallback = null, Action onLaunchSuccessCallback = null, Action<LaunchCampaignError> onLaunchFailureCallback = null, Action<NamiPurchaseState, List<NamiPurchase>, string> onLaunchPurchaseChangedCallback = null)
        {
            // TODO iOS implementation
        }
    }
}