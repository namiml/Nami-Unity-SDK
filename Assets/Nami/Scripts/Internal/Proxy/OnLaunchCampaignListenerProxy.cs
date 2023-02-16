using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NamiSdk.Android;
using NamiSdk.JNI;
using UnityEngine;

namespace NamiSdk.Proxy
{
    public class OnLaunchCampaignListenerProxy : AndroidJavaProxy
    {
        // TODO implement NamiPurchase for the list in onPurchaseChanged function

        private readonly Action<NamiPaywallAction, string> _paywallActionCallback;
        private readonly Action _onLaunchSuccessCallback;
        private readonly Action<string> _onLaunchFailureCallback;
        private readonly Action<NamiPurchaseState, List<string>, string> _onLaunchPurchaseChangedCallback;

        public OnLaunchCampaignListenerProxy(Action<NamiPaywallAction, string> paywallActionCallback, Action onLaunchSuccessCallback, Action<string> onLaunchFailureCallback, Action<NamiPurchaseState, List<string>, string> onLaunchPurchaseChangedCallback) : base("com.namiml.unity.OnLaunchCampaignListener")
        {
            _paywallActionCallback = paywallActionCallback;
            _onLaunchSuccessCallback = onLaunchSuccessCallback;
            _onLaunchFailureCallback = onLaunchFailureCallback;
            _onLaunchPurchaseChangedCallback = onLaunchPurchaseChangedCallback;
        }

        [UsedImplicitly]
        void onNamiPaywallAction(AndroidJavaObject namiPaywallAction, AndroidJavaObject skuId)
        {
            if (_paywallActionCallback == null) return;
            NamiHelper.Queue(() =>
            {
                _paywallActionCallback(namiPaywallAction.JavaToEnum<NamiPaywallAction>("NAMI", "_"), skuId.JavaToString());
            });
        }

        [UsedImplicitly]
        void onSuccess()
        {
            if (_onLaunchSuccessCallback == null) return;
            NamiHelper.Queue(() =>
            {
                _onLaunchSuccessCallback();
            });
        }

        [UsedImplicitly]
        void onFailure(string error)
        {
            if (_onLaunchFailureCallback == null) return;
            NamiHelper.Queue(() =>
            {
                _onLaunchFailureCallback(error);
            });
        }

        [UsedImplicitly]
        void onPurchaseChanged(AndroidJavaObject purchaseState, /* List<NamiPurchase> */ AndroidJavaObject activePurchases, string errorMsg)
        {
            if (_onLaunchPurchaseChangedCallback == null) return;
            NamiHelper.Queue(() =>
            {
                _onLaunchPurchaseChangedCallback(purchaseState.JavaToEnum<NamiPurchaseState>(), null, errorMsg);
            });
        }
    }
}