#if UNITY_EDITOR

using System;
using UnityEditor;
using UnityEngine;

namespace NashUtilsCs.TimeHelper
{
    public class TimeScaleHelper : EditorWindow
    {
        private float _sliderValue = 1f;
        private static bool _softPauseEnabled;
        private static float _originalTimeScale;
        private const float TimeScale = 0.00001f;
        private bool _manual;
        private float _previousSliderValue;

        // %(ctrl on Windows, cmd on macOS), # (shift), & (alt)
        [MenuItem("EDITORS/SwitchSoftPause &p")]
        public static void SwitchSoftPause()
        {
            _softPauseEnabled = !_softPauseEnabled;
            if (_softPauseEnabled)
            {
                _originalTimeScale = Time.timeScale;
                SetTimeScale(TimeScale);
            }
            else
            {
                SetTimeScale(_originalTimeScale);
            }
        }

        private static void SetTimeScale(float scale)
        {
            //Debug.LogError($"   Sets scle {scale}");
            Time.timeScale = scale;
        }

        private static bool _isShown;

        [MenuItem("EDITORS/TimeScaleController &t")]
        private static void ShowMe()
        {
            if (_isShown)
                return;
            var window = GetWindow<TimeScaleHelper>();
            window.titleContent = new GUIContent("Time scale controller");
            _isShown = true;
            window.Show();
        }

        private void OnDestroy()
        {
            _isShown = false;
        }

        private void OnGUI()
        {
            HandleKeyboard();
            GUILayout.Label($"Scale is {_sliderValue.ToString("n2")}", GUILayout.ExpandWidth(true));
            _previousSliderValue = _sliderValue;
            GUILayout.BeginHorizontal();
            _sliderValue = GUILayout.HorizontalSlider(Time.timeScale, Mathf.Max(1, Time.timeScale), 0,
                GUILayout.ExpandWidth(true));
            GUILayout.EndHorizontal();
            if (Math.Abs(_previousSliderValue - _sliderValue) > float.Epsilon)
            {
                if (_manual)
                    SetTimeScale(_sliderValue);
                else
                    SetTimeScale(Mathf.Clamp(_sliderValue, 0.05f, 1f));
            }

            GUILayout.BeginHorizontal();

            if (GUILayout.Button(" ", GUILayout.Width(8))) // spacer
            {
            }

            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Soft pause", GUILayout.Width(80)))
            {
                _manual = true;
                SetTimeScale(TimeScale);
            }

            if (GUILayout.Button("Boost", GUILayout.Width(60)))
            {
                _manual = true;
                SetTimeScale(2);
            }

            if (GUILayout.Button("Norm", GUILayout.Width(60)))
            {
                _manual = false;
                SetTimeScale(1);
            }

            GUILayout.EndHorizontal();

            // keep as example

            // GUILayout.BeginHorizontal();
            // var sert = GUILayout.TextField("JustExample1");
            // if (GUILayout.Button("JustExample1"))
            // {
            //
            // }
            // GUILayout.EndHorizontal();
        }

        private void HandleKeyboard()
        {
            Event current = Event.current;
            //MLogger.LogError(current.type);
            if (current.type != EventType.KeyDown)
                return;

            switch (current.keyCode)
            {
                case KeyCode.F2:
                    HandlePause();
                    break;
            }
        }

        private void HandlePause()
        {
            if (EditorApplication.isPlaying)
                Debug.Break();
        }
    }
}
#endif