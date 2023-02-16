using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NamiSdk.JNI;
using UnityEngine;

namespace NamiSdk.Proxy
{
    public class OnLaunchCampaignListenerProxy : AndroidJavaProxy
    {
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
        void onNamiPaywallAction(/* NamiPaywallAction */ AndroidJavaObject namiPaywallAction, AndroidJavaObject skuId)
        {
            var convertedNamiPaywallAction = namiPaywallAction.JavaToEnum<NamiPaywallAction>();
            var convertedSkuId = skuId.JavaToString();
            Debug.Log("----------------------------> onNamiPaywallAction - " + "Enum: " + convertedNamiPaywallAction);
            NamiHelper.Queue(() =>
            {
                Debug.Log("----------------------------> onNamiPaywallAction : Queue");
                _paywallActionCallback(convertedNamiPaywallAction, convertedSkuId);
            });
        }

        [UsedImplicitly]
        void onSuccess()
        {
            Debug.Log("----------------------------> onSuccess");
            NamiHelper.Queue(() =>
            {
                Debug.Log("----------------------------> onSuccess : Queue");
                _onLaunchSuccessCallback();
            });
        }

        [UsedImplicitly]
        void onFailure(string error)
        {
            Debug.Log("----------------------------> onFailure");
            NamiHelper.Queue(() =>
            {
                Debug.Log("----------------------------> onFailure : Queue");
                _onLaunchFailureCallback(error);
            });
        }

        [UsedImplicitly]
        void onPurchaseChanged(/* NamiPurchaseState */ AndroidJavaObject purchaseState, /* List<NamiPurchase> */ AndroidJavaObject activePurchases, string errorMsg)
        {
            Debug.Log("----------------------------> onPurchaseChanged");
            NamiHelper.Queue(() =>
            {
                Debug.Log("----------------------------> onPurchaseChanged : Queue");
                //_onLaunchPurchaseChangedCallback(purchaseState, activePurchases, errorMsg);
            });
        }
    }
}