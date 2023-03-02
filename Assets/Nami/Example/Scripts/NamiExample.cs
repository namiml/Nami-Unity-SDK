using NamiSdk;
using UnityEngine;

namespace NamiExample
{
    public class NamiExample : MonoBehaviour
    {
        private void Start()
        {
            Nami.Init(new NamiConfiguration.Builder()
                .LogLevel(NamiLogLevel.Debug)
                .SettingsList("useStagingAPI")
                .Build());
        }
    }
}