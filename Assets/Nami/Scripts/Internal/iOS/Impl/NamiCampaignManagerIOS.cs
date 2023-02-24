using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NamiSdk.Interfaces;
using UnityEngine;

namespace NamiSdk.Implementation
{
    public class NamiCampaignManagerIOS : INamiCampaignManager
    {
        public void Launch(string label, LaunchHandler launchHandler = null, PaywallActionHandler paywallActionHandler = null)
        {
            _nm_launch(label, Callbacks.New(data => Debug.Log("----------------> Success! : " + data)));
        }

        public List<NamiCampaign> AllCampaigns()
        {
            return default;
        }

        public void RegisterAvailableCampaignsHandler(Action<List<NamiCampaign>> availableCampaignsCallback)
        {
        }

        [DllImport("__Internal")]
        private static extern void _nm_launch(string label, Callbacks.StringCallback callback);
    }
}