namespace MauiApp1;

public partial class GestureTestPage : ContentPage
{
    public GestureTestPage()
    {
        InitializeComponent();
    }

    private void OnPan(object sender, GestureEventArgs e)
    {
        // ジェスチャー処理のプレースホルダー
    }

    private void OnTap(object sender, GestureEventArgs e)
    {
        // ジェスチャー処理のプレースホルダー
    }

    private void OnDoubleTap(object sender, GestureEventArgs e)
    {
        // ジェスチャー処理のプレースホルダー
    }

    private void OnTwoFingerTap(object sender, GestureEventArgs e)
    {
        // ジェスチャー処理のプレースホルダー
    }

    private void OnLongPress(object sender, GestureEventArgs e)
    {
        // ジェスチャー処理のプレースホルダー
    }

    private void OnFling(object sender, GestureEventArgs e)
    {
        // ジェスチャー処理のプレースホルダー
    }

    private void OnScaleBegin(object sender, ScaleEventArgs e)
    {
        // スケール処理のプレースホルダー
    }

    private void OnScale(object sender, ScaleEventArgs e)
    {
        // スケール処理のプレースホルダー
    }

    private void OnScaleEnd(object sender, ScaleEventArgs e)
    {
        // スケール処理のプレースホルダー
    }

    private async void OnNavigateClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///DetailPage");
    }
}