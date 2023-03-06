using System;

namespace NamiSDK
{
    [Serializable]
    public enum LaunchCampaignError
    {
        DefaultCampaignNotFound,
        LabeledCampaignNotFound,
        CampaignDataNotFound,
        PaywallAlreadyDisplayed,
        SDKNotInitialized
    }
}