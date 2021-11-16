#if UNITY_EDITOR
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace NashUtilsCs.RestartHelper
{
    public static class RestartHelper
    {
        // %(ctrl on Windows, cmd on macOS), # (shift), & (alt)
        [MenuItem("EDITORS/M3/RestartHelper &r")]
        public static void RestartIfCompilingIsDone()
        {
            Log("Registered attempt to restart editor Ж)");
            UnityEditor.EditorApplication.isPlaying = false;
            RestartAsync();
        }
        
        // %(ctrl on Windows, cmd on macOS), # (shift), & (alt)
        [MenuItem("EDITORS/M3/AbortLaunch &a")]
        public static void AbortLaunch()
        {
            Log("Registered attempt to abort launch");
            UnityEditor.EditorApplication.isPlaying = false;
        }

        private static async void AbortLaunchAsync()
        {
            while (UnityEditor.EditorApplication.isPlaying)
            {
                Log("Waiting to stop");
                await Task.Delay(1000);
                UnityEditor.EditorApplication.isPlaying = false;
            }
        }

        private static void Log(string text)
        {
            Debug.Log("[RestartHelper] "+ text);
        }

        private static async void RestartAsync ()
        {
            while (UnityEditor.EditorApplication.isPlaying)
            {
                Log("Waiting to stop");
                await Task.Delay(1000);
            }
            
            while (UnityEditor.EditorApplication.isCompiling)
            {
                Log("Waiting for compiling");
                await Task.Delay(1000);
            }
            
            Log("Editor is ready to go");
            UnityEditor.EditorApplication.EnterPlaymode();
            Log("Editor is loading :)");
        }
    }
}
#endif