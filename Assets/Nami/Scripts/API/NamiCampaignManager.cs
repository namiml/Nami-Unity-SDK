using System;
using System.Collections.Generic;
using NamiSdk.JNI;
using NamiSdk.Proxy;

namespace NamiSdk
{
    public class NamiCampaignManager
    {
        public static void Launch(string label, Action<NamiPaywallAction, string> paywallActionCallback = null, Action onLaunchSuccessCallback = null, Action<string> onLaunchFailureCallback = null, Action<NamiPurchaseState, List<string>, string> onLaunchPurchaseChangedCallback = null)
        {
            JniToolkitUtils.RunOnUiThread(() =>
            {
                var launchListener = new OnLaunchCampaignListenerProxy(paywallActionCallback, onLaunchSuccessCallback, onLaunchFailureCallback, onLaunchPurchaseChangedCallback);
                JavaClassNames.NamiBridge.AJCCallStaticOnce("launch", JniToolkitUtils.Activity, label, launchListener);
            });
        }
    }
}