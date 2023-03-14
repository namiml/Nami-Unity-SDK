using System;
using System.Collections.Generic;
using System.Linq;
using NamiSDK.Utils;
using UnityEngine;

namespace NamiSDK
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
        [SerializeField] private List<string> settingsList;

        private NamiConfiguration(string appPlatformId, NamiLogLevel logLevel, bool bypassStore, bool fullScreenPresentation, bool developmentMode, NamiLanguageCode? namiLanguageCode, List<string> settingsList)
        {
            this.appPlatformId = appPlatformId;
            this.logLevel = logLevel;
            this.bypassStore = bypassStore;
            this.fullScreenPresentation = fullScreenPresentation;
            this.developmentMode = developmentMode;
            this.namiLanguageCode = namiLanguageCode;
            this.settingsList = settingsList;
        }

        public class Builder
        {
            private const string ExtendedClientInfo = "extendedClientInfo:unity:" + NamiSDKSettings.Version;

            private string appPlatformId = null;
            private NamiLogLevel logLevel = Application.platform == RuntimePlatform.Android ? NamiLogLevel.Warn : NamiLogLevel.Error;
            private bool bypassStore = false;
            private bool fullScreenPresentation = false;
            private bool developmentMode = false;
            private NamiLanguageCode? namiLanguageCode = Application.platform == RuntimePlatform.Android ? (NamiLanguageCode?)null : NamiSDK.NamiLanguageCode.EN;
            private List<string> settingsList = new List<string>();

            /// <param name="appPlatformId">leave this parameter empty to use the Target Platform ID from NamiSettings.</param>
            public Builder(string appPlatformId = null)
            {
                if (appPlatformId == null)
                {
                    this.appPlatformId = NamiSettings.TargetAppPlatformId;
                }
                else
                {
                    this.appPlatformId = appPlatformId;
                }
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

            /// <summary> iOS only </summary>
            public Builder FullScreenPresentation(bool fullScreenPresentation)
            {
                this.fullScreenPresentation = fullScreenPresentation;
                return this;
            }

            /// <summary> GooglePlay only </summary>
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

            public Builder SettingsList(params string[] settingsList)
            {
                this.settingsList = settingsList.ToList();
                return this;
            }

            public NamiConfiguration Build()
            {
                settingsList.Add(ExtendedClientInfo);
                return new NamiConfiguration(appPlatformId, logLevel, bypassStore, fullScreenPresentation, developmentMode, namiLanguageCode, settingsList);
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
                if (settingsList != null) JavaClassNames.NamiBridge.AJCCallStaticOnce("settingsListHack", ajo, settingsList.ToJavaList());
                return ajo.CallAJO("build");
            }
        }

        public string JSON => JsonUtility.ToJson(this).AddJsonParam("namiLanguageCode", namiLanguageCode.ToString().ToLower());
    }
}
