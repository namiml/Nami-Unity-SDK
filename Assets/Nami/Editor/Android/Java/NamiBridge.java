package com.namiml.unity;

import android.app.Activity;
import android.content.Context;
import android.util.Log;

import com.namiml.Nami;
import com.namiml.NamiConfiguration;
import com.namiml.NamiLogLevel;
import com.namiml.campaign.LaunchCampaignResult;
import com.namiml.campaign.NamiCampaignManager;
import com.namiml.paywall.model.NamiPaywallAction;

import java.lang.reflect.Field;
import java.util.ArrayList;
import java.util.List;

import kotlin.Unit;
import kotlin.jvm.functions.Function1;
import kotlin.jvm.functions.Function2;

public class NamiBridge {
    public static void setSettingsListHack(NamiConfiguration.Builder builder) {
        builder.logLevel(NamiLogLevel.DEBUG);
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

    public static void launch(Activity context, String label) {
        NamiCampaignManager.launch(context, label, new Function2<NamiPaywallAction, String, Unit>() {
            @Override
            public Unit invoke(NamiPaywallAction namiPaywallAction, String s) {
                return null;
            }
        }, new Function1<LaunchCampaignResult, Unit>() {
            @Override
            public Unit invoke(LaunchCampaignResult launchCampaignResult) {
                return null;
            }
        });
    }
}
