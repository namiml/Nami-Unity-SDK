using System;
using NamiSDK.Interfaces;

namespace NamiSDK.Implementation
{
    public class NamiPaywallManagerUnityEditor : INamiPaywallManager
    {
        // TODO Editor implementation

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

        public void Dismiss(bool animated, Action completionCallback)
        {
        }
    }
}