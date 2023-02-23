using UnityEngine;

namespace NamiSdk.Example
{
    public class NamiExample : MonoBehaviour
    {
        [SerializeField] private string googleAppPlatformId = "ff5679b3-3215-4ec4-b186-2ea78c9e9d94";
        [SerializeField] private string appleAppPlatformId = "111c1877-d660-4ad8-90f3-0b553e19e570";

        private void Start()
        {
            var targetAppPlatformId = Application.platform == RuntimePlatform.Android
                ? googleAppPlatformId
                : appleAppPlatformId;

            Nami.Init(new NamiConfiguration.Builder(targetAppPlatformId)
                .LogLevel(NamiLogLevel.Debug)
                .SettingsList("useStagingAPI")
                .Build());
        }
    }
}