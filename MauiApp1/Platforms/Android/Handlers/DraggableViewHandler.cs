using Android.Content;
using Android.Views;
using Microsoft.Maui.Handlers;
using AView = Android.Views.View;

namespace MauiApp1.Platforms.Android.Handlers
{
    public class DraggableViewHandler : ViewHandler<IDraggableView, DraggableNativeView>
    {
        public static IPropertyMapper<IDraggableView, DraggableViewHandler> Mapper = new PropertyMapper<IDraggableView, DraggableViewHandler>(ViewMapper)
        {
            [nameof(IDraggableView.IsDraggable)] = MapIsDraggable,
            [nameof(IDraggableView.DragX)] = MapDragX,
            [nameof(IDraggableView.DragY)] = MapDragY,
        };

        public DraggableViewHandler() : base(Mapper)
        {
        }

        protected override DraggableNativeView CreatePlatformView()
        {
            return new DraggableNativeView(Context, VirtualView);
        }

        protected override void ConnectHandler(DraggableNativeView platformView)
        {
            base.ConnectHandler(platformView);
            platformView.Connect();
        }

        protected override void DisconnectHandler(DraggableNativeView platformView)
        {
            base.DisconnectHandler(platformView);
            platformView.Disconnect();
        }

        public static void MapIsDraggable(DraggableViewHandler handler, IDraggableView draggableView)
        {
            handler.PlatformView?.UpdateDraggable(draggableView.IsDraggable);
        }

        public static void MapDragX(DraggableViewHandler handler, IDraggableView draggableView)
        {
            handler.PlatformView?.UpdatePosition((float)draggableView.DragX, (float)draggableView.DragY);
        }

        public static void MapDragY(DraggableViewHandler handler, IDraggableView draggableView)
        {
            handler.PlatformView?.UpdatePosition((float)draggableView.DragX, (float)draggableView.DragY);
        }
    }

    public class DraggableNativeView : AView
    {
        private readonly IDraggableView _draggableView;
        private bool _isDragging = false;
        private float _lastRawX = 0;
        private float _lastRawY = 0;

        public DraggableNativeView(Context context, IDraggableView draggableView) : base(context)
        {
            _draggableView = draggableView;
            SetBackgroundColor(global::Android.Graphics.Color.Red);
        }

        public void Connect()
        {
        }

        public void Disconnect()
        {
        }

        public override bool OnTouchEvent(MotionEvent? e)
        {
            if (e == null || !_draggableView.IsDraggable) return false;

            switch (e.Action)
            {
                case MotionEventActions.Down:
                    _isDragging = true;
                    _lastRawX = e.RawX;
                    _lastRawY = e.RawY;
                    return true;

                case MotionEventActions.Move:
                    if (_isDragging)
                    {
                        float currentRawX = e.RawX;
                        float currentRawY = e.RawY;
                        float deltaX = currentRawX - _lastRawX;
                        float deltaY = currentRawY - _lastRawY;

                        TranslationX += deltaX;
                        TranslationY += deltaY;

                        _lastRawX = currentRawX;
                        _lastRawY = currentRawY;
                        return true;
                    }
                    break;

                case MotionEventActions.Up:
                case MotionEventActions.Cancel:
                    if (_isDragging)
                    {
                        _isDragging = false;
                        return true;
                    }
                    break;
            }

            return base.OnTouchEvent(e);
        }

        public void UpdateDraggable(bool isDraggable)
        {
        }

        public void UpdatePosition(float x, float y)
        {
            TranslationX = x;
            TranslationY = y;
        }
    }
}