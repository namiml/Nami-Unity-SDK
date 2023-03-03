using System;
using System.Collections.Generic;
using NamiSDK.Implementation;
using NamiSDK.Interfaces;

namespace NamiSDK
{
    public static class NamiCampaignManager
    {
        private static readonly INamiCampaignManager Impl;

        static NamiCampaignManager()
        {
#if UNITY_EDITOR
            Impl = new NamiCampaignManagerUnityEditor();
#elif UNITY_ANDROID
            Impl = new NamiCampaignManagerAndroid();
#elif UNITY_IOS
			Impl = new NamiCampaignManagerIOS();
#endif
        }

        public static void Launch(string label, LaunchHandler launchHandler = null, PaywallActionHandler paywallActionHandler = null)
        {
            Impl.Launch(label, launchHandler, paywallActionHandler);
        }

        public static List<NamiCampaign> AllCampaigns()
        {
            return Impl.AllCampaigns();
        }

        public static void RegisterAvailableCampaignsHandler(Action<List<NamiCampaign>> availableCampaignsCallback)
        {
            Impl.RegisterAvailableCampaignsHandler(availableCampaignsCallback);
        }
    }
}