using System;
using System.Collections.Generic;

namespace NamiSdk.Interfaces
{
	public interface INamiPurchaseManager
	{
		public void ConsumePurchasedSku(string skuId);
		public void RegisterPurchasesChangedHandler(Action<List<NamiPurchase>, NamiPurchaseState, string> purchasesChangedCallback);
		public bool IsSkuIdPurchased(string skuId);
	}
}