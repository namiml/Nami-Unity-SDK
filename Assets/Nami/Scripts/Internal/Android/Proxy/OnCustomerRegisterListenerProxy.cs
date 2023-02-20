using System;
using JetBrains.Annotations;
using NamiSdk.JNI;
using UnityEngine;

namespace NamiSdk.Proxy
{
    public class OnCustomerRegisterListenerProxy : AndroidJavaProxy
    {
        public Action<AccountStateAction, bool, string> accountStateCallback;
        public Action<CustomerJourneyState> journeyStateCallback;

        public OnCustomerRegisterListenerProxy() : base("com.namiml.unity.OnCustomerRegisterListener")
        {
        }

        [UsedImplicitly]
        void onRegisterAccountState(AndroidJavaObject accountStateAction, bool success, AndroidJavaObject error)
        {
            if (accountStateCallback == null) return;
            NamiHelper.Queue(() =>
            {
                accountStateCallback(accountStateAction.JavaToEnum<AccountStateAction>(), success, error.JavaToString());
            });
        }

        [UsedImplicitly]
        void onRegisterJourneyState(AndroidJavaObject journeyState)
        {
            if (journeyStateCallback == null) return;
            NamiHelper.Queue(() =>
            {
                journeyStateCallback(journeyState == null ? null : new CustomerJourneyState(journeyState));
            });
        }
    }
}