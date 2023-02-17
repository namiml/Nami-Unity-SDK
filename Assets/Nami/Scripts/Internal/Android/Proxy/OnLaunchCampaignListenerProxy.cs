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
        private readonly Action<NamiPaywallAction, string> _paywallActionCallback;
        private readonly Action _onLaunchSuccessCallback;
        private readonly Action<LaunchCampaignError> _onLaunchFailureCallback;
        private readonly Action<NamiPurchaseState, List<NamiPurchase>, string> _onLaunchPurchaseChangedCallback;

        public OnLaunchCampaignListenerProxy(Action<NamiPaywallAction, string> paywallActionCallback, Action onLaunchSuccessCallback, Action<LaunchCampaignError> onLaunchFailureCallback, Action<NamiPurchaseState, List<NamiPurchase>, string> onLaunchPurchaseChangedCallback) : base("com.namiml.unity.OnLaunchCampaignListener")
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
        void onFailure(AndroidJavaObject error)
        {
            if (_onLaunchFailureCallback == null) return;
            NamiHelper.Queue(() =>
            {
                _onLaunchFailureCallback(error.JavaToEnum<LaunchCampaignError>("_"));
            });
        }

        [UsedImplicitly]
        void onPurchaseChanged(AndroidJavaObject purchaseState, AndroidJavaObject activePurchases, string errorMsg)
        {
            if (_onLaunchPurchaseChangedCallback == null) return;
            NamiHelper.Queue(() =>
            {
                _onLaunchPurchaseChangedCallback(purchaseState.JavaToEnum<NamiPurchaseState>(), NamiPurchase.ExtractFromJavaList(activePurchases), errorMsg);
            });
        }
    }
}