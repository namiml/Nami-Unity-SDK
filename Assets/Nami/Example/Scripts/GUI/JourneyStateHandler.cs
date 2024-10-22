using NamiSDK;
using UnityEngine;
using UnityEngine.UI;

namespace NamiExample
{
    public class JourneyStateHandler : MonoBehaviour
    {
        [SerializeField] private Color enabledColor = Color.green;
        [SerializeField] private Color disabledColor = Color.gray;

        [Space] [SerializeField] private Image inTrialPeriodStateImages;
        [SerializeField] private Image inIntroOfferPeriodStateImages;
        [SerializeField] private Image isCancelledStateImages;
        [SerializeField] private Image formerSubscriberStateImages;
        [SerializeField] private Image inGracePeriodStateImages;
        [SerializeField] private Image inPauseStateImages;
        [SerializeField] private Image inAccountHoldStateImages;

        private void Start()
        {
            NamiCustomerManager.RegisterJourneyStateHandler(UpdateJourneyState);
        }

        private void UpdateJourneyState(CustomerJourneyState journeyState)
        {
            Debug.Log("JourneyState callback received" +
                      "\nJourneyState: " + 
                      "\n> FormerSubscriber: " + journeyState?.FormerSubscriber +
                      "\n> InGracePeriod: " + journeyState?.InGracePeriod +
                      "\n> InTrialPeriod: " + journeyState?.InTrialPeriod +
                      "\n> InIntroOfferPeriod: " + journeyState?.InIntroOfferPeriod +
                      "\n> IsCancelled: " + journeyState?.IsCancelled +
                      "\n> (Android only) InPause: " + journeyState?.InPause +
                      "\n> InAccountHold: " + journeyState?.InAccountHold);

            if (journeyState == null) return;

            SetStateImageColor(inTrialPeriodStateImages, journeyState.InTrialPeriod);
            SetStateImageColor(inIntroOfferPeriodStateImages, journeyState.InIntroOfferPeriod);
            SetStateImageColor(isCancelledStateImages, journeyState.IsCancelled);
            SetStateImageColor(formerSubscriberStateImages, journeyState.FormerSubscriber);
            SetStateImageColor(inGracePeriodStateImages, journeyState.InGracePeriod);
            SetStateImageColor(inPauseStateImages, journeyState.InPause);
            SetStateImageColor(inAccountHoldStateImages, journeyState.InAccountHold);
        }

        private void SetStateImageColor(Image image, bool isEnabled)
        {
            image.color = isEnabled ? enabledColor : disabledColor;
        }
    }
}