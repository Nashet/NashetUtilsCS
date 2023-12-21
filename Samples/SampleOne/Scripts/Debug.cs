using System.Runtime.CompilerServices;
using NashUtilsCs.HashColorLog;

namespace TestScene
{
	/// <summary>
	/// You can put that class into tour namespace and use it instead of unity's logger
	/// Or, you can relay on HashColorLog automatic logger. In that case in will duplicate messages from standart logger
	/// </summary>
	public static class Debug
	{
		public static void LogError(string text, [CallerFilePath] string file = "null", [CallerMemberName] string method = "null", object context = null)
		{
			NashUtilsCs.HashColorLog.HashColorLog.Log(text, file, method, context);
		}
		public static void LogWarning(object message)
		{
			NashUtilsCs.HashColorLog.HashColorLog.Log(message.ToString());
		}
    
		public static void Log(object message)
		{
			NashUtilsCs.HashColorLog.HashColorLog.Log(message.ToString());
		}
    
		public static void LogException(object message)
		{
			NashUtilsCs.HashColorLog.HashColorLog.Log(message.ToString());
		}
	}
}