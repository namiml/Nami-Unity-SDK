using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
            Debug.Log("----------------------------> onNamiPaywallAction : Queue : " + "NamiPaywallAction:" + paywallAction);
        }, () =>
        {
            Debug.Log("----------------------------> onSuccess : Queue");
        }, error =>
        {
            Debug.Log("----------------------------> onFailure : Queue : " + "Error:" + error);
        }, (purchaseState, activePurchases, error) =>
        {
            Debug.Log("----------------------------> onPurchaseChanged : Queue : " + "NamiPurchaseState:" + purchaseState);
        });
    }

    public void Login()
    {
        if (NamiCustomerManager.IsLoggedIn) return;
        var uuid = Guid.NewGuid().ToString();
        NamiCustomerManager.Login(uuid);
    }

    public void Logout()
    {
        if (!NamiCustomerManager.IsLoggedIn) return;
        NamiCustomerManager.Logout();
    }

    private static string sha256(string value)
    {
        using SHA256 hash = SHA256.Create();
        return string.Concat(hash
            .ComputeHash(Encoding.UTF8.GetBytes(value))
            .Select(item => item.ToString("x2")));
    }
}