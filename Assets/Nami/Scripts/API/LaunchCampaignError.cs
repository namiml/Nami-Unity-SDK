using System;

namespace NamiSdk
{
    [Serializable]
    public enum LaunchCampaignError
    {
        SDKNotInitialized,
        DefaultCampaignNotFound,
        LabeledCampaignNotFound,
        PaywallAlreadyDisplayed,
        CampaignDataNotFound
    }
}