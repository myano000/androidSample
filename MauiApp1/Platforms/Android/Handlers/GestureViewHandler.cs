using Microsoft.Maui.Handlers;

namespace MauiApp1.Platforms.Android.Handlers
{
    public class GestureViewHandler : ViewHandler<GestureView, GestureNativeView>
    {
        public static IPropertyMapper<GestureView, GestureViewHandler> Mapper = new PropertyMapper<GestureView, GestureViewHandler>(ViewMapper)
        {
        };

        public GestureViewHandler() : base(Mapper)
        {
        }

        protected override GestureNativeView CreatePlatformView()
        {
            return new GestureNativeView(Context, VirtualView);
        }

        protected override void ConnectHandler(GestureNativeView platformView)
        {
            base.ConnectHandler(platformView);
            platformView.Connect();
        }

        protected override void DisconnectHandler(GestureNativeView platformView)
        {
            base.DisconnectHandler(platformView);
            platformView.Disconnect();
        }
    }
}