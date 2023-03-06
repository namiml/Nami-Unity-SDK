using NamiSDK;
using UnityEngine;

namespace NamiExample
{
    public class NamiExample : MonoBehaviour
    {
        private void Awake()
        {
            Nami.Init(new NamiConfiguration.Builder()
                .LogLevel(NamiLogLevel.Debug)
                .SettingsList("useStagingAPI")
                .Build());
        }
    }
}