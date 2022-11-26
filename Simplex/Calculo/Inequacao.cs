using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplex.Calculo
{
    [Serializable]
    public class Inequacao : Notify
    {
        private long x1;
        public long X1
        {
            get => x1;
            set { x1 = value; OnPropertyChanged(new PropertyChangedEventArgs("X1")); }
        }

        private long x2;
        public long X2
        {
            get => x2;
            set { x2 = value; OnPropertyChanged(new PropertyChangedEventArgs("X2")); }
        }

        private long x3;
        public long X3
        {
            get => x3;
            set { x3 = value; OnPropertyChanged(new PropertyChangedEventArgs("X3")); }
        }

        private int ordem;
        public int Ordem
        {
            get => ordem;
            set { ordem = value; OnPropertyChanged(new PropertyChangedEventArgs("Ordem")); }
        }

        private long total;
        public long Total
        {
            get => total;
            set { total = value; OnPropertyChanged(new PropertyChangedEventArgs("Total")); }
        }
    }
}
