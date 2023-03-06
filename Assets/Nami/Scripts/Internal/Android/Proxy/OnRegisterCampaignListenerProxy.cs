using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NamiSDK.Utils;
using UnityEngine;

namespace NamiSDK.Proxy
{
    public class OnRegisterCampaignListenerProxy : AndroidJavaProxy
    {
        public Action<List<NamiCampaign>> availableCampaignsCallback;

        public OnRegisterCampaignListenerProxy() : base("com.namiml.unity.OnRegisterCampaignListener")
        {
        }

        [UsedImplicitly]
        void onRegisterAvailableCampaignsHandler(AndroidJavaObject availableCampaigns)
        {
            if (availableCampaignsCallback == null) return;
            NamiHelper.Queue(() =>
            {
                availableCampaignsCallback(availableCampaigns.FromJavaList<AndroidJavaObject>().Select(ajo => new NamiCampaign(ajo)).ToList());
            });
        }
    }
}