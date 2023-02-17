package com.namiml.unity;

import android.app.Activity;
import android.util.Log;

import com.namiml.NamiConfiguration;
import com.namiml.NamiError;
import com.namiml.NamiLogLevel;
import com.namiml.campaign.LaunchCampaignResult;
import com.namiml.campaign.NamiCampaignManager;
import com.namiml.customer.NamiCustomerManager;
import com.namiml.paywall.model.NamiPaywallAction;

import java.lang.reflect.Field;
import java.util.ArrayList;
import java.util.List;

import kotlin.Unit;
import kotlin.jvm.functions.Function1;
import kotlin.jvm.functions.Function2;

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
        NamiCampaignManager.launch(context, label, new Function2<NamiPaywallAction, String, Unit>() {
            @Override
            public Unit invoke(NamiPaywallAction namiPaywallAction, String skuId) {
                Log.d("Unity", "JAVA: ----------------------------> namiPaywallAction Invoke: " + "NamiPaywallAction:" + namiPaywallAction.toString());
                launchListener.onNamiPaywallAction(namiPaywallAction, skuId);
                return null;
            }
        }, new Function1<LaunchCampaignResult, Unit>() {
            @Override
            public Unit invoke(LaunchCampaignResult launchCampaignResult) {
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
            }
        });
    }

    public static void addRegisterListener(OnCustomerRegisterListener registerListener) {
        Log.d("Unity", "JAVA: ----------------------------> register");
        NamiCustomerManager.registerAccountStateHandler((accountStateAction, success, namiError) -> {
            Log.d("Unity", "JAVA: ----------------------------> registerAccountState");
            registerListener.onRegisterAccountState(accountStateAction, success, namiError.getErrorMessage());
            return null;
        });
        NamiCustomerManager.registerJourneyStateHandler((journeyState) -> {
            Log.d("Unity", "JAVA: ----------------------------> registerJourneyState");
            registerListener.onRegisterJourneyState(journeyState);
            return null;
        });
    }
}
