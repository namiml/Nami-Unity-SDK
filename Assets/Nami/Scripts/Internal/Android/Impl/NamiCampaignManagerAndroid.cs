using System;
using System.Collections;
using System.Collections.Generic;
using NamiSdk;
using NamiSdk.JNI;
using NamiSdk.Proxy;
using UnityEngine;

public class NamiCampaignManagerAndroid : INamiCampaignManager
{
	public void Launch(string label, Action<NamiPaywallAction, string> paywallActionCallback = null, Action onLaunchSuccessCallback = null, Action<LaunchCampaignError> onLaunchFailureCallback = null,
		Action<NamiPurchaseState, List<NamiPurchase>, string> onLaunchPurchaseChangedCallback = null)
	{
		JniToolkitUtils.RunOnUiThread(() =>
		{
			var launchListener = new OnLaunchCampaignListenerProxy(paywallActionCallback, onLaunchSuccessCallback, onLaunchFailureCallback, onLaunchPurchaseChangedCallback);
			JavaClassNames.NamiBridge.AJCCallStaticOnce("launch", JniToolkitUtils.Activity, label, launchListener);
		});

	}
}