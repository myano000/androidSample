using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using MauiApp1.Behaviors;
using MauiApp1.Models;

namespace MauiApp1.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged, IDisposable
    {
        private readonly CompositeDisposable _disposables = new();
        private int _count = 0;
        private string _displayText = "Touch events will appear here";
        private string _lastGesture = "None";

        public string DisplayText
        {
            get => _displayText;
            private set
            {
                _displayText = value;
                OnPropertyChanged();
            }
        }

        public string LastGesture
        {
            get => _lastGesture;
            private set
            {
                _lastGesture = value;
                OnPropertyChanged();
            }
        }

        public int Count
        {
            get => _count;
            private set
            {
                _count = value;
                OnPropertyChanged();
            }
        }

        public void AttachTouchBehavior(ObservableTouchBehavior touchBehavior)
        {
            // 基本的なタッチイベント
            _disposables.Add(touchBehavior.TouchDown.Subscribe(OnTouchDown));

            _disposables.Add(touchBehavior.TouchMove
                .Throttle(TimeSpan.FromMilliseconds(50)) // 20FPS制限
                .Subscribe(OnTouchMove));

            _disposables.Add(touchBehavior.TouchUp.Subscribe(OnTouchUp));

            // タップイベント
            _disposables.Add(touchBehavior.Tap.Subscribe(OnTap));

            // 長押しイベント
            _disposables.Add(touchBehavior.LongPress.Subscribe(OnLongPress));

            // ダブルタップイベント
            _disposables.Add(touchBehavior.DoubleTap.Subscribe(OnDoubleTap));

            // スワイプイベント
            _disposables.Add(touchBehavior.Swipe.Subscribe(OnSwipe));
        }

        private void OnTouchDown(TouchInfo touch)
        {
            DisplayText = $"Touch Down at ({touch.Position.X:F1}, {touch.Position.Y:F1})";
            LastGesture = "Touch Down";
        }

        private void OnTouchMove(TouchInfo touch)
        {
            DisplayText = $"Touch Move at ({touch.Position.X:F1}, {touch.Position.Y:F1})";
            LastGesture = "Touch Move";
        }

        private void OnTouchUp(TouchInfo touch)
        {
            DisplayText = $"Touch Up at ({touch.Position.X:F1}, {touch.Position.Y:F1})";
            LastGesture = "Touch Up";
        }

        private void OnTap(TouchInfo touch)
        {
            Count++;
            DisplayText = $"Tap #{Count} at ({touch.Position.X:F1}, {touch.Position.Y:F1})";
            LastGesture = "Tap";
        }

        private void OnLongPress(TouchInfo touch)
        {
            DisplayText = $"Long Press detected at ({touch.Position.X:F1}, {touch.Position.Y:F1})";
            LastGesture = "Long Press";
        }

        private void OnDoubleTap(TouchInfo touch)
        {
            DisplayText = $"Double Tap detected at ({touch.Position.X:F1}, {touch.Position.Y:F1})";
            LastGesture = "Double Tap";
        }

        private void OnSwipe(SwipeInfo swipe)
        {
            DisplayText = $"Swipe {swipe.Direction} - Distance: {swipe.Distance:F1}px, Duration: {swipe.Duration.TotalMilliseconds:F0}ms";
            LastGesture = $"Swipe {swipe.Direction}";
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}