#if UNITY_ANDROID
using NamiSdk.Interfaces;
using NamiSdk.Utils;

namespace NamiSdk.Implementation
{
    public class NamiAndroid : INami
    {
        public void Init(NamiConfiguration configuration)
        {
            JniToolkitUtils.RunOnUiThread(() =>
            {
                JavaClassNames.Nami.AJCCallStaticOnce("configure", configuration.AJO);
            });
        }
    }
}
#endif