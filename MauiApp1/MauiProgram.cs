using Microsoft.Extensions.Logging;
#if ANDROID
using MauiApp1.Platforms.Android.Handlers;
#endif

namespace MauiApp1
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .ConfigureMauiHandlers(handlers =>
                {
#if ANDROID
                    handlers.AddHandler<GestureView, GestureViewHandler>();
                    handlers.AddHandler<DraggableView, DraggableViewHandler>();
                    handlers.AddHandler<GLCustomView, GLCustomViewHandler>();
#endif
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
