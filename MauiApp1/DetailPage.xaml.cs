namespace MauiApp1;

public partial class DetailPage : ContentPage
{
    public DetailPage()
    {
        InitializeComponent();
    }

    private void OnGLContextCreated(object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("GL Context Created");
    }

    private void OnGLRender(object sender, EventArgs e)
    {
    }

    private void OnViewAttached(object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("GLView Attached to Window - ページ表示時");
    }

    private void OnViewDetached(object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("GLView Detached from Window - ページ非表示時");
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///GestureTestPage");
    }
}