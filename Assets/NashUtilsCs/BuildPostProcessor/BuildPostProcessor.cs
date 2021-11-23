using System.Diagnostics;
using UnityEditor;
using UnityEditor.Callbacks;

namespace NashUtilsCs.BuildPostprocessor
{
    public static class BuildPostProcessor
    {
        //didn't worked for real
        [PostProcessBuild(1)]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
        {
#if UNITY_IOS
			// ExecuteBashCommand("sh /Users/MaUser/builds/copy.commands");
			// Debug.Log( pathToBuiltProject );
#endif
        }

        private static string ExecuteBashCommand(string command)
        {
            // according to: https://stackoverflow.com/a/15262019/637142
            // thans to this we will pass everything as one command
            command = command.Replace("\"", "\"\"");

            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = "-c \"" + command + "\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            proc.Start();
            proc.WaitForExit();

            return proc.StandardOutput.ReadToEnd();
        }
    }
}