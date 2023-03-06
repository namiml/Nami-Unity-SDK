package com.namiml.unity;

import com.namiml.NamiError;
import com.namiml.billing.NamiPurchase;
import com.namiml.billing.NamiPurchaseState;
import com.namiml.paywall.NamiSKU;
import com.namiml.paywall.model.NamiPaywallAction;

import java.util.List;

public interface OnLaunchCampaignListener {
    void onNamiPaywallAction(NamiPaywallAction namiPaywallAction, NamiSKU sku);
    void onSuccess();
    void onFailure(NamiError error);
    void onPurchaseChanged(NamiPurchaseState purchaseState, List<NamiPurchase> activePurchases, String errorMsg);
}
