using System.Windows.Input;

namespace MauiApp1
{
    public class DraggableView : View, IDraggableView
    {
        public static readonly BindableProperty DragXProperty =
            BindableProperty.Create(nameof(DragX), typeof(double), typeof(DraggableView), 0.0);

        public static readonly BindableProperty DragYProperty =
            BindableProperty.Create(nameof(DragY), typeof(double), typeof(DraggableView), 0.0);

        public static readonly BindableProperty IsDraggableProperty =
            BindableProperty.Create(nameof(IsDraggable), typeof(bool), typeof(DraggableView), true);

        public static readonly BindableProperty DragStartedCommandProperty =
            BindableProperty.Create(nameof(DragStartedCommand), typeof(ICommand), typeof(DraggableView));

        public static readonly BindableProperty DragMovedCommandProperty =
            BindableProperty.Create(nameof(DragMovedCommand), typeof(ICommand), typeof(DraggableView));

        public static readonly BindableProperty DragEndedCommandProperty =
            BindableProperty.Create(nameof(DragEndedCommand), typeof(ICommand), typeof(DraggableView));

        public double DragX
        {
            get => (double)GetValue(DragXProperty);
            set => SetValue(DragXProperty, value);
        }

        public double DragY
        {
            get => (double)GetValue(DragYProperty);
            set => SetValue(DragYProperty, value);
        }

        public bool IsDraggable
        {
            get => (bool)GetValue(IsDraggableProperty);
            set => SetValue(IsDraggableProperty, value);
        }

        public ICommand? DragStartedCommand
        {
            get => (ICommand?)GetValue(DragStartedCommandProperty);
            set => SetValue(DragStartedCommandProperty, value);
        }

        public ICommand? DragMovedCommand
        {
            get => (ICommand?)GetValue(DragMovedCommandProperty);
            set => SetValue(DragMovedCommandProperty, value);
        }

        public ICommand? DragEndedCommand
        {
            get => (ICommand?)GetValue(DragEndedCommandProperty);
            set => SetValue(DragEndedCommandProperty, value);
        }

        public event EventHandler<DraggableTouchEventArgs>? DragStarted;
        public event EventHandler<DraggableTouchEventArgs>? DragMoved;
        public event EventHandler<DraggableTouchEventArgs>? DragEnded;

        public virtual void OnDragStarted(DraggableTouchEventArgs args)
        {
            DragStarted?.Invoke(this, args);
            if (DragStartedCommand?.CanExecute(args) == true)
                DragStartedCommand.Execute(args);
        }

        public virtual void OnDragMoved(DraggableTouchEventArgs args)
        {
            DragX = args.X;
            DragY = args.Y;
            DragMoved?.Invoke(this, args);
            if (DragMovedCommand?.CanExecute(args) == true)
                DragMovedCommand.Execute(args);
        }

        public virtual void OnDragEnded(DraggableTouchEventArgs args)
        {
            DragEnded?.Invoke(this, args);
            if (DragEndedCommand?.CanExecute(args) == true)
                DragEndedCommand.Execute(args);
        }
    }
}