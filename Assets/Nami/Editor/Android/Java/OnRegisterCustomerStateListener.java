package com.namiml.unity;

import com.namiml.customer.AccountStateAction;
import com.namiml.customer.CustomerJourneyState;

import java.lang.Boolean;

public interface OnRegisterCustomerStateListener {
    void onRegisterAccountState(AccountStateAction accountStateAction, Boolean success, String error);
    void onRegisterJourneyState(CustomerJourneyState journeyState);
}
