using NamiSdk.Interfaces;
using NamiSdk.JNI;

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