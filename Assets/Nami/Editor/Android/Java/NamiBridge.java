package com.namiml.unity;

import android.app.Activity;
import android.util.Log;

import com.namiml.NamiConfiguration;
import com.namiml.billing.NamiPurchaseManager;
import com.namiml.campaign.LaunchCampaignResult;
import com.namiml.campaign.NamiCampaignManager;
import com.namiml.customer.NamiCustomerManager;
import com.namiml.entitlement.NamiEntitlementManager;
import com.namiml.paywall.NamiPaywallManager;

import java.lang.reflect.Field;
import java.util.ArrayList;
import java.util.List;

public class NamiBridge {
    public static void settingsListHack(NamiConfiguration.Builder builder, List<String> settingsList) {
        try {
            Field field = builder.getClass().getDeclaredField("settingsList");
            field.setAccessible(true);
            field.set(builder, settingsList);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public static void launch(Activity context, String label, OnLaunchCampaignListener launchListener) {
        NamiCampaignManager.launch(context, label, (namiPaywallAction, sku) -> {
            launchListener.onNamiPaywallAction(namiPaywallAction, sku);
            return null;
        }, launchCampaignResult -> {
            if (launchCampaignResult instanceof LaunchCampaignResult.Success) {
                launchListener.onSuccess();
            }
            else if (launchCampaignResult instanceof LaunchCampaignResult.Failure) {
                launchListener.onFailure(((LaunchCampaignResult.Failure) launchCampaignResult).getError());
            } else if (launchCampaignResult instanceof LaunchCampaignResult.PurchaseChanged) {
                LaunchCampaignResult.PurchaseChanged result = (LaunchCampaignResult.PurchaseChanged) launchCampaignResult;
                launchListener.onPurchaseChanged(result.getPurchaseState(), result.getActivePurchases(), result.getErrorMsg());
            }
            return null;
        });
    }

    public static void registerCampaignHandler(OnRegisterCampaignListener registerListener){
        NamiCampaignManager.INSTANCE.registerAvailableCampaignsHandler(availableCampaigns -> {
            registerListener.onRegisterAvailableCampaignsHandler(availableCampaigns);
            return null;
        });
    }

    public static void registerCustomerStateHandler(OnRegisterCustomerStateListener registerListener) {
        NamiCustomerManager.registerAccountStateHandler((accountStateAction, success, namiError) -> {
            String error = namiError == null ? null : namiError.getErrorMessage();
            registerListener.onRegisterAccountState(accountStateAction, success, error == null ? "" : error);
            return null;
        });
        NamiCustomerManager.registerJourneyStateHandler(journeyState -> {
            registerListener.onRegisterJourneyState(journeyState);
            return null;
        });
    }

    public static void refresh(OnRefreshEntitlementsListener refreshListener){
        NamiEntitlementManager.refresh(entitlementsList -> {
            refreshListener.onRefresh(entitlementsList);
            return null;
        });
    }

    public static void registerActiveEntitlementsHandler(OnRegisterActiveEntitlementsListener registerListener){
        NamiEntitlementManager.registerActiveEntitlementsHandler(activeEntitlements -> {
            registerListener.onActiveEntitlementsCallback(activeEntitlements);
            return null;
        });
    }

    public static void registerPaywallHandler(OnRegisterPaywallListener registerListener){
        NamiPaywallManager.registerCloseHandler(paywallActivity -> {
            registerListener.onRegisterCloseHandler();
            paywallActivity.finish();
            return null;
        });
        NamiPaywallManager.registerSignInHandler(context -> {
            registerListener.onRegisterSignInHandler();
            return null;
        });
        NamiPaywallManager.registerBuySkuHandler((paywallActivity, skuRefId) -> {
            registerListener.onRegisterBuySkuHandler(skuRefId);
            return null;
        });
    }

    public static void registerPurchasesHandler(OnRegisterPurchasesListener registerListener){
        NamiPurchaseManager.registerPurchasesChangedHandler((purchases, purchaseState, error) -> {
            registerListener.onRegisterPurchasesChangedHandler(purchases, purchaseState, error);
            return null;
        });
    }
}
