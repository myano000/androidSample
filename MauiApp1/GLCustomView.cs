namespace MauiApp1;

public class GLCustomView : View, IGLCustomView
{
    public static readonly BindableProperty BackgroundColorProperty =
        BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(GLCustomView), Colors.Black);

    public new Color BackgroundColor
    {
        get => (Color)GetValue(BackgroundColorProperty);
        set => SetValue(BackgroundColorProperty, value);
    }

    public event EventHandler? GLContextCreated;
    public event EventHandler? GLRender;
    public event EventHandler? ViewAttached;
    public event EventHandler? ViewDetached;

    public virtual void OnGLContextCreated()
    {
        GLContextCreated?.Invoke(this, EventArgs.Empty);
    }

    public virtual void OnGLRender()
    {
        GLRender?.Invoke(this, EventArgs.Empty);
    }

    public virtual void OnViewAttached()
    {
        ViewAttached?.Invoke(this, EventArgs.Empty);
    }

    public virtual void OnViewDetached()
    {
        ViewDetached?.Invoke(this, EventArgs.Empty);
    }
}