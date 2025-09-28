using Android.Views;

namespace MauiApp1.Platforms.Android.Handlers
{
    public interface IGestureHandler 
    {
        bool OnTouchEvent(MotionEvent e);
    }

}