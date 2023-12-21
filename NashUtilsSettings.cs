using System;
using UnityEditor;
using UnityEngine;

namespace NashUtilsCs
{
    public enum LogsUsage
    {
        None,
        UnityLogger,
        ColorLogger,
    }

    [CreateAssetMenu(fileName = nameof(NashUtilsSettings), menuName = "Nashet/" + nameof(NashUtilsSettings),
        order = 10)]
    public class NashUtilsSettings : ScriptableSingleton<NashUtilsSettings>
    {
        public bool useAutoLogs;

        //public LogsUsage debugLogsUsage = LogsUsage.UnityLogger;
        public bool useNullChecker;
        public bool useColorMarkingsForNulls;
        public bool useRandomAndroidBuildVersion; // really need that

        [MenuItem("NashUtils/"+nameof(NashUtilsSettings))]
        public static void RestartIfCompilingIsDone()
        {
            //todo find asset with settings
        }
    }
}