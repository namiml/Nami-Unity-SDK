using System;
using JetBrains.Annotations;
using NamiSDK.Utils;
using UnityEngine;

namespace NamiSDK.Proxy
{
    public class OnRegisterCustomerStateListenerProxy : AndroidJavaProxy
    {
        public Action<AccountStateAction, bool, string> accountStateCallback;
        public Action<CustomerJourneyState> journeyStateCallback;

        public OnRegisterCustomerStateListenerProxy() : base("com.namiml.unity.OnRegisterCustomerStateListener")
        {
        }

        [UsedImplicitly]
        void onRegisterAccountState(AndroidJavaObject accountStateAction, bool success, string error)
        {
            if (accountStateCallback == null) return;
            NamiHelper.Queue(() =>
            {
                accountStateCallback(accountStateAction.JavaToEnum<AccountStateAction>(), success, error);
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