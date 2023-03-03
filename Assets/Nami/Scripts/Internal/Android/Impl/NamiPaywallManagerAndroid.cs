#if UNITY_ANDROID
using System;
using NamiSDK.Interfaces;
using NamiSDK.Utils;
using NamiSDK.Proxy;
using UnityEngine;

namespace NamiSDK.Implementation
{
    public class NamiPaywallManagerAndroid : INamiPaywallManager
    {
        private readonly OnRegisterPaywallListenerProxy registerListenerProxy;

        public NamiPaywallManagerAndroid()
        {
            registerListenerProxy = new OnRegisterPaywallListenerProxy();
            JavaClassNames.NamiBridge.AJCCallStaticOnce("registerPaywallHandler", registerListenerProxy);
        }

        public void RegisterCloseHandler(Action closeCallback)
        {
            registerListenerProxy.closeCallback += closeCallback;
        }

        public void RegisterSignInHandler(Action signInCallback)
        {
            registerListenerProxy.signInCallback += signInCallback;
        }

        public void RegisterBuySkuHandler(Action<string> buySkuCallback)
        {
            registerListenerProxy.buySkuCallback += buySkuCallback;
        }

        public void BuySkuComplete(string purchase, string skuRefId)
        {
            // TODO implement Purchase variable
            // JavaClassNames.NamiPaywallManager.AJCCallStaticOnce("buySkuComplete", JniToolkitUtils.Activity, purchase, skuRefId);
        }

        public void Dismiss(bool animated, Action completionCallback)
        {
            Debug.LogError("You can use NamiPaywallManager.Dismiss only for iOS platforms.");
        }
    }
}
#endif