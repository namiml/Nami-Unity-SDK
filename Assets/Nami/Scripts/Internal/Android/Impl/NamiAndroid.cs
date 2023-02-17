using NamiSdk.Interfaces;
using NamiSdk.JNI;

namespace NamiSdk
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