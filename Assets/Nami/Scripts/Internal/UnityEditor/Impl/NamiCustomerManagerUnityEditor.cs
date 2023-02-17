using System;
using NamiSdk.Interfaces;

namespace NamiSdk.Implementation
{
    public class NamiCustomerManagerUnityEditor : INamiCustomerManager
    {
        // TODO Editor implementation

        public bool IsLoggedIn { get; }

        public CustomerJourneyState JourneyState { get; }

        public string LoggedInId { get; }

        public void Login(string withId)
        {
        }

        public void Logout()
        {
        }

        public void RegisterAccountStateHandler(Action<AccountStateAction, bool, string> accountStateCallback)
        {
        }

        public void RegisterJourneyStateHandler(Action<CustomerJourneyState> journeyStateCallback)
        {
        }
    }
}