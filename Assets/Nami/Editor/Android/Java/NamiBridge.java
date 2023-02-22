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
    public static void setSettingsListHack(NamiConfiguration.Builder builder) {
        List<String> settings = new ArrayList<>();
        settings.add("useStagingAPI");

        Field field = null;
        try {
            field = builder.getClass().getDeclaredField("settingsList");
            field.setAccessible(true);
            field.set(builder, settings);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public static void launch(Activity context, String label, OnLaunchCampaignListener launchListener) {
        Log.d("Unity", "JAVA: ----------------------------> launch");
        NamiCampaignManager.launch(context, label, (namiPaywallAction, sku) -> {
            Log.d("Unity", "JAVA: ----------------------------> namiPaywallAction Invoke: " + "NamiPaywallAction:" + namiPaywallAction.toString());
            launchListener.onNamiPaywallAction(namiPaywallAction, sku);
            return null;
        }, launchCampaignResult -> {
            Log.d("Unity", "JAVA: ----------------------------> launchCampaignResult Invoke");
            if (launchCampaignResult instanceof LaunchCampaignResult.Success) {
                Log.d("Unity", "JAVA: ----------------------------> onSuccess");
                launchListener.onSuccess();
            }
            else if (launchCampaignResult instanceof LaunchCampaignResult.Failure) {
                Log.d("Unity", "JAVA: ----------------------------> onFailure");
                launchListener.onFailure(((LaunchCampaignResult.Failure) launchCampaignResult).getError());
            } else if (launchCampaignResult instanceof LaunchCampaignResult.PurchaseChanged) {
                Log.d("Unity", "JAVA: ----------------------------> PurchaseChanged");
                LaunchCampaignResult.PurchaseChanged result = (LaunchCampaignResult.PurchaseChanged) launchCampaignResult;
                launchListener.onPurchaseChanged(result.getPurchaseState(), result.getActivePurchases(), result.getErrorMsg());
            }
            return null;
        });
    }

    public static void registerCampaignHandler(OnRegisterCampaignListener registerListener){
        NamiCampaignManager.INSTANCE.registerAvailableCampaignsHandler(availableCampaigns -> {
            Log.d("Unity", "JAVA: ----------------------------> registerAvailableCampaignsHandler");
            registerListener.onRegisterAvailableCampaignsHandler(availableCampaigns);
            return null;
        });
    }

    public static void registerCustomerStateHandler(OnRegisterCustomerStateListener registerListener) {
        NamiCustomerManager.registerAccountStateHandler((accountStateAction, success, namiError) -> {
            Log.d("Unity", "JAVA: ----------------------------> registerAccountState");
            registerListener.onRegisterAccountState(accountStateAction, success, namiError == null ? null : namiError.getErrorMessage());
            return null;
        });
        NamiCustomerManager.registerJourneyStateHandler(journeyState -> {
            Log.d("Unity", "JAVA: ----------------------------> registerJourneyState");
            registerListener.onRegisterJourneyState(journeyState);
            return null;
        });
    }

    public static void refresh(OnRefreshEntitlementsListener refreshListener){
        NamiEntitlementManager.refresh(entitlementsList -> {
            Log.d("Unity", "JAVA: ----------------------------> refresh");
            refreshListener.onRefresh(entitlementsList);
            return null;
        });
    }

    public static void registerActiveEntitlementsHandler(OnRegisterActiveEntitlementsListener registerListener){
        NamiEntitlementManager.registerActiveEntitlementsHandler(activeEntitlements -> {
            Log.d("Unity", "JAVA: ----------------------------> activeEntitlementsCallback");
            registerListener.onActiveEntitlementsCallback(activeEntitlements);
            return null;
        });
    }

    public static void registerPaywallHandler(OnRegisterPaywallListener registerListener){
        NamiPaywallManager.registerCloseHandler(paywallActivity -> {
            Log.d("Unity", "JAVA: ----------------------------> registerCloseHandler");
            registerListener.onRegisterCloseHandler();
            paywallActivity.finish();
            return null;
        });
        NamiPaywallManager.registerSignInHandler(context -> {
            Log.d("Unity", "JAVA: ----------------------------> registerSignInHandler");
            registerListener.onRegisterSignInHandler();
            return null;
        });
        NamiPaywallManager.registerBuySkuHandler((paywallActivity, skuRefId) -> {
            Log.d("Unity", "JAVA: ----------------------------> registerBuySkuHandler");
            registerListener.onRegisterBuySkuHandler(skuRefId);
            return null;
        });
    }

    public static void registerPurchasesHandler(OnRegisterPurchasesListener registerListener){
        NamiPurchaseManager.registerPurchasesChangedHandler((purchases, purchaseState, error) -> {
            Log.d("Unity", "JAVA: ----------------------------> registerPurchasesChangedHandler");
            registerListener.onRegisterPurchasesChangedHandler(purchases, purchaseState, error);
            return null;
        });
    }
}
