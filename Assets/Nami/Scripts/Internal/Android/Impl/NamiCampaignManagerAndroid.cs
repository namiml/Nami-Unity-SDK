using System;
using System.Collections.Generic;
using System.Linq;
using NamiSdk.Interfaces;
using NamiSdk.Utils;
using NamiSdk.Proxy;
using UnityEngine;

namespace NamiSdk.Implementation
{
    public class NamiCampaignManagerAndroid : INamiCampaignManager
    {
        private readonly OnRegisterCampaignListenerProxy registerListenerProxy;

        public NamiCampaignManagerAndroid()
        {
            registerListenerProxy = new OnRegisterCampaignListenerProxy();
            JavaClassNames.NamiBridge.AJCCallStaticOnce("registerCampaignHandler", registerListenerProxy);
        }

        public void Launch(string label, LaunchHandler launchHandler = null, PaywallActionHandler paywallActionHandler = null)
        {
            JniToolkitUtils.RunOnUiThread(() =>
            {
                // TODO Handlers Proxy Implementation
                //var launchListener = new OnLaunchCampaignListenerProxy(paywallActionCallback, onLaunchSuccessCallback, onLaunchFailureCallback, onLaunchPurchaseChangedCallback);
                //JavaClassNames.NamiBridge.AJCCallStaticOnce("launch", JniToolkitUtils.Activity, label, launchListener);
            });
        }

        public List<NamiCampaign> AllCampaigns()
        {
            var ajoList = JavaClassNames.NamiCampaignManager.AJOGetStaticAJO("INSTANCE").CallAJO("allCampaigns");
            return ajoList.FromJavaList<AndroidJavaObject>().Select(ajo => new NamiCampaign(ajo)).ToList();
        }

        public void RegisterAvailableCampaignsHandler(Action<List<NamiCampaign>> availableCampaignsCallback)
        {
            registerListenerProxy.availableCampaignsCallback += availableCampaignsCallback;
        }
    }
}