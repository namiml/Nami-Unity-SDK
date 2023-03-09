using System;
using System.Collections.Generic;

namespace NamiSDK
{
	public class PaywallActionHandler
	{
		public Action<NamiPaywallAction, NamiSKU, string, List<NamiPurchase>> OnPaywallActionCallback;

		public PaywallActionHandler(Action<NamiPaywallAction, NamiSKU, string, List<NamiPurchase>> onPaywallActionCallback)
		{
			OnPaywallActionCallback = onPaywallActionCallback;
		}
	}
}