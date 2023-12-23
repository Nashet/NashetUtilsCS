#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build;
using UnityEngine; // really need that

namespace NashUtilsCs.BuildProcessor
{ public class BuildPreProcessor : IPreprocessBuild
    {
        public int callbackOrder
        {
            get { return 0; }
        }

        public void OnPreprocessBuild(BuildTarget target, string path)
        {
#if UNITY_ANDROID
            if (NashUtilsSettings.instance.useRandomAndroidBuildVersion)
            {
                var defaultBuild = 75588;
                if (PlayerSettings.Android.bundleVersionCode == defaultBuild)
                    PlayerSettings.Android.bundleVersionCode = Random.Range(defaultBuild, 999999);
            }
#endif
        }
    }
}
#endif