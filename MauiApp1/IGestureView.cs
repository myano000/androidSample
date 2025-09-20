using System.Windows.Input;

namespace MauiApp1
{
    public interface IGestureView : IView
    {
        // Gesture events
        event EventHandler<GestureEventArgs>? Pan;
        event EventHandler<GestureEventArgs>? Tap;
        event EventHandler<GestureEventArgs>? DoubleTap;
        event EventHandler<GestureEventArgs>? TwoFingerTap;
        event EventHandler<GestureEventArgs>? LongPress;
        event EventHandler<GestureEventArgs>? Fling;

        // Scale events
        event EventHandler<ScaleEventArgs>? ScaleBegin;
        event EventHandler<ScaleEventArgs>? Scale;
        event EventHandler<ScaleEventArgs>? ScaleEnd;

        // Commands
        ICommand? PanCommand { get; set; }
        ICommand? TapCommand { get; set; }
        ICommand? DoubleTapCommand { get; set; }
        ICommand? TwoFingerTapCommand { get; set; }
        ICommand? LongPressCommand { get; set; }
        ICommand? FlingCommand { get; set; }
        ICommand? ScaleCommand { get; set; }

        // Properties
        bool IsGestureEnabled { get; set; }
        bool IsScaleEnabled { get; set; }

        // Method definitions for handlers to call
        void OnPan(GestureEventArgs args);
        void OnTap(GestureEventArgs args);
        void OnDoubleTap(GestureEventArgs args);
        void OnTwoFingerTap(GestureEventArgs args);
        void OnLongPress(GestureEventArgs args);
        void OnFling(GestureEventArgs args);
        void OnScaleBegin(ScaleEventArgs args);
        void OnScale(ScaleEventArgs args);
        void OnScaleEnd(ScaleEventArgs args);
    }

    public class GestureEventArgs : EventArgs
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float DeltaX { get; set; }
        public float DeltaY { get; set; }
        public float VelocityX { get; set; }
        public float VelocityY { get; set; }
        public int FingerCount { get; set; } = 1;
        public GestureType Type { get; set; }
    }

    public class ScaleEventArgs : EventArgs
    {
        public float ScaleFactor { get; set; }
        public float FocusX { get; set; }
        public float FocusY { get; set; }
        public ScaleState State { get; set; }
    }

    public enum GestureType
    {
        Pan,
        Tap,
        DoubleTap,
        TwoFingerTap,
        LongPress,
        Fling
    }

    public enum ScaleState
    {
        Begin,
        Scale,
        End
    }
}