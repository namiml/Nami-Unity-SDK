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
        private readonly Action<NamiPaywallAction, NamiSKU, string, List<NamiPurchase>> _paywallActionCallback;
        private readonly Action _onLaunchSuccessCallback;
        private readonly Action<string> _onLaunchFailureCallback;
        private readonly Action<NamiPurchaseState, List<NamiPurchase>, string> _onLaunchPurchaseChangedCallback;

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

        [UsedImplicitly]
        void onNamiPaywallAction(AndroidJavaObject namiPaywallAction, AndroidJavaObject sku)
        {
            if (_paywallActionCallback == null) return;
            NamiHelper.Queue(() =>
            {
                _paywallActionCallback(namiPaywallAction.JavaToEnum<NamiPaywallAction>("NAMI", "_"), sku == null ? null : new NamiSKU(sku), null, null);
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
                _onLaunchFailureCallback(error.JavaToString());
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