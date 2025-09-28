using Android.Views;
using MauiApp1;
using System.Diagnostics;
using Android.Util;

namespace MauiApp1.Platforms.Android.Handlers
{
    public class DoubleTapHandler : Java.Lang.Object, GestureDetector.IOnDoubleTapListener
    {
        private readonly GestureView _gestureView;
        private const string TAG = "DoubleTapHandler";

        public DoubleTapHandler(GestureView gestureView)
        {
            _gestureView = gestureView;
        }

        public bool OnDoubleTap(MotionEvent e)
        {
            if (e == null) return false;

            string logMessage = $"[{DateTime.Now:HH:mm:ss.fff}] [DoubleTapHandler] OnDoubleTap: Action={e.Action}, X={e.GetX():F1}, Y={e.GetY():F1}, Pointers={e.PointerCount}";
            Debug.WriteLine(logMessage);
            Log.Debug(TAG, logMessage);

            return true;
        }

        public bool OnDoubleTapEvent(MotionEvent e)
        {
            if (e != null)
            {
                string logMessage = $"[{DateTime.Now:HH:mm:ss.fff}] [DoubleTapHandler] OnDoubleTapEvent: Action={e.Action}, X={e.GetX():F1}, Y={e.GetY():F1}, Pointers={e.PointerCount}";
                Debug.WriteLine(logMessage);
                Log.Debug(TAG, logMessage);
            }
            return false;
        }

        public bool OnSingleTapConfirmed(MotionEvent e)
        {
            if (e == null) return false;

            string logMessage = $"[{DateTime.Now:HH:mm:ss.fff}] [DoubleTapHandler] OnSingleTapConfirmed: Action={e.Action}, X={e.GetX():F1}, Y={e.GetY():F1}, Pointers={e.PointerCount}";
            Debug.WriteLine(logMessage);
            Log.Debug(TAG, logMessage);

            return true;
        }
    }
}