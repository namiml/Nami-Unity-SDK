using System;

namespace NamiSdk.Interfaces
{
	public interface INamiPaywallManager
	{
		public void RegisterCloseHandler(Action closeCallback);
		public void RegisterSignInHandler(Action signInCallback);
		public void RegisterBuySkuHandler(Action<string> buySkuCallback);
		public void BuySkuComplete(string purchase, string skuRefId);
	}
}