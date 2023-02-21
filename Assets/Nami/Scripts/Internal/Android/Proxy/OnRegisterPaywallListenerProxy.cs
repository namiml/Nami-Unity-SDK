using System;
using JetBrains.Annotations;
using NamiSdk.JNI;
using UnityEngine;

namespace NamiSdk.Proxy
{
    public class OnRegisterPaywallListenerProxy : AndroidJavaProxy
    {
        public Action closeCallback;
        public Action signInCallback;
        public Action<string> buySkuCallback;

        public OnRegisterPaywallListenerProxy() : base("com.namiml.unity.OnRegisterPaywallListener")
        {
        }

        [UsedImplicitly]
        void onRegisterCloseHandler()
        {
            if (closeCallback == null) return;
            NamiHelper.Queue(() =>
            {
                closeCallback();
            });
        }

        [UsedImplicitly]
        void onRegisterSignInHandler()
        {
            if (signInCallback == null) return;
            NamiHelper.Queue(() =>
            {
                signInCallback();
            });
        }

        [UsedImplicitly]
        void onRegisterBuySkuHandler(string skuRefId)
        {
            if (buySkuCallback == null) return;
            NamiHelper.Queue(() =>
            {
                buySkuCallback(skuRefId);
            });
        }
    }
}