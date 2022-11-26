using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplex.Calculo
{
    [Serializable]
    public class FuncaoObjetiva : Notify
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

        public void IgualarZero()
        {
            X1 = X1 * -1;
            X2 = X2 * -1;
            X3 = X3 * -1;          
        }
    }
}
