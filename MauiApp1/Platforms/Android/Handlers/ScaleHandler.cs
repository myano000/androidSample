using Android.Views;
using MauiApp1;

namespace MauiApp1.Platforms.Android.Handlers
{
    public class ScaleHandler : Java.Lang.Object, ScaleGestureDetector.IOnScaleGestureListener
    {
        private readonly IGestureView _gestureView;

        public ScaleHandler(IGestureView gestureView)
        {
            _gestureView = gestureView;
        }

        public bool OnScale(ScaleGestureDetector detector)
        {
            return true;
        }

        public bool OnScaleBegin(ScaleGestureDetector detector)
        {
            return true;
        }

        public void OnScaleEnd(ScaleGestureDetector detector)
        {
        }
    }
}