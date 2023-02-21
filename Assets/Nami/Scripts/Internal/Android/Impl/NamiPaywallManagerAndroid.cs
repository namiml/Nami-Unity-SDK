using System;
using NamiSdk.Interfaces;
using NamiSdk.JNI;
using NamiSdk.Proxy;

namespace NamiSdk.Implementation
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
    }
}