using System;
using System.Collections.Generic;

namespace NamiSDK
{
	public class LaunchHandler
	{
#if UNITY_IOS
		public Action<bool, string> OnLaunchCallback;

		public LaunchHandler(Action<bool, string> onLaunchCallback = null)
		{
			OnLaunchCallback = onLaunchCallback;
		}
#else
		public Action OnSuccessCallback;
		public Action<LaunchCampaignError> OnFailureCallback;
		public Action<NamiPurchaseState, List<NamiPurchase>, string> OnPurchaseChangedCallback;

		public LaunchHandler(Action onSuccessCallback = null, Action<LaunchCampaignError> onFailureCallback = null, Action<NamiPurchaseState, List<NamiPurchase>, string> onPurchaseChangedCallback = null)
		{
			OnSuccessCallback = onSuccessCallback;
			OnFailureCallback = onFailureCallback;
			OnPurchaseChangedCallback = onPurchaseChangedCallback;
		}
#endif
	}
}