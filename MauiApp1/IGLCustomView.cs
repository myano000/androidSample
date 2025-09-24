namespace MauiApp1
{
    public interface IGLCustomView : IView
    {
        Color BackgroundColor { get; set; }

        event EventHandler? GLContextCreated;
        event EventHandler? GLRender;
        event EventHandler? ViewAttached;
        event EventHandler? ViewDetached;

        void OnGLContextCreated();
        void OnGLRender();
        void OnViewAttached();
        void OnViewDetached();
    }
}