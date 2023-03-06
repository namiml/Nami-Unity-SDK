using UnityEditor;
using UnityEngine;

namespace NamiSDK.Editor
{
	[CustomEditor(typeof(NamiSettings))]
	public class NamiSettingsEditor : UnityEditor.Editor
	{
		[MenuItem("Window/Nami/Edit Settings", false, 1000)]
		public static void Edit()
		{
			Selection.activeObject = NamiSettings.Instance;
		}

		public override void OnInspectorGUI()
		{
			using (new EditorGUILayout.VerticalScope("box"))
			{
				/*
				EditorGUILayout.HelpBox(
					"The plugin will modify your AndroidManifest.xml before the build starts",
					MessageType.Info);
				*/

				GUILayout.Label("Android Settings", EditorStyles.boldLabel);
				var androidApiKey = EditorGUILayout.TextField(new GUIContent("Google App Platform ID"),  NamiSettings.GoogleAppPlatformId);
				CheckApiKey(androidApiKey, NamiSettings.GOOGLE_ID_PLACEHOLDER);
				NamiSettings.GoogleAppPlatformId = androidApiKey;

				EditorGUILayout.Space();
				/*
				if (GUILayout.Button("Read how to get and setup Android API key"))
				{
					Application.OpenURL("");
				}
				*/
			}
			
			EditorGUILayout.Space();

			using (new EditorGUILayout.VerticalScope("box"))
			{
				GUILayout.Label("iOS Settings", EditorStyles.boldLabel);
				var iosApiKey = EditorGUILayout.TextField(new GUIContent("Apple App Platform ID"), NamiSettings.AppleAppPlatformId);
				CheckApiKey(iosApiKey, NamiSettings.APPLE_ID_PLACEHOLDER);
				NamiSettings.AppleAppPlatformId = iosApiKey;

				EditorGUILayout.Space();
				/*
				if (GUILayout.Button("Read how to get and setup iOS API key"))
				{
					Application.OpenURL("");
				}
				*/
			}

			EditorGUILayout.Space();

			using (new EditorGUILayout.HorizontalScope("box"))
			{
				if (GUILayout.Button("Read Documentation"))
				{
					Application.OpenURL("https://docs.namiml.com/");
				}
			}
		}

		static void CheckApiKey(string key, string placeholder)
		{
			if (key == placeholder)
			{
				EditorGUILayout.HelpBox(
					"This is a placeholder ID",
					MessageType.Warning);
			}
		}
	}
}