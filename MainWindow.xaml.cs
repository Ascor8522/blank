using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using KeyEventHandler = System.Windows.Input.KeyEventHandler;
using Point = System.Windows.Point;

namespace Blank;

public partial class MainWindow
{
    private bool _wasFullScreen;
    private bool _resizeHandlerEnabled = true;

    public MainWindow()
    {
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        InitializeComponent();

        AddHandler(KeyDownEvent, new KeyEventHandler(OnKeyDownHandler));
        AddHandler(MouseDoubleClickEvent, new MouseButtonEventHandler(OnDoubleClickHandler));
        AddHandler(SizeChangedEvent, new SizeChangedEventHandler(OnWindowResizedHandler));
        AddHandler(MouseMoveEvent, new RoutedEventHandler(OnMouseMoveHandler));
    }

    private void OnKeyDownHandler(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.F11)
        {
            if (IsFullscreen()) UnFullscreen();
            else Fullscreen();
        }
        else if (e.Key == Key.Escape && IsFullscreen())
        {
            UnFullscreen();
        }
    }

    private void OnDoubleClickHandler(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton != MouseButton.Left) return;

        _resizeHandlerEnabled = false;

        if (IsFullscreen()) UnFullscreen();
        else Fullscreen();

        _resizeHandlerEnabled = true;
    }

    private void OnWindowResizedHandler(object sender, SizeChangedEventArgs e)
    {
        if (!_resizeHandlerEnabled) return;

        _resizeHandlerEnabled = false;

        if (!_wasFullScreen && WindowState == WindowState.Maximized)
        {
            WindowState = WindowState.Normal;
            Fullscreen();
            _wasFullScreen = true;
        }
        else if (_wasFullScreen && WindowState != WindowState.Maximized)
        {
            UnFullscreen();
            _wasFullScreen = false;
        }

        _resizeHandlerEnabled = true;
    }

    private void OnMouseMoveHandler(object sender, EventArgs e)
    {
        if (Mouse.LeftButton != MouseButtonState.Pressed) return;
        
        if (!_resizeHandlerEnabled) return;

        if (_wasFullScreen)
        {
            _resizeHandlerEnabled = false;

            UnFullscreen();
            CenterWindowAroundCursor();
            _wasFullScreen = false;

            _resizeHandlerEnabled = true;
        }

        DragMove();
    }

    private bool IsFullscreen()
    {
        return WindowStyle == WindowStyle.None
           && WindowState == WindowState.Maximized
           && Topmost;
    }

    // exact order must be:
    // 1. WindowStyle
    // 2. WindowState
    // 3. Topmost
    private void Fullscreen()
    {
        WindowStyle = WindowStyle.None;
        WindowState = WindowState.Maximized;
        // ResizeMode = ResizeMode.NoResize;
        Topmost = true;
    }

    private void UnFullscreen()
    {
        Topmost = false;
        // ResizeMode = ResizeMode.CanResize;
        WindowState = WindowState.Normal;
        WindowStyle = WindowStyle.SingleBorderWindow;
    }

    private void CenterWindowAroundCursor()
    {
        Matrix? transform = PresentationSource.FromVisual(this)?.CompositionTarget?.TransformFromDevice;
        if (!transform.HasValue) return;
        
        System.Drawing.Point mouseDrawingPosition = Control.MousePosition;
        Point mousePosition =  new Point(mouseDrawingPosition.X, mouseDrawingPosition.Y);
        Point transformedMousePosition = transform.Value.Transform(mousePosition);
        Left = transformedMousePosition.X - ActualWidth / 2;
        Top = transformedMousePosition.Y - ActualHeight / 2;
    }
}