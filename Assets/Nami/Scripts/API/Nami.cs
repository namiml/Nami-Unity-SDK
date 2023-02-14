using System.Collections;
using System.Collections.Generic;
using NamiSdk.JNI;
using UnityEngine;

namespace NamiSdk
{
    public class Nami
    {
        public static void Init(NamiConfiguration configuration)
        {
            JniToolkitUtils.RunOnUiThread(() =>
            {
                APIPath.Nami.AJCCallStaticOnce("configure", configuration.AJO);
            });
        }
    }
}