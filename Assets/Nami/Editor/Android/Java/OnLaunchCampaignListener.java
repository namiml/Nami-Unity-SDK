package com.namiml.unity;

import com.namiml.billing.NamiPurchase;
import com.namiml.billing.NamiPurchaseState;
import com.namiml.paywall.model.NamiPaywallAction;

import java.util.List;

public interface OnLaunchCampaignListener {
    void onNamiPaywallAction(NamiPaywallAction namiPaywallAction, String skuId);
    void onSuccess();
    void onFailure(String error);
    void onPurchaseChanged(NamiPurchaseState purchaseState, List<NamiPurchase> activePurchases, String errorMsg);
}
