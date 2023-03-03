using System;
using System.Collections.Generic;
using NamiSDK.Interfaces;

namespace NamiSDK.Implementation
{
    public class NamiCampaignManagerUnityEditor : INamiCampaignManager
    {
        // TODO Editor implementation

        public void Launch(string label, LaunchHandler launchHandler = null, PaywallActionHandler paywallActionHandler = null)
        {
        }

        public List<NamiCampaign> AllCampaigns()
        {
            return default;
        }

        public void RegisterAvailableCampaignsHandler(Action<List<NamiCampaign>> availableCampaignsCallback)
        {
        }
    }
}