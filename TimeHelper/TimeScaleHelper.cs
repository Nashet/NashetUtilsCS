#if UNITY_EDITOR

using System;
using UnityEditor;
using UnityEngine;

namespace NashUtilsCs.TimeHelper
{
    public class TimeScaleHelper : EditorWindow
    {
        private const float SOFT_PAUSE_SCALE = 0.00001f;
        private static bool _softPauseEnabled;
        private static float _originalTimeScale;
        private float _sliderValue = 1f;
        private bool _manual;
        private float _previousSliderValue;
        private bool _lags;

        public static void SwitchSoftPause()
        {
            _softPauseEnabled = !_softPauseEnabled;
            if (_softPauseEnabled)
            {
                _originalTimeScale = Time.timeScale;
                SetTimeScale(SOFT_PAUSE_SCALE);
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

        [MenuItem("NashUtils/TimeScaleController &t")]
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

            // just spacers:
            GUILayout.BeginHorizontal();
            GUILayout.Label($"", GUILayout.ExpandWidth(true));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label($"FPS: {1.0f / Time.deltaTime:F2}", GUILayout.ExpandWidth(true));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Soft pause", GUILayout.Width(80)))
            {
                _manual = true;
                SetTimeScale(SOFT_PAUSE_SCALE);
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

            if (GUILayout.Button("On/off lags ", GUILayout.Width(80))) // spacer
            {
                _lags = !_lags;
            }
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

        private void Update()
        {
            if (_lags)
            {
                string fr = "";
                for (int i = 0; i < 10000; i++)
                {
                    fr += i.ToString();
                }
            }
        }
    }
}
#endif