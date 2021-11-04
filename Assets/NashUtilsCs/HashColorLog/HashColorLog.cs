using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace NashUtilsCs.HashColorLog
{
    public static class HashColorLog
    {
        private static int _logNumber;

        public static void Log(string text, [CallerFilePath] string file = "null", [CallerMemberName]string method = "null", object context = null)
        {
            string[] className;
            if (context == null)
                className = file.Split('\\');
            else
                className = context.GetType().ToString().Split('.');

            var classname = className[className.Length - 1];
            //.Replace(".cs", "");

            //Compute hash based on source data
            var tmpSource = ASCIIEncoding.ASCII.GetBytes(classname);
            var tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
            var color = new Color(tmpHash[0] / 255f, tmpHash[1] / 255f, tmpHash[2] / 255f);

            var hadMultipleLines = text.Contains("\n");
            var oneLineText = text
                    .Replace("\n", " ")
                    .Replace("\t", "")
                ;
            
            var message = $"{_logNumber}@[{classname}][{method}()] {oneLineText}";
            var colorized = $"<color=#{(byte)(color.r * 255f):X2}{(byte)(color.g * 255f):X2}{(byte)(color.b * 255f):X2}>{message}</color>";
            if (hadMultipleLines)
                colorized += "\n(Original text) " + text;
            
            Debug.Log(colorized);

            _logNumber++;
        }
    }
}
