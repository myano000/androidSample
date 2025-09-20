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
            [nameof(IGestureView.IsGestureEnabled)] = MapIsGestureEnabled,
            [nameof(IGestureView.IsScaleEnabled)] = MapIsScaleEnabled,
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

        public static void MapIsGestureEnabled(GestureViewHandler handler, IGestureView gestureView)
        {
            handler.PlatformView?.UpdateGestureEnabled(gestureView.IsGestureEnabled);
        }

        public static void MapIsScaleEnabled(GestureViewHandler handler, IGestureView gestureView)
        {
            handler.PlatformView?.UpdateScaleEnabled(gestureView.IsScaleEnabled);
        }
    }

    public class GestureNativeView : AView, GestureDetector.IOnGestureListener, ScaleGestureDetector.IOnScaleGestureListener
    {
        private readonly IGestureView _gestureView;
        private GestureDetector? _gestureDetector;
        private ScaleGestureDetector? _scaleGestureDetector;

        public GestureNativeView(Context context, IGestureView gestureView) : base(context)
        {
            _gestureView = gestureView;
            SetBackgroundColor(global::Android.Graphics.Color.Transparent);
        }

        public void Connect()
        {
            _gestureDetector = new GestureDetector(Context, this);
            _scaleGestureDetector = new ScaleGestureDetector(Context, this);
        }

        public void Disconnect()
        {
            _gestureDetector?.Dispose();
            _scaleGestureDetector?.Dispose();
            _gestureDetector = null;
            _scaleGestureDetector = null;
        }

        public override bool OnTouchEvent(MotionEvent? e)
        {
            if (e == null) return false;

            bool handled = false;

            if (_gestureView.IsScaleEnabled && _scaleGestureDetector != null)
            {
                handled = _scaleGestureDetector.OnTouchEvent(e);
            }

            if (_gestureView.IsGestureEnabled && _gestureDetector != null)
            {
                handled = _gestureDetector.OnTouchEvent(e) || handled;
            }

            return handled || base.OnTouchEvent(e);
        }

        public void UpdateGestureEnabled(bool enabled)
        {
            // Gesture detector is recreated when needed
        }

        public void UpdateScaleEnabled(bool enabled)
        {
            // Scale detector is recreated when needed
        }

        // GestureDetector.IOnGestureListener implementation
        public bool OnDown(MotionEvent e)
        {
            return true;
        }

        public bool OnFling(MotionEvent? e1, MotionEvent e2, float velocityX, float velocityY)
        {
            if (e1 == null || e2 == null) return false;

            var args = new GestureEventArgs
            {
                X = e2.GetX(),
                Y = e2.GetY(),
                DeltaX = e2.GetX() - e1.GetX(),
                DeltaY = e2.GetY() - e1.GetY(),
                VelocityX = velocityX,
                VelocityY = velocityY,
                Type = GestureType.Fling
            };

            MainThread.BeginInvokeOnMainThread(() => _gestureView.OnFling(args));
            return true;
        }

        public void OnLongPress(MotionEvent e)
        {
            if (e == null) return;

            var args = new GestureEventArgs
            {
                X = e.GetX(),
                Y = e.GetY(),
                Type = GestureType.LongPress
            };

            MainThread.BeginInvokeOnMainThread(() => _gestureView.OnLongPress(args));
        }

        public bool OnScroll(MotionEvent? e1, MotionEvent e2, float distanceX, float distanceY)
        {
            if (e1 == null || e2 == null) return false;

            var args = new GestureEventArgs
            {
                X = e2.GetX(),
                Y = e2.GetY(),
                DeltaX = -distanceX,
                DeltaY = -distanceY,
                Type = GestureType.Pan
            };

            MainThread.BeginInvokeOnMainThread(() => _gestureView.OnPan(args));
            return true;
        }

        public void OnShowPress(MotionEvent e)
        {
            // Not used
        }

        public bool OnSingleTapUp(MotionEvent e)
        {
            if (e == null) return false;

            var args = new GestureEventArgs
            {
                X = e.GetX(),
                Y = e.GetY(),
                Type = GestureType.Tap
            };

            MainThread.BeginInvokeOnMainThread(() => _gestureView.OnTap(args));
            return true;
        }

        // ScaleGestureDetector.IOnScaleGestureListener implementation
        public bool OnScale(ScaleGestureDetector detector)
        {
            if (detector == null) return false;

            var args = new ScaleEventArgs
            {
                ScaleFactor = detector.ScaleFactor,
                FocusX = detector.FocusX,
                FocusY = detector.FocusY,
                State = ScaleState.Scale
            };

            MainThread.BeginInvokeOnMainThread(() => _gestureView.OnScale(args));
            return true;
        }

        public bool OnScaleBegin(ScaleGestureDetector detector)
        {
            if (detector == null) return false;

            var args = new ScaleEventArgs
            {
                ScaleFactor = detector.ScaleFactor,
                FocusX = detector.FocusX,
                FocusY = detector.FocusY,
                State = ScaleState.Begin
            };

            MainThread.BeginInvokeOnMainThread(() => _gestureView.OnScaleBegin(args));
            return true;
        }

        public void OnScaleEnd(ScaleGestureDetector detector)
        {
            if (detector == null) return;

            var args = new ScaleEventArgs
            {
                ScaleFactor = detector.ScaleFactor,
                FocusX = detector.FocusX,
                FocusY = detector.FocusY,
                State = ScaleState.End
            };

            MainThread.BeginInvokeOnMainThread(() => _gestureView.OnScaleEnd(args));
        }
    }
}