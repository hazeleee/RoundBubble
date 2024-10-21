using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace RoundBubble
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int WS_EX_TRANSPARENT = 0x20;
        private const int GWL_EXSTYLE = -20;
        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        private static extern uint SetWindowLong(IntPtr hwnd,int nIndex,uint dwNewLong);
        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        private static extern uint GetWindowLong(IntPtr hwnd, int nIndex);

        //private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);
        private KeyboardInfo _keyboardInfo {  get; set; }
        public KeyboardHook _keyboardHook { get; set; }

        private DispatcherTimer timer;
        public bool ListenKey { get; set; } = false;
        public bool Pin { get; set; } = false;
        public bool Lock { get; set; } = false;
        public BitmapImage BitmapImage { get; set; } = new BitmapImage(new Uri("/RoundBubble;component/Resources/unlock.png", UriKind.Relative));
        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += Timer_Tick;

            _keyboardInfo = new KeyboardInfo();
            //DataContext = _keyboardInfo;

            _keyboardHook = new KeyboardHook();
            _keyboardHook.KeyPressed += _keyboardHook_KeyPressed;


            this.SourceInitialized += delegate
            {
                IntPtr hwnd = new WindowInteropHelper(this).Handle;
                uint extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
                SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_TRANSPARENT);
            };
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            Popup.IsOpen = false;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Popup.IsOpen = false;
            if (!this.Lock)
            {
                this.DragMove();
            }

        }

        private void WalrusWPF_MouseEnter(object sender, MouseEventArgs e)
        {

            Popup.IsOpen = true;

        }

        private void WalrusWPF_MouseLeave(object sender, MouseEventArgs e)
        {
            timer.Start();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();

        }

        private void Image_MouseLeftButtonDown_Lock(object sender, MouseButtonEventArgs e)
        {
            Image image = sender as Image;
            if (image != null)
            {
                this.Lock = !this.Lock;
                if (this.Lock)
                {
                    image.Source = new BitmapImage(new Uri("/RoundBubble;component/Resources/Lock.png", UriKind.Relative));
                }
                else
                {
                    image.Source = new BitmapImage(new Uri("/RoundBubble;component/Resources/unlock.png", UriKind.Relative));
                }
            }
            Popup.IsOpen = false;
        }

        private void Image_MouseLeftButtonDown_Pin(object sender, MouseButtonEventArgs e)
        {
            Image image = sender as Image;
            if (image != null)
            {
                this.Pin = !this.Pin;
                if (this.Pin)
                {
                    image.Source = new BitmapImage(new Uri("/RoundBubble;component/Resources/Pin.png", UriKind.Relative));
                    this.Topmost = true;
                }
                else
                {
                    image.Source = new BitmapImage(new Uri("/RoundBubble;component/Resources/UnPin.png", UriKind.Relative));
                    this.Topmost = false;
                }
            }
            Popup.IsOpen = false;
        }
       public void Image_MouseLeftButtonDown_ListenKey(object sender, MouseButtonEventArgs e)
        {
            Image image = sender as Image;
            if (image != null)
            {
                this.ListenKey = !this.ListenKey;
                if (this.ListenKey)
                {
                    _keyboardHook.UnhookKeyboard();
                    _keyboardHook.KeyPressed += _keyboardHook_KeyPressed;
                    
                    _keyboardHook =new KeyboardHook();
                    image.Source = new BitmapImage(new Uri("/RoundBubble;component/Resources/Listen.png", UriKind.Relative));
                }
                else
                {
                    _keyboardHook.UnhookKeyboard();
                    //ListenText.Text = $"Walrus";
                    image.Source = new BitmapImage(new Uri("/RoundBubble;component/Resources/NoListen.png", UriKind.Relative));
                }
            }
            Popup.IsOpen = false;
        }

        private void _keyboardHook_KeyPressed(object sender, KeyEventArgs e)
        {
            ListenText.Text = e.Key.ToString();
            Storyboard fadeOutAndMoveUpAnimation = (Storyboard)FindResource("FadeOutAndMoveUpAnimation");
            if (fadeOutAndMoveUpAnimation != null)
            {

                ListenText.BeginStoryboard(fadeOutAndMoveUpAnimation);
            }

            // 0.5 秒后重置文本
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += (s, args) =>
            {
                //ListenText.Text = $"{e.Key}";
                ListenText.Opacity = 1.0;
                ListenText.RenderTransform = new TranslateTransform();
                timer.Stop();
            };
            timer.Interval = TimeSpan.FromSeconds(0.4);
            timer.Start();
        }

        public void KeyboardHook_KeyPressed(object sender, KeyEventArgs e)
        {
            
            
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _keyboardHook.UnhookKeyboard();
        }




        #region NoUse
        //private void MenuItem_Checked(object sender, RoutedEventArgs e)
        //{
        //    KeyDown -= MainWindow_KeyDown;
        //    KeyDown += MainWindow_KeyDown;

        //}

        //private void MenuItem_Unchecked(object sender, RoutedEventArgs e)
        //{
        //    KeyDown -= MainWindow_KeyDown;
        //}

        //private void TextBlock_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        //{
        //    if (!(sender is TextBlock textBlock)) return;

        //    if (textBlock.ContextMenu != null)
        //    {
        //        textBlock.ContextMenu.IsOpen = true;
        //        e.Handled = true;
        //    }
        //}
        #endregion

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            ListenText.Text = _keyboardHook.WalrusKey;
        }
    }
}
