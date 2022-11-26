using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Simplex
{
    internal class UScreen : Notify
    {
        private UserControl content;
        public UserControl Content
        {
            get => content;
            set { content = value; OnPropertyChanged(new PropertyChangedEventArgs("Content")); }
        }

        private string title;
        public string Title
        {
            get => title;
            set { title = value; OnPropertyChanged(new PropertyChangedEventArgs("Title")); }
        }

        public UScreen()
        {
            Title = "Método SIMPLEX - IFSPSC";
            Content = new UserControl1();
        }
    }
}
