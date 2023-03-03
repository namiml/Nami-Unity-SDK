#if UNITY_IOS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using NamiSDK.Interfaces;
using NamiSDK.MiniJSON;

namespace NamiSDK.Implementation
{
    public class NamiCustomerManagerIOS : INamiCustomerManager
    {
        public bool IsLoggedIn => _nm_isLoggedIn();

        public CustomerJourneyState JourneyState
        {
            get
            {
                CustomerJourneyState journeyState = null;

                var data = _nm_journeyState();

                var dictionary = Json.DeserializeDictionary(data);
                if (dictionary != null)
                {
                    dictionary.TryGetValue("journeyState", out var journeyStateObject);
                    if (journeyStateObject != null) journeyState = new CustomerJourneyState(journeyStateObject);
                }

                return journeyState;
            }
        }

        public string LoggedInId => _nm_loggedInId();

        public void Login(string withId)
        {
            _nm_login(withId);
        }

        public void Logout()
        {
            _nm_logout();
        }

        public void RegisterAccountStateHandler(Action<AccountStateAction, bool, string> accountStateCallback)
        {
            if (accountStateCallback == null) return;
            _nm_registerAccountStateHandler(
                Callbacks.New(data =>
                {
                    AccountStateAction accountStateAction = default;
                    bool success = false;
                    string error = null;

                    var dictionary = Json.DeserializeDictionary(data);
                    if (dictionary != null)
                    {
                        dictionary.TryGetValue("accountStateAction", out var accountStateActionObject);
                        dictionary.TryGetValue("success", out var successObject);
                        dictionary.TryGetValue("error", out var errorObject);

                        if (accountStateActionObject != null) accountStateAction = (AccountStateAction)(long)accountStateActionObject;
                        if (successObject != null) success = (bool)successObject;
                        error = (string)errorObject;
                    }

                    accountStateCallback.Invoke(accountStateAction, success, error);
                }));
        }

        public void RegisterJourneyStateHandler(Action<CustomerJourneyState> journeyStateCallback)
        {
            if (journeyStateCallback == null) return;
            _nm_registerJourneyStateHandler(
                Callbacks.New(data =>
                {
                    CustomerJourneyState journeyState = null;

                    var dictionary = Json.DeserializeDictionary(data);
                    if (dictionary != null)
                    {
                        dictionary.TryGetValue("journeyState", out var journeyStateObject);
                        if (journeyStateObject != null) journeyState = new CustomerJourneyState(journeyStateObject);
                    }

                    journeyStateCallback.Invoke(journeyState);
                }));
        }

        [DllImport("__Internal")]
        private static extern bool _nm_isLoggedIn();

        [DllImport("__Internal")]
        private static extern string _nm_journeyState();

        [DllImport("__Internal")]
        private static extern string _nm_loggedInId();

        [DllImport("__Internal")]
        private static extern void _nm_login(string withId);

        [DllImport("__Internal")]
        private static extern void _nm_logout();

        [DllImport("__Internal")]
        private static extern void _nm_registerAccountStateHandler(IntPtr accountStateCallbackPtr);

        [DllImport("__Internal")]
        private static extern void _nm_registerJourneyStateHandler(IntPtr journeyStateCallbackPtr);
    }
}
#endif