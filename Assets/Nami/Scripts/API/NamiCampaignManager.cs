using System;
using System.Collections.Generic;
using NamiSdk.JNI;
using NamiSdk.Proxy;

namespace NamiSdk
{
	public static class NamiCampaignManager
	{
		static INamiCampaignManager Impl;

		static NamiCampaignManager()
		{
#if UNITY_ANDROID
			Impl = new NamiCampaignManagerAndroid();
#elif UNITY_IOS
			// TODo
#else
			// TODO editor
#endif
		}

		public static void Launch(string label, Action<NamiPaywallAction, string> paywallActionCallback = null, Action onLaunchSuccessCallback = null,
			Action<LaunchCampaignError> onLaunchFailureCallback = null, Action<NamiPurchaseState, List<NamiPurchase>, string> onLaunchPurchaseChangedCallback = null)
		{
			Impl.Launch(label, paywallActionCallback, onLaunchSuccessCallback, onLaunchFailureCallback, onLaunchPurchaseChangedCallback);
		}
	}
}