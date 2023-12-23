#if UNITY_EDITOR

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace NashUtilsCs.HashColorLog
{
    public static class HashColorLog
    {
        private static readonly string[] LOG_CLASSES =
            { nameof(HashColorLog), "DebugExt", "SimpleLogger", "Extensions", "Logs", "Debug" };

        private static readonly List<LogType> AUTO_LOG_LIMIT = new List<LogType> { LogType.Assert, LogType.Error};
        private const char METHOD_NAME_PREFIX = ':';
        private const char METHOD_NAME_PREFIX2 = '.';
        private const char METHOD_NAME_POSTFIX = '(';
        private static readonly char[] CLASSNAME_SEPARATOR = { '.' };
        private static readonly char[] SLASH_SEPARATOR = { '\\' };
        private static readonly char[] NEW_LINE_SEPARATOR = { '\n' };
        private static readonly MD5CryptoServiceProvider MD5_CRYPTO_SERVICE_PROVIDER = new MD5CryptoServiceProvider();
        private const string NO_CLASS_INFO = "NoClassInfo";
        
        private static int _logNumber;
        private static bool ENABLE_AUTOLOG = true; 

        static HashColorLog()
        {
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Init()
        {
			Application.logMessageReceivedThreaded += AutoLog;
		}

		//misses first log
		// add context?
		// serialize fields?
		private static void AutoLog(string condition, string stacktrace, LogType type)
        {
            if (!NashUtilsSettings.instance.useAutoLogs)
                return;
            
            if (condition.StartsWith(".<c")) //  .<color= - already posted from manual call, see below
                return;

            if (!AUTO_LOG_LIMIT.Contains(type))
                return;
            
            var splitString = stacktrace.Split(NEW_LINE_SEPARATOR);

            var classString = GetFilteredClassName(splitString);
            var file = classString.Replace('/', '\\');
            var method = GetMethodFromFileString(file);
            Log(condition, file, method);
        }

        public static void Log(string text, [CallerFilePath] string file = "null",
            [CallerMemberName] string method = "null", object context = null)
        {
            string[] splitName;
            if (context == null)
                splitName = file.Split(SLASH_SEPARATOR);
            else
                splitName = context.GetType().ToString().Split(CLASSNAME_SEPARATOR);

            var classname = splitName[splitName.Length - 1];

            //.Replace(".cs", "");

            var color = GetColorFromName(classname);

            var hadMultipleLines = text.Contains("\n");
            var oneLineText = text
                    .Replace("\n", " ")
                    .Replace("\t", "")
                ;

            var hexColor = (uint)(color.r * 255f * 256 * 256 + color.g * 255f * 256 + color.b * 255f);
            var colorized =
                $".<color=#{hexColor:X2}>{_logNumber} {method}()@{classname} {oneLineText}</color>{(hadMultipleLines ? $"\n(Original text) {text}" : string.Empty)}";

            Debug.LogError(colorized);

            _logNumber++;
        }

        private static string GetFilteredClassName(IReadOnlyList<string> splitName)
        {
            var classname = splitName[0];

            var classLevel = 0;
            for (var index = 0; index < LOG_CLASSES.Length; index++)
            {
                var logClass = LOG_CLASSES[index];
                if (string.IsNullOrEmpty(classname) || classname.Contains(logClass))
                {
                    classLevel++;

                    if (classLevel > splitName.Count - 1)
                    {
                        classname = NO_CLASS_INFO;
                    }
                    else
                        classname = splitName[classLevel];

                    index = -1;
                }
            }

            classname = classname.Replace(")", "");
            return classname;
        }

        private static Color GetColorFromName(string classname)
        {
            //Compute hash based on source data
            var tmpSource = Encoding.ASCII.GetBytes(classname);
            var tmpHash = MD5_CRYPTO_SERVICE_PROVIDER.ComputeHash(tmpSource);
            var color = new Color(tmpHash[0] / 255f, tmpHash[1] / 255f, tmpHash[2] / 255f);
            return color;
        }

        private static string GetMethodFromFileString(string file)
        {
            if (file == NO_CLASS_INFO)
                return NO_CLASS_INFO;

            var pos1 = file.IndexOf(METHOD_NAME_PREFIX) + 1;
            var pos2 = 0;
            pos2 = file.IndexOf(METHOD_NAME_POSTFIX) - 1;


            if (pos2 < pos1)
                pos1 = file.LastIndexOf(METHOD_NAME_PREFIX2, 0, pos2) + 1;

            var method = "";
            method = file.Substring(pos1, pos2 - pos1);

            return method;
        }
    }
}
#endif