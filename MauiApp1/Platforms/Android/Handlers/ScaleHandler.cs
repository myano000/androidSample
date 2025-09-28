using Android.Views;
using MauiApp1;
using System.Diagnostics;
using Android.Util;

namespace MauiApp1.Platforms.Android.Handlers
{
    public class ScaleHandler : Java.Lang.Object, ScaleGestureDetector.IOnScaleGestureListener
    {
        private readonly GestureView _gestureView;
        private const string TAG = "ScaleHandler";
        private float _previousScaleFactor = 1.0f;

        public ScaleHandler(GestureView gestureView)
        {
            _gestureView = gestureView;
        }

        public bool OnScale(ScaleGestureDetector detector)
        {
            if (detector != null)
            {
                float currentScale = detector.ScaleFactor;
                float deltaScale = currentScale - _previousScaleFactor;

                string logMessage = $"[{DateTime.Now:HH:mm:ss.fff}] [ScaleHandler] OnScale: Factor={currentScale:F3}, Focus=({detector.FocusX:F1},{detector.FocusY:F1}), Previous={_previousScaleFactor:F3}, Delta={deltaScale:F3}";
                Debug.WriteLine(logMessage);
                Log.Debug(TAG, logMessage);

                _previousScaleFactor = currentScale;
            }
            return true;
        }

        public bool OnScaleBegin(ScaleGestureDetector detector)
        {
            if (detector != null)
            {
                _previousScaleFactor = detector.ScaleFactor;

                string logMessage = $"[{DateTime.Now:HH:mm:ss.fff}] [ScaleHandler] OnScaleBegin: Factor={detector.ScaleFactor:F3}, Focus=({detector.FocusX:F1},{detector.FocusY:F1})";
                Debug.WriteLine(logMessage);
                Log.Debug(TAG, logMessage);
            }
            return true;
        }

        public void OnScaleEnd(ScaleGestureDetector detector)
        {
            if (detector != null)
            {
                string logMessage = $"[{DateTime.Now:HH:mm:ss.fff}] [ScaleHandler] OnScaleEnd: Factor={detector.ScaleFactor:F3}, Focus=({detector.FocusX:F1},{detector.FocusY:F1})";
                Debug.WriteLine(logMessage);
                Log.Debug(TAG, logMessage);
            }
        }
    }
}