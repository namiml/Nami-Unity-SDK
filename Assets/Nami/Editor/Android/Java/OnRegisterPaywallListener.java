package com.namiml.unity;

public interface OnRegisterPaywallListener {
    void onRegisterCloseHandler();
    void onRegisterSignInHandler();
    void onRegisterBuySkuHandler(String skuRefId);
}
