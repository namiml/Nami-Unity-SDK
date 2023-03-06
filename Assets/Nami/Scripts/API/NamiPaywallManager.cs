using System;
using NamiSDK.Implementation;
using NamiSDK.Interfaces;

namespace NamiSDK
{
    public static class NamiPaywallManager
    {
        private static readonly INamiPaywallManager Impl;

        static NamiPaywallManager()
        {
#if UNITY_EDITOR
            Impl = new NamiPaywallManagerUnityEditor();
#elif UNITY_ANDROID
            Impl = new NamiPaywallManagerAndroid();
#elif UNITY_IOS
			Impl = new NamiPaywallManagerIOS();
#endif
        }

        public static void RegisterCloseHandler(Action closeCallback)
        {
            Impl.RegisterCloseHandler(closeCallback);
        }

        public static void RegisterSignInHandler(Action signInCallback)
        {
            Impl.RegisterSignInHandler(signInCallback);
        }

        public static void RegisterBuySkuHandler(Action<string> buySkuCallback)
        {
            Impl.RegisterBuySkuHandler(buySkuCallback);
        }

        public static void BuySkuComplete(string purchase, string skuRefId)
        {
            Impl.BuySkuComplete(purchase, skuRefId);
        }

        public static void Dismiss(bool animated = true, Action completionCallback = null)
        {
            Impl.Dismiss(animated, completionCallback);
        }
    }
}