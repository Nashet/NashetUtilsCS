using System.Runtime.CompilerServices;
using NashUtilsCs.HashColorLog;

namespace TestScene
{
    public static class Debug
    {
        public static void Log(string text, [CallerFilePath] string file = "null", [CallerMemberName] string method = "null", object context = null)
        {
            HashColorLog.Log(text, file, method, context);
        }
    }
}