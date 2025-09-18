namespace MauiApp1.Models
{
    public class TouchInfo
    {
        public TouchAction Action { get; set; }
        public Point Position { get; set; }
        public DateTime Timestamp { get; set; }
        public View? Source { get; set; }
        public float Pressure { get; set; } = 1.0f;
        public int MultiTouchCount { get; set; } = 1;
        public object? PlatformData { get; set; }
    }
}