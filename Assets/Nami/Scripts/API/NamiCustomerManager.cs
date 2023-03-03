using System;
using NamiSDK.Implementation;
using NamiSDK.Interfaces;

namespace NamiSDK
{
    public static class NamiCustomerManager
    {
        private static readonly INamiCustomerManager Impl;

        static NamiCustomerManager()
        {
#if UNITY_EDITOR
            Impl = new NamiCustomerManagerUnityEditor();
#elif UNITY_ANDROID
            Impl = new NamiCustomerManagerAndroid();
#elif UNITY_IOS
			Impl = new NamiCustomerManagerIOS();
#endif
        }

        public static bool IsLoggedIn => Impl.IsLoggedIn;

        public static CustomerJourneyState JourneyState => Impl.JourneyState;

        public static string LoggedInId => Impl.LoggedInId;

        public static void Login(string withId)
        {
            Impl.Login(withId);
        }

        public static void Logout()
        {
            Impl.Logout();
        }

        public static void RegisterAccountStateHandler(Action<AccountStateAction, bool, string> accountStateCallback)
        {
            Impl.RegisterAccountStateHandler(accountStateCallback);
        }

        public static void RegisterJourneyStateHandler(Action<CustomerJourneyState> journeyStateCallback)
        {
            Impl.RegisterJourneyStateHandler(journeyStateCallback);
        }
    }
}