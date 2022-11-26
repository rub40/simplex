using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Simplex
{
    internal class MainWindowController : Notify
    {
        private UScreen currentScreen;
        public UScreen CurrentScreen
        {
            get => currentScreen;
            set { currentScreen = value; OnPropertyChanged(new PropertyChangedEventArgs("CurrentScreen")); }
        }

        public MainWindowController()
        {
            CurrentScreen = new UScreen();
        }
    }
}
