using NamiSdk;
using UnityEngine;

public class NamiExample : MonoBehaviour
{
    [SerializeField] private string appPlatformId = "ff5679b3-3215-4ec4-b186-2ea78c9e9d94";

    private void Start()
    {
        Nami.Init(new NamiConfiguration.Builder(appPlatformId).LogLevel(NamiLogLevel.Debug).Build());
    }

    public void Launch(string label)
    {
        NamiCampaignManager.Launch(label, (paywallAction, s) =>
        {
            Debug.Log("----------------------------> onNamiPaywallAction : Queue" + "NamiPaywallAction:" + paywallAction);
        }, () =>
        {
            Debug.Log("----------------------------> onSuccess : Queue");
        }, error =>
        {
            Debug.Log("----------------------------> onFailure : Queue" + "Error:" + error);
        }, (purchaseState, list, error) =>
        {
            Debug.Log("----------------------------> onPurchaseChanged : Queue" + "NamiPurchaseState:" + purchaseState);
        });
    }
}