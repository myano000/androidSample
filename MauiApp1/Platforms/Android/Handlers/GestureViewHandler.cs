using Android.Content;
using Android.Views;
using Microsoft.Maui.Handlers;
using AView = Android.Views.View;

namespace MauiApp1.Platforms.Android.Handlers
{
    public class GestureViewHandler : ViewHandler<IGestureView, GestureNativeView>
    {
        public static IPropertyMapper<IGestureView, GestureViewHandler> Mapper = new PropertyMapper<IGestureView, GestureViewHandler>(ViewMapper)
        {
        };

        public GestureViewHandler() : base(Mapper)
        {
        }

        protected override GestureNativeView CreatePlatformView()
        {
            return new GestureNativeView(Context, VirtualView);
        }

        protected override void ConnectHandler(GestureNativeView platformView)
        {
            base.ConnectHandler(platformView);
            platformView.Connect();
        }

        protected override void DisconnectHandler(GestureNativeView platformView)
        {
            base.DisconnectHandler(platformView);
            platformView.Disconnect();
        }
    }

    public class GestureNativeView : AView
    {
        private readonly IGestureView _gestureView;
        private GestureDetector? _gestureDetector;
        private ScaleGestureDetector? _scaleGestureDetector;
        private DateTime _lastTwoFingerTapTime = DateTime.MinValue;

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

            bool handled = false;

            if (_scaleGestureDetector != null)
            {

                handled = _scaleGestureDetector.OnTouchEvent(e) || handled;
            }

            if(_gestureDetector != null)
            {
                handled = _gestureDetector.OnTouchEvent(e) || handled;

            }


            return handled || base.OnTouchEvent(e);
        }
    }
}