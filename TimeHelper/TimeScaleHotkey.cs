#if UNITY_EDITOR
using UnityEditor;

namespace NashUtilsCs.TimeHelper
{
    public static class TimeScaleHotkey
    {
        // %(ctrl on Windows, cmd on macOS), # (shift), & (alt)
        [MenuItem("NashUtils/RealSwitchSoftPause &p")]
        public static void SwitchSoftPause()
        {
            TimeScaleHelper.SwitchSoftPause();
        }
    }
}
#endif