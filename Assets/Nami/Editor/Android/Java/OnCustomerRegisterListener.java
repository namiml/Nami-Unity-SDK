package com.namiml.unity;

import com.namiml.NamiError;
import com.namiml.customer.AccountStateAction;
import com.namiml.customer.CustomerJourneyState;

public interface OnCustomerRegisterListener {
    void onRegisterAccountState(AccountStateAction accountStateAction, boolean success, String error);
    void onRegisterJourneyState(CustomerJourneyState journeyState);
}
