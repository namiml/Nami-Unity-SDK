using System;
using System.Collections.Generic;

namespace NamiSDK
{
	public class LaunchHandler
	{
		public Action OnSuccessCallback;
		public Action<string> OnFailureCallback;
		public Action<NamiPurchaseState, List<NamiPurchase>, string> OnPurchaseChangedCallback;

		public LaunchHandler(Action onSuccessCallback = null, Action<string> onFailureCallback = null, Action<NamiPurchaseState, List<NamiPurchase>, string> onPurchaseChangedCallback = null)
		{
			OnSuccessCallback = onSuccessCallback;
			OnFailureCallback = onFailureCallback;
			OnPurchaseChangedCallback = onPurchaseChangedCallback;
		}
	}
}