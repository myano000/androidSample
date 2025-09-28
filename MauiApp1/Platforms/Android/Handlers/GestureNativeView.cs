using Android.Content;
using Android.Nfc;
using Android.Views;
using AView = Android.Views.View;

namespace MauiApp1.Platforms.Android.Handlers
{
    public class GestureNativeView : AView
    {
        private readonly GestureView _gestureView;
        private GestureDetector? _gestureDetector;
        private ScaleGestureDetector? _scaleGestureDetector;
        private DateTime _lastTwoFingerTapTime = DateTime.MinValue;

        private readonly GestureHandler _gestureHandler;
        private readonly DoubleTapHandler _doubleTapHandler;
        private readonly ScaleHandler _scaleHandler;
        private CustumGestureHandler? _custumGestureHandler;

        public GestureNativeView(Context context, GestureView gestureView) : base(context)
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
            _custumGestureHandler = new CustumGestureHandler(_gestureDetector);
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
            bool handled = false;

            if (_custumGestureHandler != null) 
            {
                return _custumGestureHandler.OnTouchEvent(e);
            }
            return handled || base.OnTouchEvent(e);
        }


    }

}