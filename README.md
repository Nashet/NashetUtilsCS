# NashetUtilsCS

## HashColorLog 

How to use colorful error logs: You can put that class into our namespace and use it instead of unity's logger.
Or, you can relay on HashColorLog automatic logger. In that case in will duplicate messages from standart logger. You can disable HashColorLog in code.
```
namespace TestScene
{
	public static class Debug
	{
		public static void LogError(string text, [CallerFilePath] string file = "null", [CallerMemberName] string method = "null", object context = null)
		{
			NashUtilsCs.HashColorLog.HashColorLog.Log(text, file, method, context);
		}
	}
}
```

There is sample for that

## RestartHelper

Restarts playmode by pressing alt+r. alt+a aborts compilations.

## Timehelper

Timehelper is usefull to create "soft" pause. It reduces timeline scale to small number.

## NullCheker

NullCheker reminds you if you forget to set fields in Monobehavior. It also highlights null fields with red color. Requires [IsNotNull] attribute. If you want to change exclusions you can try YourProject\Library\PackageCache\com.nashet.utils@
