package com.namiml.unity;

import com.namiml.billing.NamiPurchase;
import com.namiml.billing.NamiPurchaseState;

import java.util.List;

public interface OnRegisterPurchasesListener {
    void onRegisterPurchasesChangedHandler(List<NamiPurchase> purchases, NamiPurchaseState purchaseState, String error);
}
