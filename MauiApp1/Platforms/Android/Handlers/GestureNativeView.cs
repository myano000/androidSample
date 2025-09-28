using Android.Content;
using Android.Views;
using AView = Android.Views.View;
using System.Diagnostics;
using Android.Util;

namespace MauiApp1.Platforms.Android.Handlers
{
    public class GestureNativeView : AView
    {
        private readonly IGestureView _gestureView;
        private GestureDetector? _gestureDetector;
        private ScaleGestureDetector? _scaleGestureDetector;
        private DateTime _lastTwoFingerTapTime = DateTime.MinValue;
        private const string TAG = "GestureNativeView";

        private readonly GestureHandler _gestureHandler;
        private readonly DoubleTapHandler _doubleTapHandler;
        private readonly ScaleHandler _scaleHandler;

        public GestureNativeView(Context context, IGestureView gestureView) : base(context)
        {
            _gestureView = gestureView;

            _gestureHandler = new GestureHandler(gestureView);
            _doubleTapHandler = new DoubleTapHandler(gestureView);
            _scaleHandler = new ScaleHandler(gestureView);

            SetBackgroundColor(global::Android.Graphics.Color.Transparent);
            Clickable = true;
            Focusable = true;
        }

        public void Connect()
        {
            _gestureDetector = new GestureDetector(Context, _gestureHandler);
            _gestureDetector.SetOnDoubleTapListener(_doubleTapHandler);
            _scaleGestureDetector = new ScaleGestureDetector(Context, _scaleHandler);
        }

        public void Disconnect()
        {
            _gestureDetector?.Dispose();
            _scaleGestureDetector?.Dispose();
            _gestureHandler?.Dispose();
            _doubleTapHandler?.Dispose();
            _scaleHandler?.Dispose();
            _gestureDetector = null;
            _scaleGestureDetector = null;
        }

        public override bool OnTouchEvent(MotionEvent? e)
        {
            if (e == null) return false;

            // MotionEvent‚ÌÚ×ƒƒO
            string actionName = GetActionName(e.ActionMasked);
            string pointerInfo = GetPointerInfo(e);
            string logMessage = $"[{DateTime.Now:HH:mm:ss.fff}] [GestureNativeView] OnTouchEvent: Action={actionName}, {pointerInfo}";
            Debug.WriteLine(logMessage);
            Log.Debug(TAG, logMessage);

            bool handled = false;

            // Two-finger tap detection logging
            //if (e.PointerCount == 2)
            //{
            //    if (e.ActionMasked == MotionEventActions.Down || e.ActionMasked == MotionEventActions.PointerDown)
            //    {
            //        var now = DateTime.Now;
            //        var timeDiff = now - _lastTwoFingerTapTime;

            //        string twoFingerLog = $"[{DateTime.Now:HH:mm:ss.fff}] [GestureNativeView] TwoFingerDetection: TimeDiff={timeDiff.TotalMilliseconds:F0}ms, X={e.GetX():F1}, Y={e.GetY():F1}";
            //        Debug.WriteLine(twoFingerLog);
            //        Log.Debug(TAG, twoFingerLog);

            //        _lastTwoFingerTapTime = now;
            //    }
            //}

            //if (_scaleGestureDetector != null)
            //{
            //    bool scaleHandled = _scaleGestureDetector.OnTouchEvent(e);
            //    if (scaleHandled)
            //    {
            //        string scaleLog = $"[{DateTime.Now:HH:mm:ss.fff}] [GestureNativeView] ScaleGestureDetector handled event";
            //        Debug.WriteLine(scaleLog);
            //        Log.Debug(TAG, scaleLog);
            //    }
            //    handled = scaleHandled || handled;
            //}

            if (_gestureDetector != null)
            {
                bool gestureHandled = _gestureDetector.OnTouchEvent(e);
                if (gestureHandled)
                {
                    string gestureLog = $"[{DateTime.Now:HH:mm:ss.fff}] [GestureNativeView] GestureDetector handled event";
                    Debug.WriteLine(gestureLog);
                    Log.Debug(TAG, gestureLog);
                }
                handled = gestureHandled || handled;
            }

            string resultLog = $"[{DateTime.Now:HH:mm:ss.fff}] [GestureNativeView] OnTouchEvent Result: handled={handled}";
            Debug.WriteLine(resultLog);
            Log.Debug(TAG, resultLog);

            return handled || base.OnTouchEvent(e);
        }

        private string GetActionName(MotionEventActions action)
        {
            return action switch
            {
                MotionEventActions.Down => "ACTION_DOWN",
                MotionEventActions.Up => "ACTION_UP",
                MotionEventActions.Move => "ACTION_MOVE",
                MotionEventActions.Cancel => "ACTION_CANCEL",
                MotionEventActions.PointerDown => "ACTION_POINTER_DOWN",
                MotionEventActions.PointerUp => "ACTION_POINTER_UP",
                _ => action.ToString()
            };
        }

        private string GetPointerInfo(MotionEvent e)
        {
            if (e.PointerCount == 1)
            {
                return $"X={e.GetX():F1}, Y={e.GetY():F1}, Pointers=1";
            }
            else
            {
                var pointers = new List<string>();
                for (int i = 0; i < e.PointerCount; i++)
                {
                    pointers.Add($"P{i}=({e.GetX(i):F1},{e.GetY(i):F1})");
                }
                return $"{string.Join(", ", pointers)}, Pointers={e.PointerCount}";
            }
        }
    }
}