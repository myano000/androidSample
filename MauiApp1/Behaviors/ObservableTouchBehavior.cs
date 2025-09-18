using System.Reactive.Subjects;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using MauiApp1.Models;

namespace MauiApp1.Behaviors
{
    public class ObservableTouchBehavior : Behavior<View>
    {
        private readonly Subject<TouchInfo> _touchSubject = new();
        private readonly CompositeDisposable _disposables = new();
        private DateTime _lastTouchDown;
        private Point _lastTouchPosition;

        public IObservable<TouchInfo> TouchDown =>
            _touchSubject.Where(t => t.Action == TouchAction.Down);

        public IObservable<TouchInfo> TouchUp =>
            _touchSubject.Where(t => t.Action == TouchAction.Up);

        public IObservable<TouchInfo> TouchMove =>
            _touchSubject.Where(t => t.Action == TouchAction.Move);

        public IObservable<TouchInfo> Tap =>
            _touchSubject.Where(t => t.Action == TouchAction.Tap);

        public IObservable<TouchInfo> AllTouches =>
            _touchSubject.AsObservable();

        // 長押し (500ms以上のタッチ)
        public IObservable<TouchInfo> LongPress =>
            TouchDown
                .SelectMany(down =>
                    Observable.Timer(TimeSpan.FromMilliseconds(500))
                        .TakeUntil(TouchUp)
                        .Select(_ => down));

        // ダブルタップ (300ms以内の連続タップ)
        public IObservable<TouchInfo> DoubleTap =>
            Tap
                .Buffer(TimeSpan.FromMilliseconds(300), 2)
                .Where(taps => taps.Count == 2)
                .Select(taps => taps.Last());

        // スワイプジェスチャー
        public IObservable<SwipeInfo> Swipe =>
            TouchDown
                .SelectMany(down =>
                    TouchUp
                        .Take(1)
                        .Select(up => new SwipeInfo
                        {
                            StartPosition = down.Position,
                            EndPosition = up.Position,
                            Direction = CalculateDirection(down.Position, up.Position),
                            Distance = CalculateDistance(down.Position, up.Position),
                            Duration = up.Timestamp - down.Timestamp,
                            Timestamp = up.Timestamp
                        }))
                .Where(swipe => swipe.Distance > 50); // 最小スワイプ距離

        protected override void OnAttachedTo(View bindable)
        {
            base.OnAttachedTo(bindable);

            AttachTapGesture(bindable);
            AttachPanGesture(bindable);

#if ANDROID
            AttachAndroidSpecificTouch(bindable);
#endif
        }

        private void AttachTapGesture(View view)
        {
            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += (s, e) =>
            {
                var position = e.GetPosition(view);
                _touchSubject.OnNext(new TouchInfo
                {
                    Action = TouchAction.Tap,
                    Position = position ?? Point.Zero,
                    Timestamp = DateTime.Now,
                    Source = view
                });
            };
            view.GestureRecognizers.Add(tapGesture);
        }

        private void AttachPanGesture(View view)
        {
            var panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += (s, e) =>
            {
                var action = e.StatusType switch
                {
                    GestureStatus.Started => TouchAction.Down,
                    GestureStatus.Running => TouchAction.Move,
                    GestureStatus.Completed => TouchAction.Up,
                    GestureStatus.Canceled => TouchAction.Cancel,
                    _ => TouchAction.Move
                };

                var currentPosition = new Point(e.TotalX, e.TotalY);

                if (action == TouchAction.Down)
                {
                    _lastTouchDown = DateTime.Now;
                    _lastTouchPosition = currentPosition;
                }

                _touchSubject.OnNext(new TouchInfo
                {
                    Action = action,
                    Position = currentPosition,
                    Timestamp = DateTime.Now,
                    Source = view
                });
            };
            view.GestureRecognizers.Add(panGesture);
        }

#if ANDROID
        private void AttachAndroidSpecificTouch(View view)
        {
            view.HandlerChanged += (s, e) =>
            {
                if (view.Handler?.PlatformView is Android.Views.View androidView)
                {
                    androidView.Touch += OnAndroidTouch;
                }
            };
        }

        private void OnAndroidTouch(object? sender, Android.Views.View.TouchEventArgs e)
        {
            var action = e.Event?.Action switch
            {
                Android.Views.MotionEventActions.Down => TouchAction.Down,
                Android.Views.MotionEventActions.Up => TouchAction.Up,
                Android.Views.MotionEventActions.Move => TouchAction.Move,
                Android.Views.MotionEventActions.Cancel => TouchAction.Cancel,
                _ => TouchAction.Move
            };

            _touchSubject.OnNext(new TouchInfo
            {
                Action = action,
                Position = new Point(e.Event?.GetX() ?? 0, e.Event?.GetY() ?? 0),
                Pressure = e.Event?.Pressure ?? 1.0f,
                Timestamp = DateTime.Now,
                Source = sender as View,
                MultiTouchCount = e.Event?.PointerCount ?? 1,
                PlatformData = e.Event
            });
        }
#endif

        private static Models.SwipeDirection CalculateDirection(Point start, Point end)
        {
            var deltaX = end.X - start.X;
            var deltaY = end.Y - start.Y;

            if (Math.Abs(deltaX) > Math.Abs(deltaY))
            {
                return deltaX > 0 ? Models.SwipeDirection.Right : Models.SwipeDirection.Left;
            }
            else
            {
                return deltaY > 0 ? Models.SwipeDirection.Down : Models.SwipeDirection.Up;
            }
        }

        private static double CalculateDistance(Point start, Point end)
        {
            var deltaX = end.X - start.X;
            var deltaY = end.Y - start.Y;
            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }

        protected override void OnDetachingFrom(View bindable)
        {
            _disposables.Dispose();
            _touchSubject.Dispose();
            base.OnDetachingFrom(bindable);
        }
    }
}