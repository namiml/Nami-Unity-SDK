using System;
using NamiSdk.Utils;
using UnityEngine;

namespace NamiSdk
{
    [Serializable]
    public class NamiConfiguration
    {
        [SerializeField] private string appPlatformId;
        [SerializeField] private NamiLogLevel logLevel;
        [SerializeField] private bool bypassStore;
        [SerializeField] private bool fullScreenPresentation;
        [SerializeField] private bool developmentMode;
        [SerializeField] private NamiLanguageCode? namiLanguageCode;

        private NamiConfiguration(string appPlatformId, NamiLogLevel logLevel, bool bypassStore, bool fullScreenPresentation, bool developmentMode, NamiLanguageCode? namiLanguageCode)
        {
            this.appPlatformId = appPlatformId;
            this.logLevel = logLevel;
            this.bypassStore = bypassStore;
            this.fullScreenPresentation = fullScreenPresentation;
            this.developmentMode = developmentMode;
            this.namiLanguageCode = namiLanguageCode;
        }

        public class Builder
        {
            private string appPlatformId = null;
            private NamiLogLevel logLevel = Application.platform == RuntimePlatform.Android ? NamiLogLevel.Warn : NamiLogLevel.Error;
            private bool bypassStore = false;
            private bool fullScreenPresentation = false;
            private bool developmentMode = false;
            private NamiLanguageCode? namiLanguageCode = Application.platform == RuntimePlatform.Android ? (NamiLanguageCode?)null : NamiSdk.NamiLanguageCode.EN;

            public Builder(string appPlatformId)
            {
                this.appPlatformId = appPlatformId;
            }

            public Builder BypassStore(bool bypassStore)
            {
                this.bypassStore = bypassStore;
                return this;
            }

            public Builder LogLevel(NamiLogLevel logLevel)
            {
                this.logLevel = logLevel;
                return this;
            }

            /// <summary> Apple platforms only </summary>
            public Builder FullScreenPresentation(bool fullScreenPresentation)
            {
                this.fullScreenPresentation = fullScreenPresentation;
                return this;
            }

            /// <summary> Android platforms only </summary>
            public Builder DevelopmentMode(bool developmentMode)
            {
                this.developmentMode = developmentMode;
                return this;
            }

            public Builder NamiLanguageCode(NamiLanguageCode namiLanguageCode)
            {
                this.namiLanguageCode = namiLanguageCode;
                return this;
            }

            public NamiConfiguration Build()
            {
                return new NamiConfiguration(appPlatformId, logLevel, bypassStore, fullScreenPresentation, developmentMode, namiLanguageCode);
            }
        }

        public AndroidJavaObject AJO
        {
            get
            {
                var ajo = new AndroidJavaObject(JavaClassNames.NamiConfiguration + "$Builder", JniToolkitUtils.Activity, appPlatformId);
                ajo.CallAJO("bypassStore", bypassStore);
                ajo.CallAJO("developmentMode", developmentMode);
                ajo.CallAJO("logLevel", logLevel.EnumToJava(JavaEnumNames.NamiLogLevel));
                if (namiLanguageCode != null) ajo.CallAJO("namiLanguageCode", ((NamiLanguageCode)namiLanguageCode).EnumToJava(JavaEnumNames.NamiLanguageCode));
                // JavaClassNames.NamiBridge.AJCCallStaticOnce("setSettingsListHack", ajo);
                return ajo.CallAJO("build");
            }
        }

        public string JSON => JsonUtility.ToJson(this).AddJsonParam("namiLanguageCode", namiLanguageCode.ToString().ToLower());
    }
}
