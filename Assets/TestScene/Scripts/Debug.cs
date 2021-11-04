using System.Runtime.CompilerServices;

namespace TestScene
{
    public static class Debug
    {
        public static void Log(string text, [CallerFilePath] string file = "null", [CallerMemberName]string method = "null", object context = null)
        {
            HashColorLog.HashColorLog.Log(text, file, method, context);
        }
    }
}