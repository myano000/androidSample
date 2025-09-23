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
        private float _lastTouchX = 0;
        private float _lastTouchY = 0;
        private float _startX = 0;
        private float _startY = 0;

        public DraggableNativeView(Context context, IDraggableView draggableView) : base(context)
        {
            _draggableView = draggableView;
            SetBackgroundColor(global::Android.Graphics.Color.Red);
        }

        public void Connect()
        {
            // ドラッグ処理は OnTouchEvent で実装
        }

        public void Disconnect()
        {
            // クリーンアップが必要な場合はここで実装
        }

        public override bool OnTouchEvent(MotionEvent? e)
        {
            if (e == null || !_draggableView.IsDraggable) return false;

            switch (e.Action)
            {
                case MotionEventActions.Down:
                    _isDragging = true;
                    _lastTouchX = e.GetX();
                    _lastTouchY = e.GetY();
                    _startX = _lastTouchX;
                    _startY = _lastTouchY;

                    var startArgs = new DraggableTouchEventArgs
                    {
                        X = _lastTouchX,
                        Y = _lastTouchY,
                        DeltaX = 0,
                        DeltaY = 0,
                        State = DraggableTouchState.Started
                    };
                    MainThread.BeginInvokeOnMainThread(() => _draggableView.OnDragStarted(startArgs));
                    return true;

                case MotionEventActions.Move:
                    if (_isDragging)
                    {
                        float currentX = e.GetX();
                        float currentY = e.GetY();
                        float deltaX = currentX - _lastTouchX;
                        float deltaY = currentY - _lastTouchY;

                        // ビューの位置を更新
                        TranslationX += deltaX;
                        TranslationY += deltaY;

                        var moveArgs = new DraggableTouchEventArgs
                        {
                            X = TranslationX,
                            Y = TranslationY,
                            DeltaX = deltaX,
                            DeltaY = deltaY,
                            State = DraggableTouchState.Moving
                        };
                        MainThread.BeginInvokeOnMainThread(() => _draggableView.OnDragMoved(moveArgs));

                        _lastTouchX = currentX;
                        _lastTouchY = currentY;
                        return true;
                    }
                    break;

                case MotionEventActions.Up:
                case MotionEventActions.Cancel:
                    if (_isDragging)
                    {
                        _isDragging = false;
                        var endArgs = new DraggableTouchEventArgs
                        {
                            X = TranslationX,
                            Y = TranslationY,
                            DeltaX = e.GetX() - _startX,
                            DeltaY = e.GetY() - _startY,
                            State = DraggableTouchState.Ended
                        };
                        MainThread.BeginInvokeOnMainThread(() => _draggableView.OnDragEnded(endArgs));
                        return true;
                    }
                    break;
            }

            return base.OnTouchEvent(e);
        }

        public void UpdateDraggable(bool isDraggable)
        {
            // ドラッグ可能フラグの更新
        }

        public void UpdatePosition(float x, float y)
        {
            TranslationX = x;
            TranslationY = y;
        }
    }
}