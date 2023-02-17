using System;
using System.Collections.Generic;

namespace NamiSdk.Interfaces
{
	public interface INamiCampaignManager
	{
		public void Launch(string label, Action<NamiPaywallAction, string> paywallActionCallback = null,
			Action onLaunchSuccessCallback = null, Action<LaunchCampaignError> onLaunchFailureCallback = null,
			Action<NamiPurchaseState, List<NamiPurchase>, string> onLaunchPurchaseChangedCallback = null);
	}
}