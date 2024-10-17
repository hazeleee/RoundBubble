using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace RoundBubble
{
    public class ViewModel:INotifyPropertyChanged
    {
        private BitmapImage _lockImage;
        public BitmapImage LockImage
        { 
            get 
            { 
                return _lockImage; 
            }
            set
            {
                _lockImage= value;
                OnPropertyChanged("LockImage");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
           
    }
}
