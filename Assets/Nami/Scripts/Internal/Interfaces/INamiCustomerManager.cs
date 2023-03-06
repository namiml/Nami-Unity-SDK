using System;

namespace NamiSDK.Interfaces
{
    public interface INamiCustomerManager
    {
        public bool IsLoggedIn { get; }
        public CustomerJourneyState JourneyState { get; }
        public string LoggedInId { get; }
        public void Login(string withId);
        public void Logout();
        public void RegisterAccountStateHandler(Action<AccountStateAction, bool, string> accountStateCallback);
        public void RegisterJourneyStateHandler(Action<CustomerJourneyState> journeyStateCallback);
    }
}