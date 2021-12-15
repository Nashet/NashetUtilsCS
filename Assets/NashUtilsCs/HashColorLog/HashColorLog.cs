using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using Apex;
using UnityEngine;

namespace NashUtilsCs.HashColorLog
{
    public static class HashColorLog
    {
        private static int _logNumber;

        private static string[] _logClasses =
            { nameof(HashColorLog), "DebugExt", "SimpleLogger", "Extensions", "Logs", "Debug" };

        static HashColorLog()
        {
            Application.logMessageReceivedThreaded += AutoLog;
        }

        private static void AutoLog(string condition, string stacktrace, LogType type)
        {
            switch (type)
            {
                case LogType.Error:
                case LogType.Exception:
                case LogType.Log:
                case LogType.Assert:
                    var splitString = stacktrace.Split('\n');
                    //splitString.Reorder(0, splitString.Length - 1);
                    var c = GetFilteredClassName(splitString);
                    var file2 = c.Replace('/', '\\');
                    var method = GetMethodFromFileString(file2);
                    Log(condition, file2, method);
                    break;
                case LogType.Warning:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        //doubles old logs
        public static void Log(string text, [CallerFilePath] string file = "null",
            [CallerMemberName] string method = "null", object context = null)
        {
            string[] splitName;
            if (context == null)
                splitName = file.Split('\\');
            else
                splitName = context.GetType().ToString().Split('.');

            var classname = splitName[splitName.Length - 1];

            //.Replace(".cs", "");

            var color = GetColorFromName(classname);

            var hadMultipleLines = text.Contains("\n");
            var oneLineText = text
                    .Replace("\n", " ")
                    .Replace("\t", "")
                ;

            var message = $"{_logNumber}@{method}()@[{classname}] {oneLineText}";
            var colorized =
                $"<color=#{(byte)(color.r * 255f):X2}{(byte)(color.g * 255f):X2}{(byte)(color.b * 255f):X2}>{message}</color>";
            if (hadMultipleLines)
                colorized += "\n(Original text) " + text;


            Debug.LogError(colorized);

            _logNumber++;
        }

        private static string GetFilteredClassName(string[] splitName)
        {
            var classname = splitName[0];

            var classLevel = 0;
            for (var index = 0; index < _logClasses.Length; index++)
            {
                var logClass = _logClasses[index];
                if (string.IsNullOrEmpty(classname) || classname.Contains(logClass))
                {
                    classLevel++;
                    classname = splitName[classLevel];
                    index = -1;
                    // at every new level it should be re run
                }
            }

            classname = classname.Replace(")", "");
            return classname;
        }

        private static Color GetColorFromName(string classname)
        {
            //Compute hash based on source data
            var tmpSource = ASCIIEncoding.ASCII.GetBytes(classname);
            var tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
            var color = new Color(tmpHash[0] / 255f, tmpHash[1] / 255f, tmpHash[2] / 255f);
            return color;
        }

        private static string GetMethodFromFileString(string file)
        {
            int Pos1 = file.IndexOf(":") + 1;
            int Pos2 = file.IndexOf("(") - 1;
            var method = file.Substring(Pos1, Pos2 - Pos1);
            return method;
        }
    }
}