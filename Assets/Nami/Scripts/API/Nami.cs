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
            JniToolkitUtils.RunOnUiThread((() =>
            {
                "com.namiml.Nami".AJCCallStaticOnce("configure", configuration.AJO);
            }));
        }

        public static void Launch()
        {
            JniToolkitUtils.RunOnUiThread((() =>
            { 
                "com.namiml.unity.NamiBridge".AJCCallStaticOnce("launch", JniToolkitUtils.Activity);
            }));
        }
    }
}