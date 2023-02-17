using NamiSdk.Interfaces;

namespace NamiSdk
{
    public static class Nami
    {
        private static readonly INami Impl;

        static Nami()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            Impl = new NamiUnityEditor();
#elif UNITY_ANDROID
            Impl = new NamiAndroid();
#elif UNITY_IOS
			Impl = new NamiIOS();
#endif
        }

        public static void Init(NamiConfiguration configuration)
        {
            Impl.Init(configuration);
        }
    }
}