using NamiSDK;
using UnityEngine;
using UnityEngine.UI;

namespace NamiExample
{
    [RequireComponent(typeof(Button))]
    public class LaunchButton : MonoBehaviour
    {
        private Button button;
        [SerializeField] private string label;

        private void Start()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            if (string.IsNullOrEmpty(label))
            {
                NamiCampaignManager.Launch();
                return;
            }

            var launchHandler = new LaunchHandler(() =>
            {
                Debug.Log("------------> " + label + " launch success!");
            }, errorMsg =>
            {
                Debug.Log("------------> " + label + " launch failure!\n" + errorMsg);
            }, (purchaseState, activePurchases, errorMsg) =>
            {
                Debug.Log("(Android only) ------------> " + label + " purchase changed callback:" +
                          "\nPurchaseState: " + purchaseState +
                          "\nActivePurchases Count: " + activePurchases?.Count + 
                          "\nErrorMsg: " + errorMsg);
            });

            var paywallActionHandler = new PaywallActionHandler((namiPaywallAction, sku, errorMsg, purchases) =>
            {
                Debug.Log("------------> " + label + " paywall action callback:" +
                          "\nNamiPaywallAction: " + namiPaywallAction +
                          "\nSKU Name: " + sku?.Name + 
                          "\n(iOS only) ErrorMsg: " + errorMsg +
                          "\n(iOS only) Purchases Count: " + purchases?.Count
                          );
            });

            NamiCampaignManager.Launch(label, launchHandler, paywallActionHandler);
        }
    }
}
