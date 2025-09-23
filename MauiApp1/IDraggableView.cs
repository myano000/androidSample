using System.Windows.Input;

namespace MauiApp1
{
    public interface IDraggableView : IView
    {
        double DragX { get; set; }
        double DragY { get; set; }
        bool IsDraggable { get; set; }

        event EventHandler<DraggableTouchEventArgs>? DragStarted;
        event EventHandler<DraggableTouchEventArgs>? DragMoved;
        event EventHandler<DraggableTouchEventArgs>? DragEnded;

        ICommand? DragStartedCommand { get; set; }
        ICommand? DragMovedCommand { get; set; }
        ICommand? DragEndedCommand { get; set; }

        void OnDragStarted(DraggableTouchEventArgs args);
        void OnDragMoved(DraggableTouchEventArgs args);
        void OnDragEnded(DraggableTouchEventArgs args);
    }

    public class DraggableTouchEventArgs : EventArgs
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double DeltaX { get; set; }
        public double DeltaY { get; set; }
        public DraggableTouchState State { get; set; }
    }

    public enum DraggableTouchState
    {
        Started,
        Moving,
        Ended
    }
}