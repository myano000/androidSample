using System.Windows.Input;

namespace MauiApp1
{
    public class GestureView : View, IGestureView
    {
        // Bindable Properties
        public static readonly BindableProperty IsGestureEnabledProperty =
            BindableProperty.Create(nameof(IsGestureEnabled), typeof(bool), typeof(GestureView), true);

        public static readonly BindableProperty IsScaleEnabledProperty =
            BindableProperty.Create(nameof(IsScaleEnabled), typeof(bool), typeof(GestureView), true);

        public static readonly BindableProperty PanCommandProperty =
            BindableProperty.Create(nameof(PanCommand), typeof(ICommand), typeof(GestureView));

        public static readonly BindableProperty TapCommandProperty =
            BindableProperty.Create(nameof(TapCommand), typeof(ICommand), typeof(GestureView));

        public static readonly BindableProperty LongPressCommandProperty =
            BindableProperty.Create(nameof(LongPressCommand), typeof(ICommand), typeof(GestureView));

        public static readonly BindableProperty FlingCommandProperty =
            BindableProperty.Create(nameof(FlingCommand), typeof(ICommand), typeof(GestureView));

        public static readonly BindableProperty ScaleCommandProperty =
            BindableProperty.Create(nameof(ScaleCommand), typeof(ICommand), typeof(GestureView));

        // Properties
        public bool IsGestureEnabled
        {
            get => (bool)GetValue(IsGestureEnabledProperty);
            set => SetValue(IsGestureEnabledProperty, value);
        }

        public bool IsScaleEnabled
        {
            get => (bool)GetValue(IsScaleEnabledProperty);
            set => SetValue(IsScaleEnabledProperty, value);
        }

        public ICommand? PanCommand
        {
            get => (ICommand?)GetValue(PanCommandProperty);
            set => SetValue(PanCommandProperty, value);
        }

        public ICommand? TapCommand
        {
            get => (ICommand?)GetValue(TapCommandProperty);
            set => SetValue(TapCommandProperty, value);
        }

        public ICommand? LongPressCommand
        {
            get => (ICommand?)GetValue(LongPressCommandProperty);
            set => SetValue(LongPressCommandProperty, value);
        }

        public ICommand? FlingCommand
        {
            get => (ICommand?)GetValue(FlingCommandProperty);
            set => SetValue(FlingCommandProperty, value);
        }

        public ICommand? ScaleCommand
        {
            get => (ICommand?)GetValue(ScaleCommandProperty);
            set => SetValue(ScaleCommandProperty, value);
        }

        // Events
        public event EventHandler<GestureEventArgs>? Pan;
        public event EventHandler<GestureEventArgs>? Tap;
        public event EventHandler<GestureEventArgs>? LongPress;
        public event EventHandler<GestureEventArgs>? Fling;
        public event EventHandler<ScaleEventArgs>? ScaleBegin;
        public event EventHandler<ScaleEventArgs>? Scale;
        public event EventHandler<ScaleEventArgs>? ScaleEnd;

        // Event invokers
        public virtual void OnPan(GestureEventArgs args)
        {
            Pan?.Invoke(this, args);
            if (PanCommand?.CanExecute(args) == true)
                PanCommand.Execute(args);
        }

        public virtual void OnTap(GestureEventArgs args)
        {
            Tap?.Invoke(this, args);
            if (TapCommand?.CanExecute(args) == true)
                TapCommand.Execute(args);
        }

        public virtual void OnLongPress(GestureEventArgs args)
        {
            LongPress?.Invoke(this, args);
            if (LongPressCommand?.CanExecute(args) == true)
                LongPressCommand.Execute(args);
        }

        public virtual void OnFling(GestureEventArgs args)
        {
            Fling?.Invoke(this, args);
            if (FlingCommand?.CanExecute(args) == true)
                FlingCommand.Execute(args);
        }

        public virtual void OnScaleBegin(ScaleEventArgs args)
        {
            ScaleBegin?.Invoke(this, args);
        }

        public virtual void OnScale(ScaleEventArgs args)
        {
            Scale?.Invoke(this, args);
            if (ScaleCommand?.CanExecute(args) == true)
                ScaleCommand.Execute(args);
        }

        public virtual void OnScaleEnd(ScaleEventArgs args)
        {
            ScaleEnd?.Invoke(this, args);
        }
    }
}