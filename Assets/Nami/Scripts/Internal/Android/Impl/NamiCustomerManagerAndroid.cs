#if UNITY_ANDROID
using System;
using NamiSDK.Interfaces;
using NamiSDK.Utils;
using NamiSDK.Proxy;

namespace NamiSDK.Implementation
{
    public class NamiCustomerManagerAndroid : INamiCustomerManager
    {
        private readonly OnRegisterCustomerStateListenerProxy registerListenerProxy;

        public NamiCustomerManagerAndroid()
        {
            registerListenerProxy = new OnRegisterCustomerStateListenerProxy();
            JavaClassNames.NamiBridge.AJCCallStaticOnce("registerCustomerStateHandler", registerListenerProxy);
        }

        public bool IsLoggedIn => JavaClassNames.NamiCustomerManager.AJCCallStaticOnce<bool>("isLoggedIn");

        public CustomerJourneyState JourneyState
        {
            get
            {
                var ajo = JavaClassNames.NamiCustomerManager.AJCCallStaticOnceAJO("journeyState");
                return ajo == null ? null : new CustomerJourneyState(ajo);
            }
        }

        public string LoggedInId => JavaClassNames.NamiCustomerManager.AJCCallStaticOnce<string>("loggedInId");

        public void Login(string withId)
        {
            JavaClassNames.NamiCustomerManager.AJCCallStaticOnce("login", withId);
        }

        public void Logout()
        {
            JavaClassNames.NamiCustomerManager.AJCCallStaticOnce("logout");
        }

        public void RegisterAccountStateHandler(Action<AccountStateAction, bool, string> accountStateCallback)
        {
            registerListenerProxy.accountStateCallback += accountStateCallback;
        }

        public void RegisterJourneyStateHandler(Action<CustomerJourneyState> journeyStateCallback)
        {
            registerListenerProxy.journeyStateCallback += journeyStateCallback;
        }
    }
}
#endif