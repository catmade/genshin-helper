using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace genshin_helper
{
    public partial class ClickArea : Window
    {
        ///win32 api
        private const int WS_EX_TRANSPARENT = 0x20;

        private const int GWL_EXSTYLE = (-20);
        private bool _mousePassThrough;
        private double _opacity;
        private int _radius;

        public ClickArea() : this(50, 0.5)
        {
        }

        public ClickArea(int radius, double opacity)
        {
            InitializeComponent();
            DataContext = this;
            Radius = radius;
        }

        public int Radius
        {
            get => _radius;
            set
            {
                if (value == _radius) return;
                _radius = value;
                this.Height = _radius;
                this.Width = _radius;
                this.border.CornerRadius = new CornerRadius
                {
                    TopLeft = _radius,
                    TopRight = _radius,
                    BottomLeft = _radius,
                    BottomRight = _radius,
                };
            }
        }

        public bool MousePassThrough
        {
            get => _mousePassThrough;
            set
            {
                if (_mousePassThrough == value) return;
                _mousePassThrough = value;
                if (_mousePassThrough) enableMousePassThrough();
                else disableMousePassThrough();
            }
        }

        public new double Opacity
        {
            get => _opacity;
            set
            {
                if (_opacity == value) return;
                _opacity = value;
                this.border.Opacity = _opacity;
            }
        }

        private void enableMousePassThrough()
        {
            IntPtr hwnd = new WindowInteropHelper(this).Handle;
            uint extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
            SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_TRANSPARENT);
        }

        private void disableMousePassThrough()
        {
            IntPtr hwnd = new WindowInteropHelper(this).Handle;
            uint extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
            SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle ^ WS_EX_TRANSPARENT);
        }

        [DllImport("user32", EntryPoint = "SetWindowLong")]
        private static extern uint SetWindowLong(IntPtr hwnd, int nIndex, uint dwNewLong);

        [DllImport("user32", EntryPoint = "GetWindowLong")]
        private static extern uint GetWindowLong(IntPtr hwnd, int nIndex);

        private void Window_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed) this.DragMove();
        }
    }
}