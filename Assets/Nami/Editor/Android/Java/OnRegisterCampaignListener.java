package com.namiml.unity;

import com.namiml.campaign.NamiCampaign;

import java.util.List;

public interface OnRegisterCampaignListener {
    void onRegisterAvailableCampaignsHandler(List<NamiCampaign> availableCampaigns);
}
