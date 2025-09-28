using Android.Runtime;
using Android.Util;
using Android.Views;
using Java.Interop;
using System.Diagnostics;

namespace MauiApp1.Platforms.Android.Handlers
{
    public class CustumGestureHandler : IGestureHandler
    {
        private const string TAG = "GestureNativeView";
        private readonly GestureDetector gestureDetector;

        public CustumGestureHandler(GestureDetector gestureDetector)
        {
            this.gestureDetector = gestureDetector;
        }
        public bool OnTouchEvent(MotionEvent e)
        {
            // MotionEvent‚ÌÚ×ƒƒO
            string actionName = GetActionName(e.ActionMasked);
            string pointerInfo = GetPointerInfo(e);
            string logMessage = $"[{DateTime.Now:HH:mm:ss.fff}] [GestureNativeView] OnTouchEvent: Action={actionName}, {pointerInfo}";
            Debug.WriteLine(logMessage);
            Log.Debug(TAG, logMessage);

            bool handled = gestureDetector.OnTouchEvent(e);

            return handled;
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