using System;
using System.Runtime.InteropServices;
using NamiSdk.Interfaces;

namespace NamiSdk.Implementation
{
    public class NamiPaywallManagerIOS : INamiPaywallManager
    {
        public void RegisterCloseHandler(Action closeCallback)
        {
            if (closeCallback == null) return;
            _nm_registerCloseHandler(
                Callbacks.New(data =>
                {
                    closeCallback.Invoke();
                }));
        }

        public void RegisterSignInHandler(Action signInCallback)
        {
            if (signInCallback == null) return;
            _nm_registerSignInHandler(
                Callbacks.New(data =>
                {
                    signInCallback.Invoke();
                }));
        }

        public void RegisterBuySkuHandler(Action<string> buySkuCallback)
        {
            if (buySkuCallback == null) return;
            _nm_registerBuySkuHandler(
                Callbacks.New(data =>
                {
                    string skuRefId = data;
                    buySkuCallback.Invoke(skuRefId);
                }));
        }

        public void BuySkuComplete(string purchase, string skuRefId)
        {
            _nm_buySkuComplete(purchase, skuRefId);
        }

        [DllImport("__Internal")]
        private static extern void _nm_registerCloseHandler(IntPtr closeCallbackPtr);

        [DllImport("__Internal")]
        private static extern void _nm_registerSignInHandler(IntPtr signInCallbackPtr);

        [DllImport("__Internal")]
        private static extern void _nm_registerBuySkuHandler(IntPtr buySkuCallbackPtr);

        [DllImport("__Internal")]
        private static extern void _nm_buySkuComplete(string purchase, string skuRefId);
    }
}