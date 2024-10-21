using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace RoundBubble
{
    public class KeyboardInfo : INotifyPropertyChanged
    {
        private string _walrusKey;

        public string WalrusKey
        {
            get { return _walrusKey; }
            set
            {
                _walrusKey = value;
                OnPropertyChanged("WalrusKey");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
