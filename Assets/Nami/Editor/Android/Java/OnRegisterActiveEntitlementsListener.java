package com.namiml.unity;

import com.namiml.entitlement.NamiEntitlement;

import java.util.List;

public interface OnRegisterActiveEntitlementsListener {
    void onActiveEntitlementsCallback(List<NamiEntitlement> activeEntitlements);
}
