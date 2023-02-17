using NamiSdk.JNI;
using UnityEngine;

namespace NamiSdk
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

        public bool FormerSubscriber { get; private set; }
        public bool InGracePeriod { get; private set; }
        public bool InTrialPeriod { get; private set; }
        public bool InIntroOfferPeriod { get; private set; }
        public bool IsCancelled { get; private set; }
        public bool InPause { get; private set; }
        public bool InAccountHold { get; private set; }
    }
}