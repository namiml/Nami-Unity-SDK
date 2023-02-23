using System;
using System.Collections.Generic;

namespace NamiSdk
{
	public class LaunchHandler
	{
#if UNITY_IOS
		public Action OnSuccessCallback;
		public Action<string> OnFailureCallback;

		public LaunchHandler(Action onSuccessCallback = null, Action<string> onFailureCallback = null)
		{
			OnSuccessCallback = onSuccessCallback;
			OnFailureCallback = onFailureCallback;
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