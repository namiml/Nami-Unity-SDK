using System;
using System.Collections.Generic;
using NamiSdk.JNI;

namespace NamiSdk
{
    public class NamiCampaignManager
    {
        // TODO implement NamiError and NamiPurchase for onLaunchFailureCallback and onLaunchPurchaseChangedCallback
        public static void Launch(string label, Action<NamiPaywallAction> paywallActionCallback = null, Action onLaunchSuccessCallback = null, Action<string> onLaunchFailureCallback = null, Action<List<string>, NamiPurchaseState> onLaunchPurchaseChangedCallback = null)
        {
            JniToolkitUtils.RunOnUiThread(() =>
            {
                APIPath.NamiBridge.AJCCallStaticOnce("launch", JniToolkitUtils.Activity, label);
            });
        }
    }
}