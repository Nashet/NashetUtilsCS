#if UNITY_EDITOR
using System.Diagnostics;
using UnityEditor;
using UnityEditor.Callbacks;

namespace NashUtilsCs.BuildPostprocessor
{
    public static class BuildPostProcessor
    {
        private static string _shellRoute = "/bin/bash";

        static BuildPostProcessor()
        {
#if UNITY_EDITOR_WIN
            _shellRoute = "cmd";
#endif
        }

        //didn't worked for real
        [PostProcessBuild(1)]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
        {
#if UNITY_IOS
			// ExecuteBashCommand("sh /Users/MaUser/builds/copy.commands");
			// Debug.Log( pathToBuiltProject );
            ExecuteBashCommand($"CD {pathToBuiltProject}");
            ExecuteBashCommand("pod install");
#endif
        }

        private static string ExecuteBashCommand(string command)
        {
            // according to: https://stackoverflow.com/a/15262019/637142
            // thans to this we will pass everything as one command
            command = command.Replace("\"", "\"\"");
            UnityEngine.Debug.LogError($" command {command}  ");

            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = _shellRoute,
                    Arguments = "-c \"" + command + "\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = false
                }
            };

            proc.Start();
            proc.WaitForExit();

            return proc.StandardOutput.ReadToEnd();
        }

        public static void Test()
        {
            OnPostprocessBuild(BuildTarget.iOS, "route");
        }
    }
}
#endif