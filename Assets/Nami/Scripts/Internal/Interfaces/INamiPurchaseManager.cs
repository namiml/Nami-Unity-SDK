using System;
using System.Collections.Generic;

namespace NamiSDK.Interfaces
{
	public interface INamiPurchaseManager
	{
		public void ConsumePurchasedSku(string skuId);
		public void RegisterPurchasesChangedHandler(Action<List<NamiPurchase>, NamiPurchaseState, string> purchasesChangedCallback);
		public bool IsSkuIdPurchased(string skuId);
		public void PresentCodeRedemptionSheet();
		public void RegisterRestorePurchasesHandler(Action<RestorePurchasesState, List<NamiPurchase>, List<NamiPurchase>, string> restorePurchasesCallback);
		public void RestorePurchases(Action<RestorePurchasesState, List<NamiPurchase>, List<NamiPurchase>, string> restorePurchasesCallback);
	}
}