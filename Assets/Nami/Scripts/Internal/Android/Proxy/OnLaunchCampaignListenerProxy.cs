using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NamiSDK.Utils;
using UnityEngine;

namespace NamiSDK.Proxy
{
    public class OnLaunchCampaignListenerProxy : AndroidJavaProxy
    {
        private readonly Action<NamiPaywallAction, NamiSKU> _paywallActionCallback;
        private readonly Action _onLaunchSuccessCallback;
        private readonly Action<LaunchCampaignError> _onLaunchFailureCallback;
        private readonly Action<NamiPurchaseState, List<NamiPurchase>, string> _onLaunchPurchaseChangedCallback;

        public OnLaunchCampaignListenerProxy(Action<NamiPaywallAction, NamiSKU> paywallActionCallback, Action onLaunchSuccessCallback, Action<LaunchCampaignError> onLaunchFailureCallback, Action<NamiPurchaseState, List<NamiPurchase>, string> onLaunchPurchaseChangedCallback) : base("com.namiml.unity.OnLaunchCampaignListener")
        {
            _paywallActionCallback = paywallActionCallback;
            _onLaunchSuccessCallback = onLaunchSuccessCallback;
            _onLaunchFailureCallback = onLaunchFailureCallback;
            _onLaunchPurchaseChangedCallback = onLaunchPurchaseChangedCallback;
        }

#if UNITY_ANDROID
        public OnLaunchCampaignListenerProxy(LaunchHandler launchHandler, PaywallActionHandler paywallActionHandler) : base("com.namiml.unity.OnLaunchCampaignListener")
        {
            if (launchHandler != null)
            {
                _onLaunchSuccessCallback = launchHandler.OnSuccessCallback;
                _onLaunchFailureCallback = launchHandler.OnFailureCallback;
                _onLaunchPurchaseChangedCallback = launchHandler.OnPurchaseChangedCallback;
            }
            if (paywallActionHandler != null)
            {
                _paywallActionCallback = paywallActionHandler.OnPaywallActionCallback;
            }
        }
#endif

        [UsedImplicitly]
        void onNamiPaywallAction(AndroidJavaObject namiPaywallAction, AndroidJavaObject sku)
        {
            if (_paywallActionCallback == null) return;
            NamiHelper.Queue(() =>
            {
                _paywallActionCallback(namiPaywallAction.JavaToEnum<NamiPaywallAction>("NAMI", "_"), sku == null ? null : new NamiSKU(sku));
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
                var activePurchasesList = activePurchases.FromJavaList<AndroidJavaObject>().Select(ajo => new NamiPurchase(ajo)).ToList();
                _onLaunchPurchaseChangedCallback(purchaseState.JavaToEnum<NamiPurchaseState>(), activePurchasesList, errorMsg);
            });
        }
    }
}