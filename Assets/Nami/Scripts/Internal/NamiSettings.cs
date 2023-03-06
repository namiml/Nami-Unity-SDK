using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace NamiSDK
{
    public class NamiSettings : ScriptableObject
    {
        public const string APPLE_ID_PLACEHOLDER = "111c1877-d660-4ad8-90f3-0b553e19e570";
        public const string GOOGLE_ID_PLACEHOLDER = "ff5679b3-3215-4ec4-b186-2ea78c9e9d94";

        private const string SettingsAssetName = "NamiSettings";
        private const string SettingsAssetPath = "Assets/Resources/";

        [SerializeField] private string appleId = APPLE_ID_PLACEHOLDER;
        [SerializeField] private string googleId = GOOGLE_ID_PLACEHOLDER;

        public static string AppleAppPlatformId
        {
            get => Instance.appleId;
            set
            {
                Instance.appleId = value;
                MarkAssetDirty();
            }
        }

        public static string GoogleAppPlatformId
        {
            get => Instance.googleId;
            set
            {
                Instance.googleId = value;
                MarkAssetDirty();
            }
        }

        public static string TargetAppPlatformId
        {
            get => Application.platform == RuntimePlatform.Android ? GoogleAppPlatformId : AppleAppPlatformId;
        }

        private static NamiSettings _instance;

        public static NamiSettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load(SettingsAssetName) as NamiSettings;
                    if (_instance == null)
                    {
                        _instance = CreateInstance<NamiSettings>(); 
                        SaveAsset(SettingsAssetPath, SettingsAssetName);
                    }
                }

                return _instance;
            }
        }

        private static void SaveAsset(string directory, string name)
        {
#if UNITY_EDITOR
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            AssetDatabase.CreateAsset(Instance, directory + name + ".asset");
            AssetDatabase.Refresh();
#endif
        }

        private static void MarkAssetDirty()
        {
#if UNITY_EDITOR
            EditorUtility.SetDirty(Instance);
#endif
        }
    }
}