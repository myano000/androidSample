using Android.Views;
using MauiApp1;

namespace MauiApp1.Platforms.Android.Handlers
{
    public class GestureHandler : Java.Lang.Object, GestureDetector.IOnGestureListener
    {
        private readonly IGestureView _gestureView;

        public GestureHandler(IGestureView gestureView)
        {
            _gestureView = gestureView;
        }

        public bool OnDown(MotionEvent e)
        {
            return true;
        }

        public bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
        {
            if (e2 == null) return false;
            return true;
        }

        public void OnLongPress(MotionEvent e)
        {
            if (e == null) return;
        }

        public bool OnScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
        {
            if (e2 == null) return false;


            return true;
        }

        public void OnShowPress(MotionEvent e)
        {
        }

        public bool OnSingleTapUp(MotionEvent e)
        {
            return false;
        }
    }
}