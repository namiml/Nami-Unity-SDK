package com.namiml.unity;

import com.namiml.entitlement.NamiEntitlement;

import java.util.List;

public interface OnRefreshEntitlementsListener {
    void onRefresh(List<NamiEntitlement> entitlementsList);
}
