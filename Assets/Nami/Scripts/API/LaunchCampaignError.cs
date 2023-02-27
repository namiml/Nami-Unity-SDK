using System;

namespace NamiSdk
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