using Android.Views;
using MauiApp1;
using System.Diagnostics;
using Android.Util;

namespace MauiApp1.Platforms.Android.Handlers
{
    public class GestureHandler : Java.Lang.Object, GestureDetector.IOnGestureListener
    {
        private readonly GestureView _gestureView;
        private const string TAG = "GestureHandler";

        public GestureHandler(GestureView gestureView)
        {
            _gestureView = gestureView;
        }

        public bool OnDown(MotionEvent e)
        {
            if (e != null)
            {
                string logMessage = $"[{DateTime.Now:HH:mm:ss.fff}] [GestureHandler] OnDown: Action={e.Action}, X={e.GetX():F1}, Y={e.GetY():F1}, Pointers={e.PointerCount}";
                Debug.WriteLine(logMessage);
                Log.Debug(TAG, logMessage);
            }
            return true;
        }

        public bool OnFling(MotionEvent? e1, MotionEvent e2, float velocityX, float velocityY)
        {
            if (e2 == null) return false;

            string logMessage = $"[{DateTime.Now:HH:mm:ss.fff}] [GestureHandler] OnFling: Start=({e1?.GetX():F1},{e1?.GetY():F1}), End=({e2.GetX():F1},{e2.GetY():F1}), Velocity=({velocityX:F0},{velocityY:F0}), Pointers={e2.PointerCount}";
            Debug.WriteLine(logMessage);
            Log.Debug(TAG, logMessage);

            return true;
        }

        public void OnLongPress(MotionEvent e)
        {
            if (e == null) return;

            string logMessage = $"[{DateTime.Now:HH:mm:ss.fff}] [GestureHandler] OnLongPress: Action={e.Action}, X={e.GetX():F1}, Y={e.GetY():F1}, Pointers={e.PointerCount}";
            Debug.WriteLine(logMessage);
            Log.Debug(TAG, logMessage);
        }

        public bool OnScroll(MotionEvent? e1, MotionEvent e2, float distanceX, float distanceY)
        {
            if (e2 == null) return false;

            string logMessage = $"[{DateTime.Now:HH:mm:ss.fff}] [GestureHandler] OnScroll: Start=({e1?.GetX():F1},{e1?.GetY():F1}), Current=({e2.GetX():F1},{e2.GetY():F1}), Distance=({distanceX:F1},{distanceY:F1}), Pointers={e2.PointerCount}";
            Debug.WriteLine(logMessage);
            Log.Debug(TAG, logMessage);

            return true;
        }

        public void OnShowPress(MotionEvent e)
        {
            if (e != null)
            {
                string logMessage = $"[{DateTime.Now:HH:mm:ss.fff}] [GestureHandler] OnShowPress: Action={e.Action}, X={e.GetX():F1}, Y={e.GetY():F1}, Pointers={e.PointerCount}";
                Debug.WriteLine(logMessage);
                Log.Debug(TAG, logMessage);
            }
        }

        public bool OnSingleTapUp(MotionEvent e)
        {
            if (e != null)
            {
                string logMessage = $"[{DateTime.Now:HH:mm:ss.fff}] [GestureHandler] OnSingleTapUp: Action={e.Action}, X={e.GetX():F1}, Y={e.GetY():F1}, Pointers={e.PointerCount}";
                Debug.WriteLine(logMessage);
                Log.Debug(TAG, logMessage);
            }
            return false;
        }
    }
}