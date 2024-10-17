using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
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
        private DispatcherTimer timer;
        public bool Pin { get; set; } = false;
        public bool Lock { get; set; } = false;
        public BitmapImage BitmapImage { get; set; }=new BitmapImage(new Uri("/RoundBubble;component/Resources/unlock.png", UriKind.Relative));
        public MainWindow()
        {
            InitializeComponent();

            timer=new DispatcherTimer();
            timer.Interval=TimeSpan.FromSeconds(0.8);
            timer.Tick += Timer_Tick;
            //ViewModel=new ViewModel();
            //DataContext = ViewModel;

            //BitmatImage = new BitmapImage(new Uri("/RoundBubble;component/Resources/Lock.png", UriKind.Relative));
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
    }
}
