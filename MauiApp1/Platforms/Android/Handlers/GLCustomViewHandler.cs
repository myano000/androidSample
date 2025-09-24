using Android.Content;
using Android.Opengl;
using Microsoft.Maui.Handlers;
using Javax.Microedition.Khronos.Opengles;
using AColor = Android.Graphics.Color;
using EGLConfig = Javax.Microedition.Khronos.Egl.EGLConfig;

namespace MauiApp1.Platforms.Android.Handlers
{
    public class GLCustomViewHandler : ViewHandler<IGLCustomView, GLSurfaceView>
    {
        public static IPropertyMapper<IGLCustomView, GLCustomViewHandler> Mapper = new PropertyMapper<IGLCustomView, GLCustomViewHandler>(ViewMapper)
        {
            [nameof(IGLCustomView.BackgroundColor)] = MapBackgroundColor,
        };

        public GLCustomViewHandler() : base(Mapper)
        {
        }

        protected override GLSurfaceView CreatePlatformView()
        {
            var glView = new GLCustomSurfaceView(Context, VirtualView);
            glView.SetEGLContextClientVersion(2);
            glView.SetRenderer(new GLCustomRenderer(VirtualView));
            glView.RenderMode = Rendermode.Continuously;
            return glView;
        }

        protected override void ConnectHandler(GLSurfaceView platformView)
        {
            base.ConnectHandler(platformView);
        }

        protected override void DisconnectHandler(GLSurfaceView platformView)
        {
            base.DisconnectHandler(platformView);
        }

        public static void MapBackgroundColor(GLCustomViewHandler handler, IGLCustomView view)
        {
        }
    }

    public class GLCustomSurfaceView : GLSurfaceView
    {
        private readonly IGLCustomView _virtualView;

        public GLCustomSurfaceView(Context context, IGLCustomView virtualView) : base(context)
        {
            _virtualView = virtualView;
        }

        protected override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();
            _virtualView.OnViewAttached();
        }

        protected override void OnDetachedFromWindow()
        {
            base.OnDetachedFromWindow();
            _virtualView.OnViewDetached();
        }
    }

    public class GLCustomRenderer : Java.Lang.Object, GLSurfaceView.IRenderer
    {
        private readonly IGLCustomView _view;
        private float _red = 0.0f;
        private float _green = 0.5f;
        private float _blue = 0.8f;

        public GLCustomRenderer(IGLCustomView view)
        {
            _view = view;
        }

        public void OnSurfaceCreated(IGL10? gl, EGLConfig? config)
        {
            _view.OnGLContextCreated();

            var color = _view.BackgroundColor;
            _red = (float)color.Red;
            _green = (float)color.Green;
            _blue = (float)color.Blue;
        }

        public void OnSurfaceChanged(IGL10? gl, int width, int height)
        {
            gl?.GlViewport(0, 0, width, height);
        }

        public void OnDrawFrame(IGL10? gl)
        {
            gl?.GlClearColor(_red, _green, _blue, 1.0f);
            gl?.GlClear(GL10.GlColorBufferBit | GL10.GlDepthBufferBit);

            _view.OnGLRender();
        }
    }
}