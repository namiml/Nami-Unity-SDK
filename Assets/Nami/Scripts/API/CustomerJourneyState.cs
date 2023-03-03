using NamiSDK.MiniJSON;
using NamiSDK.Utils;
using UnityEngine;

namespace NamiSDK
{
    public class CustomerJourneyState
    {
        public CustomerJourneyState(AndroidJavaObject ajo)
        {
            FormerSubscriber = ajo.GetBool("formerSubscriber");
            InGracePeriod = ajo.GetBool("inGracePeriod");
            InTrialPeriod = ajo.GetBool("inTrialPeriod");
            InIntroOfferPeriod = ajo.GetBool("inIntroOfferPeriod");
            IsCancelled = ajo.GetBool("isCancelled");
            InPause = ajo.GetBool("inPause");
            InAccountHold = ajo.GetBool("inAccountHold");
        }
        
        public CustomerJourneyState(object json)
        {
            var dictionary = Json.DeserializeDictionary(json);
            if (dictionary != null)
            {
                dictionary.TryGetValue("formerSubscriber", out var formerSubscriberObject);
                dictionary.TryGetValue("inGracePeriod", out var inGracePeriodObject);
                dictionary.TryGetValue("inTrialPeriod", out var inTrialPeriodObject);
                dictionary.TryGetValue("inIntroOfferPeriod", out var inIntroOfferPeriodObject);
                dictionary.TryGetValue("isCancelled", out var isCancelledObject);
                dictionary.TryGetValue("inPause", out var inPauseObject);
                dictionary.TryGetValue("inAccountHold", out var inAccountHoldObject);

                if (formerSubscriberObject != null) FormerSubscriber = (bool)formerSubscriberObject;
                if (inGracePeriodObject != null) InGracePeriod = (bool)inGracePeriodObject;
                if (inTrialPeriodObject != null) InTrialPeriod = (bool)inTrialPeriodObject;
                if (inIntroOfferPeriodObject != null) InIntroOfferPeriod = (bool)inIntroOfferPeriodObject;
                if (isCancelledObject != null) IsCancelled = (bool)isCancelledObject;
                if (inPauseObject != null) InPause = (bool)inPauseObject;
                if (inAccountHoldObject != null) InAccountHold = (bool)inAccountHoldObject;
            }
        }

        public bool FormerSubscriber { get; private set; }

        public bool InGracePeriod { get; private set; }

        public bool InTrialPeriod { get; private set; }

        public bool InIntroOfferPeriod { get; private set; }

        public bool IsCancelled { get; private set; }

        /// <summary> Android platforms only </summary>
        public bool InPause { get; private set; }

        public bool InAccountHold { get; private set; }
    }
}