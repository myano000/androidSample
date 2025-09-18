using MauiApp1.ViewModels;
using MauiApp1.Behaviors;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        private readonly MainPageViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();
            _viewModel = new MainPageViewModel();
            BindingContext = _viewModel;

            // ViewModelにタッチビヘイビアを接続
            Loaded += OnPageLoaded;
        }

        private void OnPageLoaded(object? sender, EventArgs e)
        {
            // GridからBehaviorを取得
            var grid = Content as Grid;
            var touchBehavior = grid?.Behaviors?.OfType<ObservableTouchBehavior>().FirstOrDefault();
            if (touchBehavior != null)
            {
                _viewModel.AttachTouchBehavior(touchBehavior);
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _viewModel?.Dispose();
        }
    }
}
