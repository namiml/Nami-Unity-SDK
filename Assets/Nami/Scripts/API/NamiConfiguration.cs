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
        // [SerializeField] private List<string> settingsList;
        [SerializeField] private bool developmentMode = false;
        [SerializeField] private bool bypassStore = false;
        [SerializeField] private NamiLanguageCode? namiLanguageCode;

        private NamiConfiguration(string appPlatformId, NamiLogLevel logLevel, /* , List<string> settingsList, */ bool developmentMode, bool bypassStore, NamiLanguageCode? namiLanguageCode)
        {
            this.appPlatformId = appPlatformId;
            this.logLevel = logLevel;
            // this.settingsList = settingsList;
            this.developmentMode = developmentMode;
            this.bypassStore = bypassStore;
            this.namiLanguageCode = namiLanguageCode;
        }

        public class Builder
        {
            private string appPlatformId;
            private NamiLogLevel logLevel = NamiLogLevel.Warn;
            // private List<string> settingsList;
            private bool developmentMode = false;
            private bool bypassStore = false;
            private NamiLanguageCode? namiLanguageCode;

            public Builder(string appPlatformId)
            {
                this.appPlatformId = appPlatformId;
            }

            public Builder BypassStore(bool bypassStore)
            {
                this.bypassStore = bypassStore;
                return this;
            }

            public Builder DevelopmentMode(bool developmentMode)
            {
                this.developmentMode = developmentMode;
                return this;
            }

            public Builder LogLevel(NamiLogLevel logLevel)
            {
                this.logLevel = logLevel;
                return this;
            }

            public Builder NamiLanguageCode(NamiLanguageCode namiLanguageCode)
            {
                this.namiLanguageCode = namiLanguageCode;
                return this;
            }

            public NamiConfiguration Build()
            {
                return new NamiConfiguration(appPlatformId, logLevel, /* settingsList, */ developmentMode, bypassStore, namiLanguageCode);
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

        public string JSON
        {
            get
            {
                var jsonString = JsonUtility.ToJson(this);
                jsonString = jsonString.AddJsonParam("namiLanguageCode", namiLanguageCode);
                Debug.Log("-------------------------------> " + jsonString);
                return jsonString;
            }
        }
    }
}
