using System;
using NamiSdk.Interfaces;

namespace NamiSdk.Implementation
{
    public class NamiPaywallManagerIOS : INamiPaywallManager
    {
        // TODO iOS

        public void RegisterCloseHandler(Action closeCallback)
        {
        }

        public void RegisterSignInHandler(Action signInCallback)
        {
        }

        public void RegisterBuySkuHandler(Action<string> buySkuCallback)
        {
        }

        public void BuySkuComplete(string purchase, string skuRefId)
        {
        }
    }
}