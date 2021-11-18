#if UNITY_EDITOR
using System;
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
        [MenuItem("EDITORS/M3/RestartHelper &r")]
        public static void RestartIfCompilingIsDone()
        {
            if (_isRestarting)
            {
                Log($"All ready restarting. But you can push  alt a");
            }
            else
            {
                ClearConsole();
                Log("Registered attempt to restart editor Ж)");
                RestartAsync();    
            }
        }

        // %(ctrl on Windows, cmd on macOS), # (shift), & (alt)
        [MenuItem("EDITORS/M3/AbortLaunch &a")]
        public static void AbortLaunch()
        {
            Log("Registered attempt to abort launch");
            UnityEditor.EditorApplication.isPlaying = false;
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
            UnityEditor.EditorApplication.isPlaying = false;
            await Task.Delay(40);
            while (UnityEditor.EditorApplication.isPlaying)
            {
                Log("Waiting to stop");
                await Task.Delay(100);
            }

            var stopingTime = _timer.ElapsedMilliseconds;
            Log($"Stopping took {stopingTime} ms.");
            while (UnityEditor.EditorApplication.isCompiling)
            {
                Log($"Waiting for compiling");
                await Task.Delay(100);
            }

            Log($"Editor is ready to go. Compiling took {_timer.ElapsedMilliseconds - stopingTime} ms");
            UnityEditor.EditorApplication.EnterPlaymode();

            _timer.Stop();
            Log($"Editor is loading :) totaly took {_timer.ElapsedMilliseconds} ms");
            _isRestarting = false;
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