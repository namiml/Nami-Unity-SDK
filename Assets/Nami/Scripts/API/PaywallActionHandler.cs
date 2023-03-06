using System;
using System.Collections.Generic;

namespace NamiSDK
{
	public class PaywallActionHandler
	{
#if UNITY_IOS
		public Action<NamiPaywallAction, NamiSKU, string, List<NamiPurchase>> OnPaywallActionCallback;

		public PaywallActionHandler(Action<NamiPaywallAction, NamiSKU, string, List<NamiPurchase>> onPaywallActionCallback)
		{
			OnPaywallActionCallback = onPaywallActionCallback;
		}
#else
		public Action<NamiPaywallAction, NamiSKU> OnPaywallActionCallback;

		public PaywallActionHandler(Action<NamiPaywallAction, NamiSKU> onPaywallActionCallback)
		{
			OnPaywallActionCallback = onPaywallActionCallback;
		}
#endif
	}
}