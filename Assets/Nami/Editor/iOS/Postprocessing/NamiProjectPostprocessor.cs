#if UNITY_IOS

using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEngine;

namespace NamiSDK
{
	public static class NamiProjectPostProcessor
	{
		[PostProcessBuild(2000)]
		public static void OnPostProcessBuild(BuildTarget target, string path)
		{
			Debug.Log("NamiProjectPostProcessor is now postprocessing iOS Project");

			var projectPath = PBXProject.GetPBXProjectPath(path);

			var project = new PBXProject();
			project.ReadFromFile(projectPath);

#if UNITY_2019_3_OR_NEWER
			var targetGuid = project.GetUnityFrameworkTargetGuid();
#else
			var targetName = PBXProject.GetUnityTargetName();
			var targetGuid = project.TargetGuidByName(targetName);
#endif
			project.SetBuildProperty(targetGuid, "ENABLE_BITCODE", "NO");
			project.SetBuildProperty(targetGuid, "SWIFT_OBJC_BRIDGING_HEADER", "Libraries/Nami/Editor/iOS/ObjC/Nami-Bridging-Header.h");
			project.SetBuildProperty(targetGuid, "SWIFT_OBJC_INTERFACE_HEADER_NAME", "NamiApple-Swift.h");
			project.AddBuildProperty(targetGuid, "LD_RUNPATH_SEARCH_PATHS", "@executable_path/Frameworks $(PROJECT_DIR)/lib/$(CONFIGURATION) $(inherited)");
			project.AddBuildProperty(targetGuid, "FRAMERWORK_SEARCH_PATHS", "$(inherited) $(PROJECT_DIR) $(PROJECT_DIR)/Frameworks");
			project.AddBuildProperty(targetGuid, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "YES");
			project.AddBuildProperty(targetGuid, "DYLIB_INSTALL_NAME_BASE", "@rpath");
			project.AddBuildProperty(targetGuid, "LD_DYLIB_INSTALL_NAME", "@executable_path/../Frameworks/$(EXECUTABLE_PATH)");
			project.AddBuildProperty(targetGuid, "DEFINES_MODULE", "YES");
			project.AddBuildProperty(targetGuid, "SWIFT_VERSION", "5.0");
			project.AddBuildProperty(targetGuid, "COREML_CODEGEN_LANGUAGE", "Swift");

			project.WriteToFile(projectPath);
		}
	}
}

#endif