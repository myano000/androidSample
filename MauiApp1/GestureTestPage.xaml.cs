namespace MauiApp1;

public partial class GestureTestPage : ContentPage
{
    private string _logText = "";

    public GestureTestPage()
    {
        InitializeComponent();
    }

    private async void AddLog(string message)
    {
        // Ensure we're on the main thread
        if (!MainThread.IsMainThread)
        {
            MainThread.BeginInvokeOnMainThread(() => AddLog(message));
            return;
        }

        _logText += $"{DateTime.Now:HH:mm:ss.fff} - {message}\n";
        statusLabel.Text = _logText;

        // Keep only last 20 lines
        var lines = _logText.Split('\n');
        if (lines.Length > 20)
        {
            _logText = string.Join('\n', lines.Skip(lines.Length - 20));
            statusLabel.Text = _logText;
        }

        // Auto-scroll to bottom
        try
        {
            await logScrollView.ScrollToAsync(statusLabel, ScrollToPosition.End, false);
        }
        catch
        {
            // Ignore scroll errors if the view is not ready
        }
    }

    private void OnPan(object sender, GestureEventArgs e)
    {
        var fingerText = e.FingerCount == 1 ? "1 finger" : $"{e.FingerCount} fingers";
        AddLog($"Pan ({fingerText}): X={e.X:F1}, Y={e.Y:F1}, ΔX={e.DeltaX:F1}, ΔY={e.DeltaY:F1}");
    }

    private void OnTap(object sender, GestureEventArgs e)
    {
        var fingerText = e.FingerCount == 1 ? "1 finger" : $"{e.FingerCount} fingers";
        AddLog($"Tap ({fingerText}): X={e.X:F1}, Y={e.Y:F1}");
    }

    private void OnDoubleTap(object sender, GestureEventArgs e)
    {
        var fingerText = e.FingerCount == 1 ? "1 finger" : $"{e.FingerCount} fingers";
        AddLog($"DoubleTap ({fingerText}): X={e.X:F1}, Y={e.Y:F1}");
    }

    private void OnTwoFingerTap(object sender, GestureEventArgs e)
    {
        AddLog($"TwoFingerTap: X={e.X:F1}, Y={e.Y:F1}, Fingers={e.FingerCount}");
    }

    private void OnLongPress(object sender, GestureEventArgs e)
    {
        var fingerText = e.FingerCount == 1 ? "1 finger" : $"{e.FingerCount} fingers";
        AddLog($"LongPress ({fingerText}): X={e.X:F1}, Y={e.Y:F1}");
    }

    private void OnFling(object sender, GestureEventArgs e)
    {
        var fingerText = e.FingerCount == 1 ? "1 finger" : $"{e.FingerCount} fingers";
        AddLog($"Fling ({fingerText}): VX={e.VelocityX:F0}, VY={e.VelocityY:F0}");
    }

    private void OnScaleBegin(object sender, ScaleEventArgs e)
    {
        AddLog($"Scale Begin: Factor={e.ScaleFactor:F2}, Focus=({e.FocusX:F1}, {e.FocusY:F1})");
    }

    private void OnScale(object sender, ScaleEventArgs e)
    {
        AddLog($"Scale: Factor={e.ScaleFactor:F2}, Focus=({e.FocusX:F1}, {e.FocusY:F1})");
    }

    private void OnScaleEnd(object sender, ScaleEventArgs e)
    {
        AddLog($"Scale End: Factor={e.ScaleFactor:F2}, Focus=({e.FocusX:F1}, {e.FocusY:F1})");
    }

    private void OnDragStarted(object sender, DraggableTouchEventArgs e)
    {
        AddLog($"Drag Start: ({e.X:F0}, {e.Y:F0})");
    }

    private void OnDragMoved(object sender, DraggableTouchEventArgs e)
    {
        var logMessage = $"Drag: ({e.X:F0}, {e.Y:F0})";
        AddLog(logMessage);
    }

    private void OnDragEnded(object sender, DraggableTouchEventArgs e)
    {
        AddLog($"Drag End: ({e.X:F0}, {e.Y:F0})");
    }

    private async void OnNavigateClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///DetailPage");
    }
}