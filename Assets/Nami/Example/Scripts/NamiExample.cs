using NamiSdk;
using UnityEngine;

public class NamiExample : MonoBehaviour
{
    [SerializeField] private string appPlatformId = "ff5679b3-3215-4ec4-b186-2ea78c9e9d94";

    private void Start()
    {
        Nami.Init(new NamiConfiguration.Builder(appPlatformId).Build());
    }

    public void Launch(string label)
    {
        NamiCampaignManager.Launch(label);
    }
}