namespace MauiApp1.Models
{
    public enum SwipeDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    public class SwipeInfo
    {
        public Point StartPosition { get; set; }
        public Point EndPosition { get; set; }
        public SwipeDirection Direction { get; set; }
        public double Distance { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime Timestamp { get; set; }
    }
}