#if UNITY_ANDROID
using NamiSDK.Interfaces;
using NamiSDK.Utils;

namespace NamiSDK.Implementation
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