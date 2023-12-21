#if UNITY_EDITOR

using System.Reflection;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace NashUtilsCs.RestartHelper
{
    public static class RestartHelper
    {
        private static bool _isRestarting;
        private static readonly System.Diagnostics.Stopwatch _timer = new System.Diagnostics.Stopwatch();

        // %(ctrl on Windows, cmd on macOS), # (shift), & (alt)
        [MenuItem("NashUtils/RestartHelper &r")]
        public static void RestartIfCompilingIsDone()
        {
            if (_isRestarting)
            {
                Log($"All ready restarting. But you can push  alt a");
            }
            else
            {
                //ClearConsole();
                Log("Registered attempt to restart editor Ж)");
                RestartAsync();
            }
        }

        // %(ctrl on Windows, cmd on macOS), # (shift), & (alt)
        [MenuItem("NashUtils/AbortLaunch &a")]
        public static void AbortLaunch()
        {
            Log("Registered attempt to abort launch");
            AbortLaunchAsync();
            _isRestarting = false;
        }

        private static async void AbortLaunchAsync()
        {
            while (UnityEditor.EditorApplication.isPlaying)
            {
                Log("Waiting to stop");
                await Task.Delay(100);
                UnityEditor.EditorApplication.isPlaying = false;
            }
        }

        private static void Log(string text)
        {
            Debug.LogError("[RestartHelper] " + text);
        }

        private static async void RestartAsync()
        {
            _isRestarting = true;
            _timer.Start();
            EditorApplication.isPlaying = false;
            await Task.Delay(40);

            while (EditorApplication.isPlaying)
            {
                Log("Waiting to stop");
                await Task.Delay(50);
            }

            var stoppingTime = _timer.ElapsedMilliseconds;
            Log($"Stopping took {stoppingTime} ms.");


            while (EditorApplication.isCompiling || EditorApplication.isUpdating)
            {
                // actually goes to play here, compiling goes later
                Log($"Waiting for compiling");
                await Task.Delay(50);
            }

            Log($"Editor is ready to go. Compiling took {_timer.ElapsedMilliseconds - stoppingTime} ms");

            Log($"Editor is in play, but compilation still going :) totaly took {_timer.ElapsedMilliseconds} ms");
            _timer.Stop();
            _isRestarting = false;

            EditorApplication.EnterPlaymode(); //quickly stops that async or entire script
            EditorApplication.Beep();
        }

        private static void ClearConsole()
        {
            var assembly = Assembly.GetAssembly(typeof(SceneView));
            var type = assembly.GetType("UnityEditor.LogEntries");
            var method = type.GetMethod("Clear");
            method.Invoke(new object(), null);
        }
    }
}
#endif