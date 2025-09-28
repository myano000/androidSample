using Android.Views;
using MauiApp1;

namespace MauiApp1.Platforms.Android.Handlers
{
    public class DoubleTapHandler : Java.Lang.Object, GestureDetector.IOnDoubleTapListener
    {
        private readonly IGestureView _gestureView;

        public DoubleTapHandler(IGestureView gestureView)
        {
            _gestureView = gestureView;
        }

        public bool OnDoubleTap(MotionEvent e)
        {
            if (e == null) return false;

            return true;
        }

        public bool OnDoubleTapEvent(MotionEvent e)
        {
            return false;
        }

        public bool OnSingleTapConfirmed(MotionEvent e)
        {
            if (e == null) return false;

            return true;
        }
    }
}